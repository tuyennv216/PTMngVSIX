using PTMngVSIX.Setting;
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

				if (ModelSetting.TranslateOutput)
				{
					sb.AppendLine($"Based on this context, please complete the requested action and respond in {ModelSetting.OutputLanguage}: ");
				}
				else
				{
					sb.AppendLine("Based on this context, please complete the requested action: ");
				}
			}
			else
			{
				sb.AppendLine($"Please complete the requested action and respond in {ModelSetting.OutputLanguage}: ");
			}

			sb.AppendLine(request.Prompt);

			return sb.ToString();
		}
	}
}
