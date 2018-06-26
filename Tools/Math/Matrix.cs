using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Math
{
    public class Matrix
    {
        public double[,] Data { get; set; }
        public uint Rows { get; set; }
        public uint Columns { get; set; }

        public Matrix(uint rows, uint columns, double value)
        {
            Data = new double[rows, columns];
            Parallel.For(0, rows, (n) =>
            {
                for (int m = 0; m < columns; m++)
                    Data[n, m] = value;
            });
        }

        public Matrix (uint rows, uint columns, IEnumerable<double> data)
        {
            Data = new double[rows, columns];
            var array = data.ToArray();
            Parallel.For(0, rows, (n) =>
            {
                for (int m = 0; m < columns; m++)
                    Data[n, m] = array[n*columns + m];
            });
        }

        public Matrix(Vector3D ix, Vector3D iy, Vector3D iz)
        {
            Data = new double[3, 3];

            Data[0, 0] = ix.X;
            Data[0, 1] = ix.Y;
            Data[0, 2] = ix.Z;

            Data[1, 0] = iy.X;
            Data[1, 1] = iy.Y;
            Data[1, 2] = iy.Z;

            Data[2, 0] = iz.X;
            Data[2, 1] = iz.Y;
            Data[2, 2] = iz.Z;
        }

        public Matrix(double ax, double ay, double az)
        {

        }

        public Matrix(Vector3D ix, Vector3D iy)
        {
            Data = new double[2, 2];

            Data[0, 0] = ix.X;
            Data[0, 1] = ix.Y;

            Data[1, 0] = iy.X;
            Data[1, 1] = iy.Y;
        }

        public Matrix(double angle)
        {
            Data = new double[2, 2];

            Data[0, 0] = System.Math.Cos(angle);
            Data[0, 1] = -System.Math.Sin(angle);
            Data[1, 0] = System.Math.Cos(angle);
            Data[1, 1] = System.Math.Sin(angle);
        }

        public Matrix(Vector3D axis, double angle)
        {

        }
    }
}
