using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {

            Tools.Math.Matrix R = new Tools.Math.Matrix(90.0, 90.0, 90.0);

            double det = R.Det();


            Console.WriteLine($"{R}");
            Console.WriteLine($"{R.Transposed()}");

            Console.ReadKey();
        }
    }
}
