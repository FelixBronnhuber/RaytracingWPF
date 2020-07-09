using System;
using RaytracingWPF.Command;

namespace RaytracingWPF.Render
{
    public class RenderMain
    {
        private readonly RenderCommand _renderCommand;

        public RenderMain(RenderCommand renderCommand, string input)
        {
            _renderCommand = renderCommand;
            Parser parser = new Parser(input);
            Scene scene = parser.ToScene();

            byte[] bytes = RenderScene(scene);
            _renderCommand.SaveToBmp(scene.Width, scene.Height, bytes);
        }

        private byte[] RenderScene(Scene scene)
        {
            byte[] pixelData = new byte[scene.Width * scene.Height * 4];
            Primitive[] primitives = scene.ConvertToArray();
            double width = Convert.ToDouble(scene.Width);
            double height = Convert.ToDouble(scene.Height);
            
            double d = -(Convert.ToDouble(scene.Width) / 2 / Math.Tan(Math.PI / 4));
            Console.WriteLine(d);
            Vector3D start = new Vector3D(d, 0, 0);
            Vector3D direction;

            //int n = 0;
            for (int x = 0; x < scene.Width; x++)
            for (int y = 0; y < scene.Height; y++)
            {
                direction = new Vector3D(-d, Convert.ToDouble(x) - Convert.ToDouble(width / 2), Convert.ToDouble(y) - Convert.ToDouble(height / 2));
                byte[] color = GetColor(primitives, direction, start);
                if (x == 7 && y == 7)
                {
                    color = new[] {Byte.MaxValue, Byte.MaxValue, Byte.MaxValue, Byte.MaxValue};
                }

                pixelData[(y * scene.Width + x) * 4] = color[0];
                pixelData[(y * scene.Width + x) * 4 + 1] = color[1];
                pixelData[(y * scene.Width + x) * 4 + 2] = color[2];
                pixelData[(y * scene.Width + x) * 4 + 3] = color[3];
            }

            return pixelData;
        }

        private static byte[] GetColor(Primitive[] primitives, Vector3D direction, Vector3D start)
        {
            if (primitives == null) throw new ArgumentNullException(nameof(primitives));
            // Color Format B G R A
            byte[] color = new byte[4];
            double minDistance = double.MaxValue;
            double? distance;

            foreach (Primitive primitive in primitives)
            {
                distance = primitive.GetCollisionDistance(start, direction);

                if (distance != null && (distance < minDistance))
                {
                    minDistance = distance.Value;
                    color = primitive.GetColor();
                }
            }

            return color;
        }
    }
}