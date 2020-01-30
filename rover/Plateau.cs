using System;
using System.Collections.Generic;

namespace rover.Models
{
    public class Plateau
    {
        private int maxY;
        private int maxX;

        private List<Rover> rovers;

        public Plateau(int width, int height)
        {
            maxX = width;
            maxY = height;
            rovers = new List<Rover>();
        }

        public Rover addRover(Coordinates position)
        {
            Rover rover = null;
            if(isPositionAvailable(position.x, position.y))
            {
                rover = new Rover(this, position);
                rovers.Add(rover);
            }
            return rover;
        }

        public bool isPositionAvailable(int x, int y)
        {
            if(x > maxX || x < 0) return false; 
            if(y > maxY || y < 0) return false; 
            for(int i=0; i<rovers.Count; i++)
            {
                Coordinates position = rovers[i].getPosition();
                if(position.x == x && position.y == y) return false;  
            }
            return true;
        }
    }
}