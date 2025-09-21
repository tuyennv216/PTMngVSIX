using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using System.Collections.Generic;

namespace PTMngVSIX.Abstraction.AIServices.RequestModel
{
	public class RequestBase
	{
		/// <summary>
		/// Tên tác vụ, ví dụ: "GenerateCode", "ExplainCode", "FixBug", v.v.
		/// </summary>
		public string Task { get; set; }

		public List<string> History { get; set; } = new List<string>();
		public RequestBase LastRequest { get; set; }
		public ResponseBase LastResponse { get; set; }

		/// <summary>
		/// Những thông tin bổ sung
		/// </summary>
		public string Information { get; set; }

		/// <summary>
		/// Mô tả, yêu cầu từ người dùng
		/// </summary>
		public string Prompt { get; set; }

		public bool IsEmpty =>
			Task.Length == 0 &&
			Information.Length == 0 &&
			(Prompt == null || Prompt.Length == 0);
	}
}
