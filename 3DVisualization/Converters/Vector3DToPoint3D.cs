using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _3DVisualization.Converters
{
    [ValueConversion(typeof(Optinav.Mathlib.Vector3D), typeof(System.Windows.Media.Media3D.Point3D))]
    public class Vector3DToPoint3D : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Media3D.Point3D result = new System.Windows.Media.Media3D.Point3D();

            if(value != null)
                if(value.GetType() == typeof(Optinav.Mathlib.Vector3D))
                {
                    var v = (Optinav.Mathlib.Vector3D)value;
                    result = new System.Windows.Media.Media3D.Point3D(v.x, v.y, v.z);
                }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
