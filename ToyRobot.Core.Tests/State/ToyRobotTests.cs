using Xunit;
using FluentAssertions;

namespace ToyRobot.Core.Tests
{
	public class ToyRobotTests
	{
		[Fact]
		public void GivenGetNextPosition_WhenNotPlaced_ThenNoPosition()
		{
			var robot = new State.ToyRobot();

			robot.GetNextPosition().Should().BeNull();
		}

		[Fact]
		public void GivenGetNextPosition_WhenNoDirection_ThenNoPosition()
		{
			var robot = new State.ToyRobot();
			robot.Place(1, 1, RobotDirection.East);
			robot.Direction = null;

			robot.GetNextPosition().Should().BeNull();
		}

		[Theory]
		[InlineData(1, 2, 2, RobotDirection.North, 2, 3)]
		[InlineData(1, 2, 2, RobotDirection.East, 3, 2)]
		[InlineData(1, 2, 2, RobotDirection.South, 2, 1)]
		[InlineData(1, 2, 2, RobotDirection.West, 1, 2)]
		[InlineData(2, 2, 2, RobotDirection.North, 2, 4)]
		[InlineData(2, 2, 2, RobotDirection.East, 4, 2)]
		[InlineData(2, 2, 2, RobotDirection.South, 2, 0)]
		[InlineData(2, 2, 2, RobotDirection.West, 0, 2)]
		public void GivenGetNextPosition_WhenPlaced_ThenNewPositionIsApplicationOfDirectionAndSpeed(int speed, int x, int y, RobotDirection direction, int expectedX, int expectedY)
		{
			var robot = new State.ToyRobot(speed);
			robot.Place(x, y, direction);

			var position = robot.GetNextPosition();

			position.Should().NotBeNull();
			position.X.Should().Be(expectedX);
			position.Y.Should().Be(expectedY);
		}

		[Fact]
		public void GivenMove_WhenNoNextPosition_ThenNoMovement()
		{
			var robot = new State.ToyRobot();
			robot.Place(1, 1, RobotDirection.East);
			robot.Direction = null;

			robot.Move();

			robot.PositionX.Should().Be(1);
			robot.PositionY.Should().Be(1);
		}

		[Fact]
		public void GivenMove_WhenNextPosition_ThenMovement()
		{
			var robot = new State.ToyRobot();
			robot.Place(1, 1, RobotDirection.South);


			robot.Move();

			robot.PositionX.Should().Be(1);
			robot.PositionY.Should().Be(0);
		}

		[Theory]
		[InlineData(3, 2, RobotDirection.West)]
		[InlineData(0, 3, RobotDirection.South)]
		public void GivenPlace_WhenPlacing_ThenPositionAndDirectionSet(int positionX, int positionY, RobotDirection direction)
		{
			var robot = new State.ToyRobot();

			robot.Place(positionX, positionY, direction);

			robot.IsPlaced.Should().BeTrue();
			robot.PositionX.Should().Be(positionX);
			robot.PositionY.Should().Be(positionY);
			robot.Direction.Should().Be(direction);
		}
	}
}
