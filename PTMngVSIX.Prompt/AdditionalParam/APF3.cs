using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public static class Apf3
	{
		// Luôn cung cấp đầy đủ context (error message, code snippet, stack trace)
		// Yêu cầu giải thích từng bước(step-by-step analysis)
		// Request multiple solutions nếu có thể
		// Verify fixes với compiler/runtime thực tế

		public static readonly Dictionary<string, object> AP3010_ExplainError = new Dictionary<string, object>
		{
			{ "temperature", 0.1 },			// RẤT THẤP - cần chính xác tuyệt đối
			{ "top_p", 0.85 },				// Tập trung vào solutions đúng
			{ "frequency_penalty", 0.2 },	// Tránh lặp lại lỗi
			{ "presence_penalty", 0.15 },	// Khuyến khích solutions mới
		};

		public static readonly Dictionary<string, object> AP3020_SuggestFixes = new Dictionary<string, object>
		{
			{ "temperature", 0.2 },			// Cần sáng tạo để tìm bug
			{ "top_p", 0.95 },				// Nhiều perspectives
		};
	}
}
