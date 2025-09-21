using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PTMngVSIX.Abstraction.AI
{
	public class BaseOpenAILikeChatClient : IChatClient
	{
		private HttpClient _httpClient;
		private readonly string _endpoint;
		private readonly string _apiKey;
		private readonly string _defaultModelId;
		private readonly JsonSerializerOptions _jsonOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		public BaseOpenAILikeChatClient(string endpoint, string apiKey, string defaultModelId = "")
		{
			_endpoint = endpoint;
			_apiKey = apiKey;
			_defaultModelId = defaultModelId;

			WithHttpClient(new HttpClient());
		}

		public void WithHttpClient(HttpClient httpClient)
		{
			_httpClient?.Dispose();
			_httpClient = httpClient;

			// Thiết lập base URL và headers
			_httpClient.BaseAddress = new Uri(_endpoint);
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
			//_httpClient.DefaultRequestHeaders.Add("HTTP-Referer", _websiteUrl);
			_httpClient.DefaultRequestHeaders.Add("X-Title", "PTMng");
			//_httpClient.DefaultRequestHeaders.Add("X-API-Version", "1.0");
			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public Task<string> GetResponseAsync(string prompt)
		{
			var messages = new ChatMessage[] { new (ChatRoles.User, prompt ) };
			var options = new ChatOptions
			{
				ModelId = _defaultModelId
			};

			return GetResponseAsync(messages, options);
		}

		public Task<string> GetResponseAsync(ChatMessage message, ChatOptions options)
		{
			var messages = new ChatMessage[] { message };

			return GetResponseAsync(messages, options);
		}

		public async Task<string> GetResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions options)
		{
			var requestBody = CreateRequestBody(messages, options, false);
			var content = new StringContent(
				JsonSerializer.Serialize(requestBody),
				Encoding.UTF8,
				"application/json"
			);

			var targetUri = _httpClient.BaseAddress.AbsolutePath + "/chat/completions";
			var response = await _httpClient.PostAsync(targetUri, content);
			response.EnsureSuccessStatusCode();

			var responseContent = await response.Content.ReadAsStringAsync();
			var fixedContent = responseContent.Trim(' ', '\n', '\r');
			var responseObject = JsonSerializer.Deserialize<OpenAIResponse>(fixedContent, _jsonOptions);

			return responseObject?.Choices?[0]?.Message?.Content ?? string.Empty;
		}

		public async IAsyncEnumerable<string> GetStreamResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions options)
		{
			var requestBody = CreateRequestBody(messages, options, true);
			var content = new StringContent(
				JsonSerializer.Serialize(requestBody),
				Encoding.UTF8,
				"application/json"
			);

			var targetUri = _httpClient.BaseAddress.AbsolutePath + "/chat/completions";
			var response = await _httpClient.PostAsync(targetUri, content);
			response.EnsureSuccessStatusCode();

			using var stream = await response.Content.ReadAsStreamAsync();
			using var reader = new System.IO.StreamReader(stream);

			while (!reader.EndOfStream)
			{
				var line = await reader.ReadLineAsync();
				if (!string.IsNullOrEmpty(line) && line.StartsWith("data: "))
				{
					var jsonData = line.Substring(6);
					if (jsonData != "[DONE]")
					{
						var chunk = JsonSerializer.Deserialize<OpenAIStreamResponse>(jsonData);
						if (chunk?.Choices?[0]?.Delta?.Content != null)
						{
							yield return chunk.Choices[0].Delta.Content;
						}
					}
				}
			}
		}

		private object CreateRequestBody(IEnumerable<ChatMessage> messages, ChatOptions options, bool stream)
		{
			var requestMessages = new List<object>();
			foreach (var msg in messages)
			{
				requestMessages.Add(new
				{
					role = msg.Role,
					content = msg.Content
				});
			}

			var deepinfraOptions = new Dictionary<string, object>
			{
				{ "reasoning", false },          // Yêu cầu không trả về reasoning
				{ "include_reasoning", false },  // Yêu cầu không trả về reasoning
				{ "intermediary", false }        // Đảm bảo không trả về nội dung trung gian
			};

			var requestBody = new Dictionary<string, object>
			{
				{ "model", options.ModelId },
				{ "messages", requestMessages },
				{ "stream", stream },
				{ "deepinfra", deepinfraOptions }
			};

			if (string.IsNullOrEmpty(options.ModelId))
			{
				requestBody["model"] = _defaultModelId;
			}

			// Thêm các AdditionalProperties
			foreach (var prop in options.AdditionalProperties)
			{
				requestBody[prop.Key] = prop.Value;
			}

			return requestBody;
		}
	}
}
