using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public static class Apf1
	{
		public static readonly Dictionary<string, object> AP1000_GenerateCode = new Dictionary<string, object>
		{
			{ "temperature", 0.2 },			// Thấp - tập trung vào accuracy
			{ "top_p", 0.9 },				// Đa dạng syntax hợp lệ
			{ "frequency_penalty", 0.1 },	// Tránh lặp code patterns
			{ "presence_penalty", 0.05 },	// Khuyến khích solutions mới
			{ "top_k", 45 }
		};

		public static readonly Dictionary<string, object> AP1001_GenerateFunction = new Dictionary<string, object>
		{
			{ "temperature", 0.15 },		// Rất thấp - functions cần chính xác
			{ "top_p", 0.88 },				// Tập trung vào logic đúng
			{ "frequency_penalty", 0.15 },	// Tránh lặp function patterns
			{ "presence_penalty", 0.0 },	// Giữ consistency
			{ "top_k", 40 }
		};

		public static readonly Dictionary<string, object> AP1002_GenerateClass = new Dictionary<string, object>
		{
			{ "temperature", 0.18 },		// Thấp - class structure cần rõ ràng
			{ "top_p", 0.92 },				// Đa dạng class design patterns
			{ "frequency_penalty", 0.08 },	// Tránh lặp class structures
			{ "presence_penalty", 0.1 },	// Khuyến khích OOP best practices
			{ "top_k", 50 }
		};

		public static readonly Dictionary<string, object> AP1010_Reflect = new Dictionary<string, object>
		{
			{ "temperature", 0.4 },			// Cần sáng tạo để cải tiến
			{ "top_p", 0.92 },				// Cân bằng giữa mới và cũ
			{ "frequency_penalty", 0.15 },	// Giảm lặp pattern cũ
			{ "presence_penalty", 0.1 },
		};

		public static readonly Dictionary<string, object> AP1011_ReflectFunction = new Dictionary<string, object>
		{
			{ "temperature", 0.2 },          // Ấn định sáng tạo thấp - tập trung tối ưu logic function
			{ "top_p", 0.7 },                // Chọn chặt chẽ - ưu tiên giải pháp refactor hiệu quả
			{ "frequency_penalty", 0.25 },   // Tăng cường tránh mã trùng lặp/cũ trong function
			{ "presence_penalty", 0.1 },     // Duy trì tính nhất quán và các thành phần chức năng chính
		};

		public static readonly Dictionary<string, object> AP1012_ReflectClass = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },          // Giảm sáng tạo, tập trung vào cấu trúc logic
			{ "top_p", 0.85 },               // Chọn lọc chặt chẽ hơn các phương án refactor
			{ "frequency_penalty", 0.2 },    // Tăng cường tránh lặp lại pattern cũ
			{ "presence_penalty", 0.05 },    // Giảm nhẹ để duy trì tính nhất quán
		};

		public static readonly Dictionary<string, object> AP1004_FillInMiddle = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },			// Cần hiểu context và điền phù hợp
			{ "top_p", 0.95 },				// Linh hoạt với existing code
			{ "frequency_penalty", 0.0 },	// Giữ nguyên pattern hiện có
			{ "presence_penalty", 0.0 },
		};

		public static readonly Dictionary<string, object> AP1005_AddComment = new Dictionary<string, object>
		{
			{ "temperature", 0.2 },
			{ "top_p", 0.9 },
			{ "frequency_penalty", 0.15 },
			{ "presence_penalty", 0.1 },
			{ "top_k", 40 }
		};

		public static readonly Dictionary<string, object> AP1006_CompleteFunction = new Dictionary<string, object>
		{
			{ "temperature", 0.1 },          // Rất ít sáng tạo - hoàn thành function theo ngữ cảnh hiện có
			{ "top_p", 0.6 },                // Chọn lọc rất chặt - chỉ giữ phương án hoàn thành phù hợp nhất
			{ "frequency_penalty", 0.1 },    // Giảm nhẹ để duy trì pattern code hiện tại
			{ "presence_penalty", 0.02 },    // Rất thấp - đảm bảo giữ nguyên bối cảnh và biến hiện có
		};

		public static readonly Dictionary<string, object> AP1040_Summary = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },			// Diễn đạt tự nhiên nhưng chính xác
			{ "top_p", 0.98 },				// Đa dạng ngôn ngữ mô tả
		};

		public static readonly Dictionary<string, object> AP1050_Optimize = new Dictionary<string, object>
		{
			{ "temperature", 0.25 },		// Cần solutions hiệu quả nhưng an toàn
			{ "top_p", 0.88 },				// Tập trung vào optimization patterns
			{ "frequency_penalty", 0.1 },	// Tránh redundant optimizations
			{ "presence_penalty", 0.05 },
		};

		public static readonly Dictionary<string, object> AP1060_Explain = new Dictionary<string, object>
		{
			{ "temperature", 0.35 },		// Giải thích rõ ràng, dễ hiểu
			{ "top_p", 0.96 },				// Đa dạng cách diễn đạt
		};
	}
}
