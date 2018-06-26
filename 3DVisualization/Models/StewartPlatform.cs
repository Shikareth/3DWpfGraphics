using Optinav.Mathlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DVisualization.Models
{
    public class StewartPlatform
    {
        #region Constraints



        #endregion

        public Platform BasePlatform { get; set; } = new Platform();
        public Platform WorkPlatform { get; set; } = new Platform();

        public Actuator A1 { get; set; }
        public Actuator A2 { get; set; }
        public Actuator A3 { get; set; }
        public Actuator A4 { get; set; }
        public Actuator A5 { get; set; }
        public Actuator A6 { get; set; }

        public StewartPlatform()
        {

        }
    }

    public class Actuator
    {
        public double MaxLength { get; set; } = 1;
        public double MinLength { get; set; } = 0.5;
        public double ActualLength { get; set; }
    }

    public class Platform
    {
        public double Radius { get; set; } = 1.5;

        public Vector3D Origin { get; set; } = new Vector3D(0, 0, 0);
        public Vector3D Rotation { get; set; } = new Vector3D(1, 0, 0);
        public Vector3D Normal { get; set; } = new Vector3D(0, 0, 1);

        //TODO add platform rotation axis and rotation angle property

        public Vector3D[] Joints { get; set; } = new Vector3D[6];

        private Vector3D[] realJoints = new Vector3D[6];
        public Vector3D[] RealJoints
        {
            get {
                CalculateRealJoints();
                return realJoints;
            }
        } 

        public void InitJoints(double p = 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2D c;

                c = Tools.PolarToCartesian(Radius, Tools.DegToRad(i * 120 + p));

                Joints[i * 2] = new Vector3D(c.x, c.y, 0.0);

                c = Tools.PolarToCartesian(Radius, Tools.DegToRad(i * 120 - p));

                Joints[i * 2 + 1] = new Vector3D(c.x, c.y, 0.0);
            }
        }

        private void CalculateRealJoints()
        {
            for (int i = 0; i < 6; i++)
            {
                var ix = Rotation;
                var iz = Normal;
                var iy = iz.CrossProduct(ix);

                ix.Normalize();
                iy.Normalize();
                iz.Normalize();


                realJoints[i] = Joints[i];

            }
        }
    }

    public static class Tools
    {
        public static Vector2D PolarToCartesian(Vector2D v)
        {
            return new Vector2D()
            {
                x = v.x * Math.Cos(v.y),
                y = v.x * Math.Sin(v.y)
            };
        }
        public static Vector2D PolarToCartesian(double R, double A)
        {
            return new Vector2D()
            {
                x = R * Math.Cos(A),
                y = R * Math.Sin(A)
            };
        }

        public static Vector2D CartesianToPolar(Vector2D v)
        {
            return new Vector2D()
            {
                x = Math.Sqrt(Math.Pow(v.x,2) + Math.Pow(v.y, 2)),
                y = Math.Atan2(v.y, v.x)
            };
        }
        public static Vector2D CartesianToPolar(double X, double Y)
        {
            return new Vector2D()
            {
                x = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)),
                y = Math.Atan2(Y, X)
            };
        }

        public static double DegToRad(double value)
        {
            return value / 180 * Math.PI;
        }
        public static double RadToDeg(double value)
        {
            return 180 * value / Math.PI;
        }

    }
}
