using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Dialog
{
	internal class MsgboxDialog
	{
		internal static async Task ShowMessageAsync(string title, string message)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

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

			var textBlock = new System.Windows.Controls.TextBlock
			{
				Text = message,
				TextWrapping = System.Windows.TextWrapping.Wrap,
				Margin = new System.Windows.Thickness(0, 0, 0, 10)
			};

			var button = new System.Windows.Controls.Button
			{
				Content = "OK",
				Width = 75,
				Height = 25,
				IsDefault = true,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Right
			};

			button.Click += (_, _) =>
			{
				dialog.DialogResult = true;
				dialog.Close();
			};

			stack.Children.Add(textBlock);
			stack.Children.Add(button);

			dialog.Content = stack;
			dialog.Focus();

			dialog.ShowDialog();
		}
	}
}
