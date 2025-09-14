namespace PTMngVSIX.Abstraction.ResponseModel
{
	public class ResponseBase
	{
		public string Type { get; set; }
		public string Summary { get; set; }
		public string Solution { get; set; }
		public string Answer { get; set; }

		public ResponseBase()
		{
		}

		public static ResponseBase Unknow (string answer)
		{
			return new ResponseBase
			{
				Type = "Unknow",
				Answer = answer
			};
		}

		public static ResponseBase Error(string answer)
		{
			return new ResponseBase
			{
				Type = "Error",
				Answer = answer
			};
		}
	}
}
