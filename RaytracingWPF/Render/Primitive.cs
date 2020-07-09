using System;
using Raytracing_WPF.Annotations;
using RaytracingWPF.Command;

namespace RaytracingWPF.Render
{
    public class Primitive
    {
        private readonly byte[] _color;
        private readonly int _id;

        public Primitive(int id, int r, int g, int b)
        {
            _id = id;
            _color = new[] {Convert.ToByte(b), Convert.ToByte(g), Convert.ToByte(r), byte.MaxValue};
        }

        public int GetID()
        {
            return _id;
        }

        [CanBeNull]
        public virtual double? GetCollisionDistance(Vector3D start, Vector3D direction)
        {
            return null;
        }

        public byte[] GetColor()
        {
            return _color;
        }
    }
}