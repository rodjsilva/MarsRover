using System;

namespace rover.Models
{
    public class Coordinates
    {
        public int x;
        public int y;
        public char orientation;

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) 
            {
                return false;
            }
            else 
            { 
                Coordinates c = (Coordinates) obj; 
                return (x == c.x) && (y == c.y) && (orientation == c.orientation);
            }   
        }

        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }
    }
}