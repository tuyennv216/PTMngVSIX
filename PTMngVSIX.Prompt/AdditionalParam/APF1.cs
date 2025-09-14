using Microsoft.Extensions.AI;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public class APF1
	{
		public static readonly AdditionalPropertiesDictionary AP1000_GenerateCode = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.2 },			// Thấp - tập trung vào accuracy
			{ "top_p", 0.9 },				// Đa dạng syntax hợp lệ
			{ "frequency_penalty", 0.1 },	// Tránh lặp code patterns
			{ "presence_penalty", 0.05 },	// Khuyến khích solutions mới
			{ "top_k", 45 }
		};

		public static readonly AdditionalPropertiesDictionary AP1001_GenerateFunction = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.15 },		// Rất thấp - functions cần chính xác
			{ "top_p", 0.88 },				// Tập trung vào logic đúng
			{ "frequency_penalty", 0.15 },	// Tránh lặp function patterns
			{ "presence_penalty", 0.0 },	// Giữ consistency
			{ "top_k", 40 }
		};

		public static readonly AdditionalPropertiesDictionary AP1002_GenerateClass = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.18 },		// Thấp - class structure cần rõ ràng
			{ "top_p", 0.92 },				// Đa dạng class design patterns
			{ "frequency_penalty", 0.08 },	// Tránh lặp class structures
			{ "presence_penalty", 0.1 },	// Khuyến khích OOP best practices
			{ "top_k", 50 }
		};

		public static readonly AdditionalPropertiesDictionary AP1003_Reflect = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.4 },			// Cần sáng tạo để cải tiến
			{ "top_p", 0.92 },				// Cân bằng giữa mới và cũ
			{ "frequency_penalty", 0.15 },	// Giảm lặp pattern cũ
			{ "presence_penalty", 0.1 },
		};

		public static readonly AdditionalPropertiesDictionary AP1004_FillInMiddle = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.3 },			// Cần hiểu context và điền phù hợp
			{ "top_p", 0.95 },				// Linh hoạt với existing code
			{ "frequency_penalty", 0.0 },	// Giữ nguyên pattern hiện có
			{ "presence_penalty", 0.0 },
		};

		public static readonly AdditionalPropertiesDictionary AP1005_AddComment = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.2 },
			{ "top_p", 0.9 },
			{ "frequency_penalty", 0.15 },
			{ "presence_penalty", 0.1 },
			{ "top_k", 40 }
		};

		public static readonly AdditionalPropertiesDictionary AP1040_Summary = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.3 },			// Diễn đạt tự nhiên nhưng chính xác
			{ "top_p", 0.98 },				// Đa dạng ngôn ngữ mô tả
		};

		public static readonly AdditionalPropertiesDictionary AP1050_Optimize = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.25 },		// Cần solutions hiệu quả nhưng an toàn
			{ "top_p", 0.88 },				// Tập trung vào optimization patterns
			{ "frequency_penalty", 0.1 },	// Tránh redundant optimizations
			{ "presence_penalty", 0.05 },
		};

		public static readonly AdditionalPropertiesDictionary AP1060_Explain = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.35 },		// Giải thích rõ ràng, dễ hiểu
			{ "top_p", 0.96 },				// Đa dạng cách diễn đạt
		};
	}
}
