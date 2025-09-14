using Microsoft.Extensions.AI;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public class APF3
	{
		// Luôn cung cấp đầy đủ context (error message, code snippet, stack trace)
		// Yêu cầu giải thích từng bước(step-by-step analysis)
		// Request multiple solutions nếu có thể
		// Verify fixes với compiler/runtime thực tế

		public static readonly AdditionalPropertiesDictionary AP3010_ExplainError = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.1 },			// RẤT THẤP - cần chính xác tuyệt đối
			{ "top_p", 0.85 },				// Tập trung vào solutions đúng
			{ "frequency_penalty", 0.2 },	// Tránh lặp lại lỗi
			{ "presence_penalty", 0.15 },	// Khuyến khích solutions mới
		};

		public static readonly AdditionalPropertiesDictionary AP3020_SuggestFixes = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.2 },			// Cần sáng tạo để tìm bug
			{ "top_p", 0.95 },				// Nhiều perspectives
		};
	}
}
