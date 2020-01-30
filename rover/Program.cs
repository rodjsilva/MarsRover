using System;
using System.IO;
using System.Text;
using rover.Models;

namespace rover
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Error: you must specify the input file path as a command line argument");
                return;
            }

            var path = args[0];            

            try
            {
                int x;
                int y;
                string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                if(lines.Length < 3 || (lines.Length - 1) % 2 != 0)
                {
                    Console.WriteLine("Invalid input: insufficient number of lines of text (minimum of 3, one for the plateau size and 2 for each rover: initial position and instructions");
                }
                else
                {
                    string plateauString = lines[0];
                    string[] plateauDimensions = plateauString.Split(' ');
                    if(plateauDimensions.Length != 2 || !int.TryParse(plateauDimensions[0], out x) || !int.TryParse(plateauDimensions[1], out y) || x < 0 || y < 0)
                    {
                        Console.WriteLine("Invalid input: invalid plateau parameters");
                    }
                    else
                    {
                        Plateau plateau = new Plateau(x, y);
                        MissionControl mc = new MissionControl(plateau);
                        for(int i=1; i<lines.Length; i+=2)
                        {
                            string coordinatesString = lines[i];
                            string commandsString = lines[i+1];
                            string[] coordinateParts = coordinatesString.Split(' ');
                            int error = 0;
                            if(coordinateParts.Length != 3) error = (int)Errors.InvalidCoordinatesError;
                            if(!int.TryParse(coordinateParts[0], out x)) error = (int)Errors.InvalidXError;
                            if(!int.TryParse(coordinateParts[1], out y)) error = (int)Errors.InvalidYError;
                            if(coordinateParts[2].Length != 1 || !"NSWE".Contains(coordinateParts[2])) error = (int)Errors.InvalidOrientationError;
                            foreach(char c in commandsString)
                            {
                                if(!"LRM".Contains(c))
                                {
                                    error = (int)Errors.InvalidInstructionError;
                                    break;
                                }
                            }
                            if(error > 0)
                            {
                                Console.WriteLine(String.Format("-1 -1 {0}", error.ToString()));
                            }
                            else
                            {
                                Coordinates coordinates = new Coordinates(){x = x, y = y, orientation = Convert.ToChar(coordinateParts[2])};
                                coordinates.x = x;
                                coordinates.y = y;
                                coordinates.orientation = Convert.ToChar(coordinateParts[2]);
                                coordinates = mc.processRover(coordinates, commandsString);
                                Console.WriteLine(String.Format("{0} {1} {2}", coordinates.x, coordinates.y, coordinates.orientation));
                            }
                        }
                    }
                }               
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("Exception: {0}", ex.Message));
            }
        }
    }
}
