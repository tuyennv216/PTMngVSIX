using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	internal static class Apf2
	{
		public static readonly Dictionary<string, object> AP2000_DocsSelection = new Dictionary<string, object>
		{
			{ "temperature", 0.35 },		// Cần diễn đạt tự nhiên nhưng chính xác
			{ "top_p", 0.96 },				// Đa dạng ngôn ngữ mô tả
			{ "frequency_penalty", 0.0 },	// Giữ technical terms consistency
			{ "presence_penalty", 0.05 },	// Khuyến khích explanations mới
			{ "top_k", 55 }
		};

		public static readonly Dictionary<string, object> AP2001_DocsFunction = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },			// Tập trung vào accuracy function docs
			{ "top_p", 0.94 },				// Đa dạng cách mô tả parameters
			{ "frequency_penalty", 0.1 },	// Tránh lặp documentation patterns
			{ "presence_penalty", 0.0 },	// Giữ consistency
			{ "top_k", 50 }
		};

		public static readonly Dictionary<string, object> AP2002_DocsClass = new Dictionary<string, object>
		{
			{ "temperature", 0.4 },			// Cao hơn - cần mô tả architecture
			{ "top_p", 0.97 },				// Đa dạng class documentation styles
			{ "frequency_penalty", 0.0 },	// Giữ OOP terminology
			{ "presence_penalty", 0.1 },	// Khuyến khích comprehensive docs
			{ "top_k", 60 }
		};

		public static readonly Dictionary<string, object> AP2010_DocsApi = new Dictionary<string, object>
		{
			{ "temperature", 0.1 },
			{ "top_p", 0.95 },
			{ "frequency_penalty", 0.2 },
			{ "presence_penalty", 0.15 },
			{ "top_k", 40 }
		};

		public static readonly Dictionary<string, object> AP2020_DocsTechnicalSpecification = new Dictionary<string, object>
		{
			{ "temperature", 0.05 },
			{ "top_p", 0.98 },
			{ "frequency_penalty", 0.25 },
			{ "presence_penalty", 0.2 },
			{ "top_k", 30 }
		};

	}
}
