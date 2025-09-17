using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.ToolWindow.Forms;
using PTMngVSIX.Utils.Chat;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PTMngVSIX.ToolWindow
{
	/// <summary>
	/// Interaction logic for PTMngChatControl.
	/// </summary>
	public partial class PTMngChatControl : System.Windows.Controls.UserControl
	{
		private Option Option { get; set; } = new();
		private FontFamily DefaultFontFamily;
		private readonly object _documentLock = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="PTMngChatControl"/> class.
		/// </summary>
		public PTMngChatControl()
		{
			this.InitializeComponent();

			try
			{
				DefaultFontFamily = new FontFamily("Cascadia Mono");
			}
			catch
			{
				DefaultFontFamily = new FontFamily("Consolas");
			}

			// Đăng ký sự kiện cho button
			ResetButton.Click += ResetButton_Click;
			SettingButton.Click += SettingButton_Click;

			// Đăng ký sự kiện cho TextBox (ví dụ: Enter key)
			InputChat.KeyDown += InputChat_KeyDown;

			ChatDisplay.Document.Blocks.Clear();
			CodeSnippetDisplay.Document.Blocks.Clear();

			// Tải danh sách code snippet đang có
			foreach (var snippet in ChatService.Instance.ActiveSnippets)
			{
				AddCodeSnippet(snippet);
			}
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			_ = ChatService.Instance.ResetChatAsync();
		}

		private void SettingButton_Click(object sender, RoutedEventArgs e)
		{
			var optionForm = new OptionForm();
			if (optionForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				var setting = optionForm.Option;
				ActiveDocumentCheckbox.IsChecked |= setting.IncludeActiveDocument;

				this.Option = setting;
			}
		}

		private void Chat_Copy_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(ChatDisplay.Selection.Text);
		}

		private void Chat_AddCodeSnippet_Click(object sender, RoutedEventArgs e)
		{
			string selectedText = ChatDisplay.Selection.Text;
			_ = ChatService.Instance.AddCodeSnippetAsync(selectedText);
		}

		private void InputChat_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Enter && Keyboard.Modifiers != ModifierKeys.Shift)
			{
				e.Handled = true;
				_ = SendMessageAsync();
			}
		}

		private void SendButton_Click(object sender, RoutedEventArgs e)
		{
			_ = SendMessageAsync();
		}

		private async Task SendMessageAsync()
		{
			string prompt = InputChat.Text.Trim().Trim('\r', '\n', ' ');
			InputChat.Clear();

			if (!string.IsNullOrEmpty(prompt))
			{
				var message = new Utils.Chat.Message
				{
					Task = Data.Constant.TaskName.TaskF6.Chat,
					Prompt = prompt,
					Option = Option.Commit()
				};
				message.Option.IncludeActiveDocument = ActiveDocumentCheckbox.IsChecked ?? false;

				var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

				AddActionResult(message, response);
			}
		}

		public void AddActionResult(Message message, ResponseBase response)
		{
			var userParagraph = new Paragraph
			{
				FontFamily = DefaultFontFamily,
				Foreground = Brushes.SeaGreen,
				BorderThickness = new Thickness(0, 1, 0, 0),
				BorderBrush = Brushes.Black,
				Padding = new Thickness(0, 5, 0, 0)
			};
			userParagraph.Inlines.Add(new Run(message.Prompt));

			var aiParagraph = new Paragraph
			{
				FontFamily = DefaultFontFamily,
				Foreground = Brushes.Black,
				Padding = new Thickness(0, 0, 0, 5)
			};
			aiParagraph.Inlines.Add(new Run(response.Answer));

			lock (_documentLock)
			{
				ChatDisplay.Document.Blocks.Add(userParagraph);
				ChatDisplay.Document.Blocks.Add(aiParagraph);
			}
			ChatDisplay.ScrollToEnd();
		}

		public void AddCodeSnippet(string code)
		{
			var paragraph = new Paragraph()
			{
				FontFamily = DefaultFontFamily,
				Cursor = System.Windows.Input.Cursors.Hand,
				FontSize = 12,
				Margin = new Thickness(0),
				Tag = code,
			};

			if (CodeSnippetDisplay.Document.Blocks.Count > 0)
			{
				paragraph.Inlines.Add(new Run("---\n"));
			}
			var codeBlock = new Run(code);

			paragraph.Inlines.Add(codeBlock);
			paragraph.MouseLeftButtonDown += Paragraph_MouseLeftButtonDown;

			CodeSnippetDisplay.Document.Blocks.Add(paragraph);
			CodeSnippetDisplay.ScrollToEnd();
		}

		public void ResetChatText()
		{
			ChatDisplay.Document.Blocks.Clear();
		}

		public void ResetCodeSnippet()
		{
			CodeSnippetDisplay.Document.Blocks.Clear();
		}

		private void Paragraph_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is Paragraph paragraph)
			{
				CodeSnippetDisplay.Document.Blocks.Remove(paragraph);
			}
		}
	}
}