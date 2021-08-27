using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Finmer.Converters
{

    /// <summary>
    /// Converts an integer value to a collection with the same number of elements, because that is how we repeat controls in MVVM. *shrug*
    /// </summary>
    public class CountToListConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null);

            var output = new List<object>();
            for (int i = 0; i < (int)value; i++)
                output.Add(new object());
            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
