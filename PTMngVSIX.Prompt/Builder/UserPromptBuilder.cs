using System.Text;

namespace PTMngVSIX.Prompt.Builder
{
	public class UserPromptBuilder
	{
		public static string Build(Abstraction.RequestModel.RequestBase request)
		{
			var sb = new StringBuilder();

			if (request.Information.Length > 0)
			{
				sb.AppendLine(request.Information);
				sb.AppendLine("Based on this context, please complete the requested action:");
			}

			sb.AppendLine(request.Prompt);

			return sb.ToString();
		}
	}
}
