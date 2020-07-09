using System.Collections.Generic;

namespace RaytracingWPF.Render
{
    public class Scene
    {
        private static int _id;
        private readonly List<Primitive> _objects;

        public Scene(int width, int height)
        {
            Width = width;
            Height = height;
            _objects = new List<Primitive>();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public void AddPrimitive(Primitive primitive)
        {
            _objects.Add(primitive);
            _id++;
        }

        public Primitive[] ConvertToArray()
        {
            return _objects.ToArray();
        }

        public int GetID()
        {
            return _id;
        }
    }
}