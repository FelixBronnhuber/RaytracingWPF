using System;

namespace RaytracingWPF.Command
{
    public class Vector3D
    {
        public Vector3D(double x1, double x2, double x3)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public Vector3D(Vector3D a, Vector3D b)
        {
            X1 = a.X1 - b.X1;
            X2 = a.X2 - b.X2;
            X3 = a.X3 - b.X3;
        }

        public double X1 { get; set; }

        public double X2 { get; set; }

        public double X3 { get; set; }

        public double GetDotProductWith(Vector3D vector3D)
        {
            return X1 * vector3D.X1 + X2 * vector3D.X2 + X3 * vector3D.X3;
        }

        public double GetLength()
        {
            return Math.Sqrt(X1 * X1 + X2 * X2 + X3 * X3);
        }

        public Vector3D GetUnitVector()
        {
            double length = GetLength();
            return new Vector3D(X1 / length, X2 / length, X3 / length);
        }

        public static Vector3D GetPointForLambda(Vector3D start, Vector3D direction, double lambda)
        {
            double x1 = start.X1 + direction.X1 * lambda;
            double x2 = start.X2 + direction.X2 * lambda;
            double x3 = start.X3 + direction.X3 * lambda;

            return new Vector3D(x1, x2, x3);
        }
    }
}