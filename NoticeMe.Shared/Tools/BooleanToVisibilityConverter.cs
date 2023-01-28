using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace NoticeMe.Tools
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public BoolToVisibilityConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;

            if (value is bool && (bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is Visibility && (Visibility)value == Visibility.Visible);
        }
    }
}