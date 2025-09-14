namespace PTMngVSIX.Data.Constant.TaskName
{
	public static class TaskF1
	{
		public const string GenerateCode = "generate_code";
		public const string GenerateFunction = "generate_function";
		public const string GenerateClass = "generate_class";
		public const string RefelctCode = "reflect_code";

		public const string FillInMiddle = "fill_in_middle";
		public const string AddComment = "add_comments";
		public const string CompleteFunction = "complete_function";

		public const string OptimizeFunction = "optimize_function";
		public const string SummaryFunction = "summary_function";
		public const string ExplainFunction = "explain_function";
	}

	public static class TaskF2
	{
		public const string AutoCompletion = "auto_completion";
		public const string SuggestCode = "suggest_code";

		public const string GenerateDocsSelection = "generate_docs";
		public const string GenerateDocsFunction = "generate_docs_function";
		public const string GenerateDocsClass = "generate_docs_class";
		public const string GenerateDocsApi = "generate_docs_api";
		public const string GenerateDocsTechnicalSpecification = "generate_docs_specification";
	}

	public static class TaskF3
	{
		public const string ExplainError = "explain_error";
		public const string SuggestFixes = "suggest_fixes";
	}

	public static class TaskF4
	{
		public const string Whitebox = "generate_test_cases_white_box";
		public const string Blackbox_UnitTests = "generate_test_cases_black_box_unit_tests";
		public const string Blackbox_IntegrationTests = "generate_test_cases_black_box_integration_tests";
		public const string Blackbox_EdgeCaseTesting = "generate_test_cases_black_box_edge_case_testing";
		public const string Blackbox_PerformanceTesting = "generate_test_cases_black_box_performance_testing";
		public const string ReviewTests = "review_tests";
	}

	public static class TaskF5
	{
		public const string ReviewCodeFunction = "review_code_function";
		public const string ReviewCodeClass = "review_code_class";
		public const string SuggestSolution = "suggest_solution";
		public const string SuggestDeploy = "suggest_deploy";
		public const string SuggestArchitecture = "suggest_architecture";
		public const string SuggestTechnologies = "suggest_technologies";
		public const string SuggestFeatures = "suggest_features";
	}

	public static class TaskF6
	{
		public const string Chat = "chat";
	}

	public static class Translator
	{
		public const string Translate = "translate";
	}
}
