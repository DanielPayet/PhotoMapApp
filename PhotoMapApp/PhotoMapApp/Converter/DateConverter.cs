using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Converter
{
    public class DateConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            double totalDays = Math.Floor((DateTime.Now - date).TotalDays);
            if (totalDays < 1) {
                return "Today";
            } else if (totalDays < 2) {
                return "Yesterday";
            } else if (totalDays < 7) {
                return $"{totalDays} day{ (totalDays >= 2 ? "s": "")}";
            } else if (totalDays < 30) {
                double nbWeek = Math.Floor(totalDays / 7);
                return $"{nbWeek} week{ ( nbWeek >= 2 ? "s" : "" )}";
            } else if (totalDays < 365 ) {
                double nbMonth = Math.Floor(totalDays / 30);
                return $"{nbMonth} month{ ( nbMonth >= 2 ? "s" : "" )}";
            } else if (totalDays >= 365) {
                double nbYear = Math.Floor(totalDays / 365);
                return $"{nbYear} year{ ( nbYear >= 2 ? "s" : "" )}";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
