using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _3DVisualization.Converters
{
    [ValueConversion(typeof(Models.Platform), typeof(System.Windows.Media.Media3D.Point3D))]
    public class WorkPlatformJointTransformation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Media3D.Point3D result = new System.Windows.Media.Media3D.Point3D(0,0,0);

            if (value != null)
                if(value.GetType() == typeof(Models.Platform))
                {
                    var platform = (Models.Platform)value;



                }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
