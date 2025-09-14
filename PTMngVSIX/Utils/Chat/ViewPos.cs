using Microsoft.VisualStudio.Text;

namespace PTMngVSIX.Utils.Chat
{
	internal class ViewPos
	{
		public ITextSnapshot TextSnapshot { get; set; }
		public int Position { get; set; }
	}
}
