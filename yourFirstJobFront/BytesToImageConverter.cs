using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yourFirstJobFront
{
    public class BytesToImageConverter : IValueConverter
    {
        public BytesToImageConverter() { }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var bytes = (byte[])value;
            var streamSource = ImageSource.FromStream(() => new MemoryStream(bytes));

            return streamSource;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
