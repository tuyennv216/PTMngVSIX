using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Dialog
{
	internal class TextDialog
	{
		internal static async Task ShowTextDialogAsync(string title, string text)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dialog = new System.Windows.Window
			{
				Title = title,
				Width = 800,
				Height = 500,
				ResizeMode = System.Windows.ResizeMode.NoResize,
				WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
				//Owner = System.Windows.Application.Current?.MainWindow
				Owner = null,
				Topmost = true,
			};

			// Grid layout
			var grid = new System.Windows.Controls.Grid();
			grid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
			grid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition { Height = System.Windows.GridLength.Auto });

			// TextBox
			var textBox = new System.Windows.Controls.TextBox
			{
				Text = text,
				AcceptsReturn = true,
				TextWrapping = System.Windows.TextWrapping.Wrap,
				IsReadOnly = true,
				Margin = new System.Windows.Thickness(10),
				VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
				HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto
			};
			System.Windows.Controls.Grid.SetRow(textBox, 0);
			grid.Children.Add(textBox);

			// Button panel
			var buttonPanel = new System.Windows.Controls.StackPanel
			{
				Orientation = System.Windows.Controls.Orientation.Horizontal,
				HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
				Margin = new System.Windows.Thickness(10)
			};

			var buttonCopy = new System.Windows.Controls.Button
			{
				Content = "Copy",
				Width = 75,
				Height = 25,
				Margin = new System.Windows.Thickness(0, 0, 10, 0)
			};

			var buttonOk = new System.Windows.Controls.Button
			{
				Content = "OK",
				Width = 75,
				Height = 25,
				IsDefault = true
			};

			buttonPanel.Children.Add(buttonCopy);
			buttonPanel.Children.Add(buttonOk);
			System.Windows.Controls.Grid.SetRow(buttonPanel, 1);
			grid.Children.Add(buttonPanel);

			// Events
			buttonCopy.Click += (_, _) => System.Windows.Clipboard.SetText(text);
			buttonOk.Click += (_, _) => dialog.Close();

			dialog.Content = grid;
			dialog.Show();
		}

	}
}
