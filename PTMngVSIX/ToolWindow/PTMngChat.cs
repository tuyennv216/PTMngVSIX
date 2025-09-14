using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace PTMngVSIX.ToolWindow
{
	/// <summary>
	/// This class implements the tool window exposed by this package and hosts a user control.
	/// </summary>
	/// <remarks>
	/// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
	/// usually implemented by the package implementer.
	/// <para>
	/// This class derives from the ToolWindowPane class provided from the MPF in order to use its
	/// implementation of the IVsUIElementPane interface.
	/// </para>
	/// </remarks>
	[Guid("d41dd891-0f5c-4a59-8eb8-959feb104060")]
	public class PTMngChat : ToolWindowPane
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PTMngChat"/> class.
		/// </summary>
		public PTMngChat() : base(null)
		{
			this.Caption = "PTMng Chat";

			// This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
			// we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
			// the object returned by the Content property.
			this.Content = new PTMngChatControl();
		}
	}
}
