using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CybersecurityChatbotWPF.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color UserColor { get; set; } = Color.FromRgb(76, 175, 80);
        public Color BotColor { get; set; } = Color.FromRgb(33, 150, 243);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isUser && isUser)
                return new SolidColorBrush(UserColor);
            return new SolidColorBrush(BotColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}