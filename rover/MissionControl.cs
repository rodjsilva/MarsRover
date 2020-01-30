using System;
using rover.Models;

namespace rover
{
    public class MissionControl
    {
        private Plateau plateau;
        
        public MissionControl(Plateau terrain)
        {
            plateau = terrain;
        }

        public Coordinates processRover(Coordinates coordinates, string commands)
        {
            Rover rover = plateau.addRover(coordinates);
            if(rover == null)
            {
                int error = (int) Errors.PositionUnavailableError;
                return new Coordinates(){x = -1, y = -1, orientation = Convert.ToChar(error.ToString())};
            }
            rover.execCommands(commands);
            return rover.getPosition();
        }
    }
}