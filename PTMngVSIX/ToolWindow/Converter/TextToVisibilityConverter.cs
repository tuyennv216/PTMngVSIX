using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace PTMngVSIX.ToolWindow.Converter
{
	public class TextToVisibilityConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string text)
			{
				return string.IsNullOrEmpty(text) ? Visibility.Collapsed : Visibility.Visible;
			}

			if (value is System.Windows.Documents.FlowDocument document)
			{
				return string.IsNullOrEmpty(new TextRange(document.ContentStart, document.ContentEnd).Text)
					? Visibility.Collapsed : Visibility.Visible;
			}

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
