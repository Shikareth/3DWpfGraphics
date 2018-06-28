using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Math;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {

            Matrix R = new Matrix(90.0, 0.0, 0.0);
            Matrix A = R.Transposed();

            Vector3D v = new Vector3D(0, 1, 0);

            double det = R.Det();


            Console.WriteLine(R.ToString("N3"));
            Console.WriteLine(A.ToString("N3"));

            var B = R * A;

            Console.WriteLine(B.ToString("N3"));

            var p = v * R;

            Console.WriteLine(p.ToString("N3"));

            Console.ReadKey();
        }
    }
}
