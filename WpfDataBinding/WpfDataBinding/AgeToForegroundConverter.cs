// -----------------------------------------------------------------------
// <copyright file="AgeToForegroundConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfDataBinding
{
    public class AgeToForegroundConverter : IValueConverter
    {
        // Called when converting the Age to a Foreground brush
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Only convert to brushes...
            if (targetType != typeof(Brush))
            {
                return null;
            }

            int age = int.Parse(value.ToString());
            return (age > 25 ? Brushes.Red : Brushes.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
