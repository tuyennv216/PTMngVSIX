using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Setting;
using System.Text;

namespace PTMngVSIX.Prompt.Builder
{
	public class UserPromptBuilder
	{
		public static string Build(RequestBase request)
		{
			var sb = new StringBuilder();

			if (!string.IsNullOrEmpty(ModelSetting.Enviroment))
			{
				sb.AppendLine("Here is the development environment:");
				sb.AppendLine("```");
				sb.AppendLine(ModelSetting.Enviroment);
				sb.AppendLine("```");
			}

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
