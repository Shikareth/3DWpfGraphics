using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Math
{
    public class Matrix
    {
        public double[] Data { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Matrix(int rows, int columns, double value)
        {
            Data = new double[rows*columns];
            Rows = rows;
            Columns = columns;
            Parallel.For(0, Data.Length, (n) =>
            {
                Data[n] = value;
            });
        }

        public Matrix(int rows, int columns, IEnumerable<double> data)
        {
            Data = new double[rows * columns];
            Rows = rows;
            Columns = columns;
            var array = data.ToArray();
            Parallel.For(0, Data.Length, (n) =>
            {
                Data[n] = array[n];
            });
        }

        public Matrix(Vector3D ix, Vector3D iy, Vector3D iz)
        {
            Data = new double[9];
            Rows = 3;
            Columns = 3;

            Data[0] = ix.X;
            Data[1] = ix.Y;
            Data[2] = ix.Z;

            Data[3] = iy.X;
            Data[4] = iy.Y;
            Data[5] = iy.Z;

            Data[6] = iz.X;
            Data[7] = iz.Y;
            Data[8] = iz.Z;
        }

        public Matrix(double ax, double ay, double az)
        {
            Data = new double[9];
            Rows = 3;
            Columns = 3;

            var cx = System.Math.Cos(ax);
            var sx = System.Math.Sin(ax);
            var cy = System.Math.Cos(ay);
            var sy = System.Math.Sin(ay);
            var cz = System.Math.Cos(az);
            var sz = System.Math.Sin(az);

            Data[0] = cy * cz;
            Data[1] = -cy * sz;
            Data[2] = sy;

            Data[3] = cx * sz + cz * sx * sy;
            Data[4] = cx * cz - sx * sy * sz;
            Data[5] = -cy * sx;

            Data[6] = sx * sz - cx * cz * sy;
            Data[7] = cz * sx + cx * sy * sz;
            Data[8] = cx * cy;
        }

        public Matrix(Vector3D ix, Vector3D iy)
        {
            Data = new double[4];
            Rows = 2;
            Columns = 2;

            Data[0] = ix.X;
            Data[1] = ix.Y;

            Data[2] = iy.X;
            Data[3] = iy.Y;
        }

        public Matrix(double angle)
        {
            Data = new double[4];
            Rows = 2;
            Columns = 2;

            Data[0] = System.Math.Cos(angle);
            Data[1] = -System.Math.Sin(angle);

            Data[2] = System.Math.Cos(angle);
            Data[3] = System.Math.Sin(angle);
        }

        public Matrix(Vector3D axis, double angle)
        {
            Data = new double[9];
            Rows = 3;
            Columns = 3;

            var A = System.Math.Cos(angle);
            var B = 1 - System.Math.Cos(angle);
            var C = System.Math.Sin(angle);

            Data[0] = A + System.Math.Pow(axis.X, 2) * B;
            Data[1] = axis.X * axis.Y * B - axis.Z * C;
            Data[2] = axis.X * axis.Z * B + axis.Y * C;

            Data[3] = axis.Y * axis.X * B + axis.Z * C;
            Data[4] = A + System.Math.Pow(axis.Y, 2) * B;
            Data[5] = axis.Y * axis.Z * B - axis.X * C;

            Data[6] = axis.Z * axis.X * B - axis.Y * C;
            Data[7] = axis.Z * axis.Y * B + axis.X * C;
            Data[8] = A + System.Math.Pow(axis.Z, 2) * B;

        }

        public Matrix Transposed()
        {
            double[] array = new double[Data.Length];
            Parallel.For(0, Data.Length, (n) =>
            {
                int row = n / Columns;
                int col = n % Columns;

                array[n] = Data[col * Rows + row];
            });

            return new Matrix(Columns, Rows, array);
        }
        public void Transpose()
        {
            double[] array = new double[Data.Length];
            Parallel.For(0, Data.Length, (n) =>
            {
                int row = n / Columns;
                int col = n % Columns;

                array[n] = Data[col * Rows + row];
            });

            Data = array;
            var columns = Columns;
            Columns = Rows;
            Rows = Columns;
        }

        public Matrix Minor(int row, int column)
        {
            double[] array = new double[(Rows - 1) * (Columns - 1)];

            Parallel.For(0, array.Length, (n) =>
            {
                int r = n / (Columns - 1);
                int c = n % (Columns - 1);

                if (r >= row) r++;
                if (c >= column) c++;

                array[n] = Data[r * Columns + c];
            });

            return new Matrix(Rows-1, Columns-1, array);
        }

        public double Det()
        {
            if (Rows != Columns || Rows < 2) return double.NaN;
            int size = Columns;

            double det = 0;

            switch (size)
            {
                case 2:
                    det = Data[0] * Data[3] - Data[1] * Data[2];
                    break;

                case 3:
                    det = Data[0] * Data[4] * Data[8] +
                        Data[1] * Data[5] * Data[6] +
                        Data[2] * Data[3] * Data[7] -
                        Data[6] * Data[4] * Data[2] -
                        Data[7] * Data[5] * Data[0] -
                        Data[8] * Data[3] * Data[1];
                    break;
                default:
                    Parallel.For(0, Columns, (n) =>
                    {
                        int r = n / Columns;
                        int c = n % Columns;





                    });
                    det = double.NaN;
                    break;
            }

            return det;
        }


        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns) return null;

            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A.Data[n] + B.Data[n];
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator +(Matrix A, double B)
        {
            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A.Data[n] + B;
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator +(double A, Matrix B)
        {
            double[] array = new double[B.Rows * B.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A + B.Data[n];
            });

            return new Matrix(B.Rows, B.Columns, array);
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns) return null;

            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A.Data[n] - B.Data[n];
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator -(Matrix A, double B)
        {
            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A.Data[n] - B;
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator -(double A, Matrix B)
        {
            double[] array = new double[B.Rows * B.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A - B.Data[n];
            });

            return new Matrix(B.Rows, B.Columns, array);
        }

        //TODO Matrix multiplication
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Rows != B.Rows || A.Columns != B.Columns) return null;

            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                int row = n / A.Columns;
                int col = n % A.Columns;


                array[n] = A.Data[n] * B.Data[n];
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator *(Matrix A, double B)
        {
            double[] array = new double[A.Rows * A.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A.Data[n] * B;
            });

            return new Matrix(A.Rows, A.Columns, array);
        }
        public static Matrix operator *(double A, Matrix B)
        {
            double[] array = new double[B.Rows * B.Columns];

            Parallel.For(0, array.Length, (n) =>
            {
                array[n] = A * B.Data[n];
            });

            return new Matrix(B.Rows, B.Columns, array);
        }




        public override string ToString()
        {
            string matrix = string.Empty;

            for (int y = 0; y < Rows; y++)
            {
                matrix += "|\t";

                for (int x = 0; x < Columns; x++)
                    matrix += $"{Data[y * Columns + x]}\t";

                matrix += "|\n";
            }

            return matrix;
        }

    }
}
