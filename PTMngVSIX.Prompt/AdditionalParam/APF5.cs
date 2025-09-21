using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public static class Apf5
	{
		public static readonly Dictionary<string, object> AP5020_ReviewFuncion = new Dictionary<string, object>
		{
			{ "temperature", 0.25 },		// Balanced - technical accuracy + insights
			{ "top_p", 0.92 },				// Đa dạng perspectives review
			{ "frequency_penalty", 0.1 },	// Tránh lặp comments
			{ "presence_penalty", 0.05 },	// Khuyến khích insights mới
			{ "top_k", 50 }
		};

		public static readonly Dictionary<string, object> AP5021_ReviewClass = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },
			{ "top_p", 0.96 },
			{ "frequency_penalty", 0.22 },
			{ "presence_penalty", 0.18 },
			{ "top_k", 38 }
		};

		public static readonly Dictionary<string, object> AP5030_Solution = new Dictionary<string, object>
		{
			{ "temperature", 0.35 },		// Cao hơn - creative solutions
			{ "top_p", 0.96 },				// Đa dạng architectural patterns
			{ "frequency_penalty", 0.0 },	// Giữ industry standards
			{ "presence_penalty", 0.1 },	// Khuyến khích innovative approaches
			{ "top_k", 65 }
		};

		public static readonly Dictionary<string, object> AP5040_Deploy = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },			// Cân bằng giữa best practices và practicality
			{ "top_p", 0.94 },				// Đa dạng deployment options
			{ "frequency_penalty", 0.05 },	// Giữ proven strategies
			{ "presence_penalty", 0.08 },	// Khuyến khích modern approaches
			{ "top_k", 55 }
		};

		public static readonly Dictionary<string, object> AP5050_Architecture = new Dictionary<string, object>
		{
			{ "temperature", 0.4 },			// Cần creativity cho architectural design
			{ "top_p", 0.97 },				// Rộng - nhiều patterns và styles
			{ "frequency_penalty", -0.1 },	// Khuyến khích industry standards
			{ "presence_penalty", 0.15 },	// Innovative architecture patterns
			{ "top_k", 70 }
		};

		public static readonly Dictionary<string, object> AP5060_Technologies = new Dictionary<string, object>
		{
			{ "temperature", 0.28 },		// Balanced tech recommendations
			{ "top_p", 0.93 },				// Đa dạng tech stack options
			{ "frequency_penalty", 0.0 },	// Giữ popular technologies
			{ "presence_penalty", 0.12 },	// Khuyến khích emerging tech
			{ "top_k", 60 }
		};

		public static readonly Dictionary<string, object> AP5070_Features = new Dictionary<string, object>
		{
			{ "temperature", 0.45 },		// Cao - creative feature ideas
			{ "top_p", 0.98 },				// Rất đa dạng feature suggestions
			{ "frequency_penalty", 0.2 },	// Tránh lặp features cơ bản
			{ "presence_penalty", 0.2 },	// Khuyến khích innovative features
			{ "top_k", 75 }
		};

	}
}
