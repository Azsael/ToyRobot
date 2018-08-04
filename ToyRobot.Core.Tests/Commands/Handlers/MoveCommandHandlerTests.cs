using FluentAssertions;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using Xunit;
using Moq;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Tests
{
    public class MoveCommandHandlerTests
	{
		private readonly MoveCommandHandler _handler;
		private readonly Mock<ISurface> _surface;
		private readonly Mock<IRobot> _robot;

		public MoveCommandHandlerTests()
		{
			_handler = new MoveCommandHandler();

			_surface = new Mock<ISurface>();
			_robot = new Mock<IRobot>();
		}

		[Theory]
		[InlineData(RobotCommandType.Report, false)]
		[InlineData(RobotCommandType.Place, false)]
		[InlineData(RobotCommandType.Move, true)]
		[InlineData(RobotCommandType.Left, false)]
		[InlineData(RobotCommandType.Right, false)]
		public void GivenSupports_WhenGivenCommandType_ThenOnlySupportsMove(RobotCommandType commandType, bool result)
		{
			_handler.Supports(commandType).Should().Be(result);
		}

		[Fact]
		public void GivenHandleCommand_WhenNotPlaced_ThenInvalidResponse()
		{
			_robot.Setup(x => x.IsPlaced).Returns(false);
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenPlacedButNotValidPosition_ThenInvalidResponse()
		{
			_robot.Setup(x => x.IsPlaced).Returns(true);
			_robot.Setup(x => x.GetNextPosition()).Returns(new Position { X = 1, Y = 2 });
			_surface.Setup(x => x.IsValidPosition(1, 2)).Returns(false);
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenPlacedAndValidPosition_ThenRobotMovedAndProcessedResponse()
		{
			_robot.Setup(x => x.IsPlaced).Returns(true);
			_robot.Setup(x => x.GetNextPosition()).Returns(new Position { X = 3, Y = 4 });
			_surface.Setup(x => x.IsValidPosition(3, 4)).Returns(true);
			_robot.Setup(x => x.Move()).Verifiable();
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
			_robot.Verify();
		}
	}
}
