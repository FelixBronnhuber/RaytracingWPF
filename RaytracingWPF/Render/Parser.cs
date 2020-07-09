using System;
using Raytracing_WPF.Annotations;
using RaytracingWPF.Render;
using RaytracingWPF.Command;
using static System.String;

namespace RaytracingWPF.Render
{
    public class Parser
    {
        [NotNull] private readonly string[] _keywords;

        public Parser(string input)
        {
            //Removes all whitespace from input
            input = input.Replace(" ", Empty);
            input = input.ToUpper();
            char[] splitChars = {'\n', ','};
            _keywords = input.Split(splitChars);
        }

        public Scene ToScene()
        {
            try
            {
                int width = Convert.ToInt32(_keywords[0]);
                int height = Convert.ToInt32(_keywords[1]);
                Scene scene = new Scene(width, height);

                //split into lines first then separate by ',' FUTURE PROVING

                for (int i = 2; i < _keywords.Length; i += 8)
                {
                    int line = 999;
                    try
                    {
                        double radius = Convert.ToDouble(_keywords[i + 1]);
                        Vector3D position = new Vector3D(
                            Convert.ToDouble(_keywords[i + 2]),
                            Convert.ToDouble(_keywords[i + 3]),
                            Convert.ToDouble(_keywords[i + 4])
                        );
                        int r = Convert.ToInt32(_keywords[i + 5]);
                        int g = Convert.ToInt32(_keywords[i + 6]);
                        int b = Convert.ToInt32(_keywords[i + 7]);

                        scene.AddPrimitive(new Sphere(scene.GetID(), r, g, b, radius, position));
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e);
                        Console.WriteLine(@"ERROR: 	 COULD NOT PARSE LINE {0}", line);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                return scene;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.WriteLine(@"COULD NOT PARSE THE INPUT");
                Console.ForegroundColor = ConsoleColor.White;
            }

            throw new Exception("FATAL PARSE ERROR");
        }

        public void PrintArray()
        {
            foreach (string keyword in _keywords) Console.WriteLine(keyword);
        }
    }
}