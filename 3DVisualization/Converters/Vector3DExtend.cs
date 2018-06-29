using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _3DVisualization.Converters
{
    class Vector3DExtend : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Media3D.Point3D result = new System.Windows.Media.Media3D.Point3D();

            if(values != null)
                if(values.Count() == 3)
                {
                    if (values[0] == null || values[1] == null || !double.TryParse(values[2].ToString(), out double scale)) return result;
                    Tools.Math.Vector3D origin = (Tools.Math.Vector3D)values[0];
                    Tools.Math.Vector3D direction = (Tools.Math.Vector3D)values[1];

                    var t = (direction - origin) * scale + origin;
                    result = new System.Windows.Media.Media3D.Point3D(t.X, t.Y, t.Z);
                }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
