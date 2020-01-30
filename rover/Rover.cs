namespace rover.Models
{
    public class Rover
    {
        private Coordinates position;        
        private Plateau terrain;

        public Rover(Plateau plateau, Coordinates coordinates)
        {
            position = coordinates;
            terrain = plateau;            
        }

        public void execCommands(string commands)
        {
            for(int i=0; i<commands.Length; i++)
            {
                switch(commands[i])
                {
                    case 'L':
                        rotateLeft();
                        break;
                    case 'R':
                        rotateRight();
                        break;
                    case 'M':
                        moveForward();
                        break;
                }
            }    
        }

        public void rotateLeft()
        {
            switch(position.orientation)
            {
                case 'N':
                    position.orientation = 'W';
                    break;
                case 'S':
                    position.orientation = 'E';
                    break;
                case 'W':
                    position.orientation = 'S';
                    break;
                case 'E':
                    position.orientation = 'N';
                    break;
            }
        }

        public void rotateRight()
        {
            switch(position.orientation)
            {
                case 'N':
                    position.orientation = 'E';
                    break;
                case 'S':
                    position.orientation = 'W';
                    break;
                case 'W':
                    position.orientation = 'N';
                    break;
                case 'E':
                    position.orientation = 'S';
                    break;
            }
        }

        public void moveForward()
        {
            int requiredX = position.x;
            int requiredY = position.y;
            switch(position.orientation)
            {
                case 'N':
                    requiredY++;
                    break;
                case 'S':
                    requiredY--;
                    break;
                case 'W':
                    requiredX--;
                    break;
                case 'E':
                    requiredX++;
                    break;
            }
            if(terrain.isPositionAvailable(requiredX, requiredY))
            {
                position.x = requiredX;
                position.y = requiredY;
            }
        }

        public Coordinates getPosition()
        {
            return position;
        }
    }
}