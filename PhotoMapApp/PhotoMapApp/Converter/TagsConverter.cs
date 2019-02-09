using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PhotoMapApp.Converter
{
    public class TagsConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<String> tags = ( (List<Tag>) value ).ConvertAll((tag) => tag.Name);
            return String.Join(", ", tags.ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
