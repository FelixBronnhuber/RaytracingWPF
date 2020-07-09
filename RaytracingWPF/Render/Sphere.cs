using System;
using Raytracing_WPF.Annotations;
using RaytracingWPF.Command;
using RaytracingWPF.Render;

namespace RaytracingWPF.Render
{
    public class Sphere : Primitive
    {
        public Sphere(int id, int r, int g, int b, double radius, Vector3D position) : base(id, r, g, b)
        {
            Radius = radius;
            Position = position;
            //Console.WriteLine(@"Radius: {0}    Position: {1} {2} {3}", radius, position.X1, position.X2, position.X3);
        }

        public double Radius { get; }

        public Vector3D Position { get; }

        [CanBeNull]
        private Vector3D CollisionPoint(Vector3D start, Vector3D direction)
        {
            Vector3D v = new Vector3D(Position, start);

            double discriminant = (2 * direction.GetDotProductWith(v))*(2 * direction.GetDotProductWith(v)) - 4 * direction.GetDotProductWith(direction) * (v.GetDotProductWith(v) - Radius*Radius);
            if (discriminant < 0) return null;
            //Console.WriteLine(@"Start: {0} {1} {2}    Position: {3} {4} {5}	Dis: {6}    Direction: {7} {8} {9}", start.X1, start.X2, start.X3, Position.X1, Position.X2, Position.X3, discriminant, direction.X1, direction.X2, direction.X3);

            double div = 2 * direction.GetDotProductWith(direction);
            double b = -2 * direction.GetDotProductWith(v);

            double lambda0 = (b + Math.Sqrt(discriminant)) / div;
            double lambda1 = (b - Math.Sqrt(discriminant)) / div;

            Vector3D p0 = Vector3D.GetPointForLambda(start, direction, lambda0);
            Vector3D p1 = Vector3D.GetPointForLambda(start, direction, lambda1);

            double distance0 = p0.GetLength();
            double distance1 = p1.GetLength();

            return distance0 < distance1 ? p0 : p1;
        }

        [CanBeNull]
        public override double? GetCollisionDistance(Vector3D start, Vector3D direction)
        {
            Vector3D collisionPoint = CollisionPoint(start, direction);
            if (collisionPoint == null) return null;

            Vector3D distanceVector3D = new Vector3D(collisionPoint, start);
            double? distance = distanceVector3D.GetLength();

            return distance;
        }
    }
}