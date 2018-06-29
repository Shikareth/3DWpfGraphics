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

            Matrix R = Matrix.RotationMatrixXZX(Misc.DegToRad(90), 0, 0);

            var LU = R.LU_Doolittle();

            Console.WriteLine(R.ToString("N3"));
            Console.WriteLine(LU[0].ToString("N3"));
            Console.WriteLine(LU[1].ToString("N3"));

            Console.ReadKey();
        }
    }
}
