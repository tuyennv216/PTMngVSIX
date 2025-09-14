using Microsoft.VisualStudio.Text;
using PTMngVSIX.Utils.Model;
using System.Collections.Generic;

namespace PTMngVSIX.Utils.Chat
{
	public class Message
	{
		public string Answer { get; set; }

		public string Task { get; set; }
		public string Prompt { get; set; }
		public ErrorViewModel Error { get; set; }
		public List<string> Snippets { get; set; } = new List<string>();

		public Option Option { get; set; } = new Option();
		public ITrackingPoint TrackingPoint { get; set; }

		public override string ToString()
		{
			return $"Task: {Task}, Prompt: {Prompt}";
		}
	}
}
