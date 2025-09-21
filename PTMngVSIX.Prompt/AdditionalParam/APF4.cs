using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public static class Apf4
	{
		public static readonly Dictionary<string, object> AP4010_WhiteBox = new Dictionary<string, object>
		{
			{ "temperature", 0.15 },		// Thấp - tập trung vào code coverage
			{ "top_p", 0.88 },				// Tập trung vào paths logic
			{ "frequency_penalty", 0.1 },	// Tránh lặp test cases
			{ "presence_penalty", 0.05 },	// Khuyến khích edge cases mới
		};

		public static readonly Dictionary<string, object> AP4020_Blackbox_UnitTests = new Dictionary<string, object>
		{
			{ "temperature", 0.1 },			// Rất thấp - precise assertions
			{ "top_p", 0.85 },				// Tập trung vào method logic
		};

		public static readonly Dictionary<string, object> AP4021_Blackbox_IntegrationTests = new Dictionary<string, object>
		{
			{ "temperature", 0.2 },			// Balance giữa coverage và scenarios
			{ "top_p", 0.92 },				// Đa dạng interaction patterns
		};

		public static readonly Dictionary<string, object> AP4022_Blackbox_EdgeCaseTesting = new Dictionary<string, object>
		{
			{ "temperature", 0.3 },			// Creative edge cases
			{ "top_p", 0.98 },				// Maximum diversity
			{ "frequency_penalty", -0.1 },	// Khuyến khích extreme values
		};

		public static readonly Dictionary<string, object> AP4023_Blackbox_PerformanceTesting = new Dictionary<string, object>
		{
			{ "temperature", 0.18 },		// Tập trung vào load scenarios
			{ "top_p", 0.9 },				// Realistic workload patterns
		};
	}
}
