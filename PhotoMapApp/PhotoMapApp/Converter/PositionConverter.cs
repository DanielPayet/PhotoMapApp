using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PhotoMapApp.Converter
{
    public class PositionConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Position position = (Position) value;
            if(position.Latitude == 0 && position.Longitude == 0) {
                return "Position inconnue";
            }
            return position.Latitude + " - " + position.Longitude;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
