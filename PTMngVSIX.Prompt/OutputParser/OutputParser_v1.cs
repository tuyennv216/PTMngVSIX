using PTMngVSIX.Prompt.OutputModel;

namespace PTMngVSIX.Prompt.OutputParser
{
	public class OutputParser_v1
	{
		public static ModelReturn_v1 Parser(string content)
		{
			var result = new ModelReturn_v1();

			var removeOutlined = content.Trim().Trim(' ', '`', '\r', '\n');

			if (removeOutlined.Length > 0 && removeOutlined[0] == '[')
			{
				var endTypeIndex = removeOutlined.IndexOf(']');
				if (endTypeIndex >= 0)
				{
					result.Type = removeOutlined.Substring(1, endTypeIndex - 1);
					result.Text = removeOutlined.Substring(endTypeIndex + 1)
						.Trim().Trim(' ', '`', '\r', '\n');
				}
			}
			else
			{
				result.Type = "raw";
				result.Text = removeOutlined;
			}

			return result;
		}
	}
}
