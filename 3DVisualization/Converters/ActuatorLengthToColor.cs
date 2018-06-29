using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _3DVisualization.Converters
{
    public class ActuatorLengthToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.SolidColorBrush color = System.Windows.Media.Brushes.LawnGreen;

            if(value != null)
                if(value.GetType() == typeof(Models.Actuator))
                {
                    Models.Actuator A = (Models.Actuator)value;

                    if (A.ActualLength >= A.MaxLength) color = System.Windows.Media.Brushes.Red;
                    if (A.ActualLength <= A.MinLength) color = System.Windows.Media.Brushes.Blue;
                }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
