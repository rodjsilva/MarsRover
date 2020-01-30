using System;
using Xunit;
using rover.Models;
using rover;

namespace rover.tests
{
    public class Tests
    {
        [Fact]
        public void plateauCreated()
        {
            Plateau plateau = new Plateau(0,0);
            Assert.Equal(plateau.isPositionAvailable(0,0), true);
        }

        [Fact]
        public void roverCreatedOnPlateau()
        {
            Plateau plateau = new Plateau(0,0);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            Rover rover = plateau.addRover(coordinates);
            coordinates = rover.getPosition();
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 0, orientation = 'N'}) && plateau.isPositionAvailable(0,0) == false, true);
        }

        [Fact]
        public void roverCanRotateLeft()
        {
            Plateau plateau = new Plateau(0,0);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            Rover rover = plateau.addRover(coordinates);
            rover.rotateLeft();
            coordinates = rover.getPosition();
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 0, orientation = 'W'}), true);
        }

        [Fact]
        public void roverCanRotateRight()
        {
            Plateau plateau = new Plateau(0,0);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            Rover rover = plateau.addRover(coordinates);
            rover.rotateRight();
            coordinates = rover.getPosition();
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 0, orientation = 'E'}), true);
        }

        [Fact]
        public void roverCanMoveForward()
        {
            Plateau plateau = new Plateau(1,1);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            Rover rover = plateau.addRover(coordinates);
            rover.moveForward();
            coordinates = rover.getPosition();
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 1, orientation = 'N'}), true);
        }

        [Fact]
        public void roverCantMoveOutsidePlateau()
        {
            Plateau plateau = new Plateau(0,0);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            Rover rover = plateau.addRover(coordinates);
            rover.moveForward();
            rover.rotateRight();
            rover.moveForward();
            rover.rotateRight();
            rover.moveForward();
            rover.rotateRight();
            rover.moveForward();
            coordinates = rover.getPosition();
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 0, orientation = 'W'}), true);
        }

        [Fact]
        public void missionControlCanCommandRover()
        {
            Plateau plateau = new Plateau(0,0);
            MissionControl mc = new MissionControl(plateau);
            Coordinates coordinates = new Coordinates(){x = 0, y = 0, orientation = 'N' };
            coordinates = mc.processRover(coordinates, "MRMRMRM");
            Assert.Equal(coordinates.Equals(new Coordinates(){x = 0, y = 0, orientation = 'W'}), true);
        }

        [Theory]
        [InlineData(1,2,'N',"LMLMLMLMM", 1, 3, 'N')]
        [InlineData(3,3,'E',"MMRMMRMRRM", 5, 1, 'E')]
        public void MyFirstTheory(int x, int y, char orientation, string commands, int finalX, int finalY, char finalOrientation)
        {
            Plateau plateau = new Plateau(5,5);
            MissionControl mc = new MissionControl(plateau);
            Coordinates coordinates =  new Coordinates(){x = x, y = y, orientation = orientation};
            coordinates = mc.processRover(coordinates, commands);
            Coordinates final = new Coordinates(){x = finalX, y = finalY, orientation = finalOrientation};
            Assert.Equal(coordinates.Equals(final), true);
        }
    }
}
