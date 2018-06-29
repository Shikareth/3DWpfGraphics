using Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Math;

namespace _3DVisualization.Models
{
    public class StewartPlatform
    {
        public Platform BasePlatform { get; set; } = new Platform();
        public Platform WorkPlatform { get; set; } = new Platform();
        public Actuator[] Actuators { get; set; } = new Actuator[6];

        public StewartPlatform(double ActuatorMinLength, double ActuatorMaxLength)
        {
            for (int n = 0; n < Actuators.Length; n++)
            {
                Actuators[n] = new Actuator()
                {
                    MinLength = ActuatorMinLength,
                    MaxLength = ActuatorMaxLength,
                    ActualLength = ActuatorMinLength
                };
            }
            UpdateActuators();
        }

        public void Move(Vector3D t)
        {
            WorkPlatform.Origin += t;
            UpdateActuators();
        }
        public void Move(Vector3D t, Matrix R)
        {
            WorkPlatform.Origin = R * WorkPlatform.Origin + t;
            WorkPlatform.X_Axis *= R;
            WorkPlatform.Normal *= R;
            UpdateActuators();
        }
        public void Rotate(Matrix R)
        {
            WorkPlatform.Origin *= R;
            WorkPlatform.X_Axis *= R;
            WorkPlatform.Normal *= R;
            UpdateActuators();
        }

        private void UpdateActuators()
        {
            for (int n = 0; n < Actuators.Length; n++)
                Actuators[n].ActualLength = (WorkPlatform.RealJoints[n] - BasePlatform.RealJoints[n]).Length;
        }
    }

    public class Actuator
    {
        public double MaxLength { get; set; } = 2;
        public double MinLength { get; set; } = 1;
        public double ActualLength { get; set;}
    }

    public class Platform
    {
        public double Radius
        {
            get { return radius; }
            set { radius = value; InitJoints(angleOffset); }
        }
        private double radius = 1.5;

        private double angleOffset = 0;

        public Vector3D Origin
        {
            get { return origin; }
            set { origin = value; InitRealJoints(); }
        }
        private Vector3D origin = new Vector3D(0, 0, 0);

        public Vector3D X_Axis
        {
            get { return x_axis; }
            set { x_axis = value; InitRealJoints(); }
        }
        private Vector3D x_axis = new Vector3D(1, 0, 0);

        public Vector3D Y_Axis
        {
            get { return Vector3D.CrossProduct(Normal, X_Axis); }
        }

        public Vector3D Normal
        {
            get { return normal; }
            set { normal = value; InitRealJoints(); }
        }
        private Vector3D normal = new Vector3D(0, 0, 1);

        //TODO add platform rotation axis and rotation angle property OR make converter [Rotation Matrix] -> [Axis, angle]

        public Vector3D[] Joints { get; set; } = new Vector3D[6];
        public Vector3D[] RealJoints { get; set; } = new Vector3D[6];

        /// <summary>
        /// Initializes platform. Positions of the joints are based on circle radius splitted into 120 degrees parts. 
        /// Then joints are evenly moved from radius end point in polar coordinates system starting in circle center over "p" degrees clockwise and counterclockwise.
        /// </summary>
        /// <param name="p">Radial offsed in degrees</param>
        public Platform(double p = 0)
        {
            angleOffset = p;
            InitJoints(p);
        }
        
        public void InitJoints(double p)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2D c;

                c = Misc.PolarToCartesian(Radius, Misc.DegToRad(i * 120 + p));

                Joints[i * 2] = new Vector3D(c.X, c.Y, 0.0);

                c = Misc.PolarToCartesian(Radius, Misc.DegToRad(i * 120 - p));

                Joints[i * 2 + 1] = new Vector3D(c.X, c.Y, 0.0);
            }

            InitRealJoints();
        }

        private void InitRealJoints()
        {
            var ix = X_Axis.Normalized();
            var iz = Normal.Normalized();
            var iy = Vector3D.CrossProduct(iz, ix).Normalized();

            Matrix R = new Matrix(ix, iy, iz);

            Vector3D[] array = new Vector3D[Joints.Length];

            for (int n = 0; n < array.Length; n++)
                array[n] = Joints[n] * R + Origin;

            RealJoints = array;
        }

    }
}
