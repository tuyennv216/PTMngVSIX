namespace PTMngVSIX.Prompt.OutputModel
{
	public class ModelReturn_v2 : IReturnModel
	{
		public string Type { get; set; }
		public string Summary { get; set; }
		public string Solution { get; set; }
		public string Answer { get; set; }
	}
}
