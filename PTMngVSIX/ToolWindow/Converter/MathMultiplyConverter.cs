using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PTMngVSIX.ToolWindow.Converter
{
	internal class MathMultiplyConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length >= 2 && values[0] is double value1 && values[1] is double value2)
			{
				return value1 * value2;
			}
			return DependencyProperty.UnsetValue;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
