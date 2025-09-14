using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Dialog
{
	internal class InputDialog
	{
		internal static async Task<string> ShowInputDialogAsync(string title, string label, string defaultValue = "")
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			string result = null;

			var dialog = new System.Windows.Window
			{
				Title = title,
				Width = 400,
				Height = 150,
				ResizeMode = System.Windows.ResizeMode.NoResize,
				WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
				Owner = System.Windows.Application.Current?.MainWindow ?? null
			};

			var stack = new System.Windows.Controls.StackPanel { Margin = new System.Windows.Thickness(10) };
			var textBlock = new System.Windows.Controls.TextBlock { Text = label, Margin = new System.Windows.Thickness(0, 0, 0, 5) };
			var textBox = new System.Windows.Controls.TextBox
			{
				Text = defaultValue,
				Margin = new System.Windows.Thickness(0, 0, 0, 10),
				AcceptsReturn = false
			};
			var button = new System.Windows.Controls.Button { Content = "OK", Width = 75, Height = 25, IsDefault = true };

			button.Click += (_, _) =>
			{
				result = textBox.Text;
				textBox.Text = string.Empty;
				dialog.DialogResult = true;
				dialog.Close();
			};

			stack.Children.Add(textBlock);
			stack.Children.Add(textBox);
			stack.Children.Add(button);

			dialog.Content = stack;
			dialog.Focus();

			textBox.SelectAll();
			textBox.Focus();

			return dialog.ShowDialog() == true ? result : null;
		}
	}
}
