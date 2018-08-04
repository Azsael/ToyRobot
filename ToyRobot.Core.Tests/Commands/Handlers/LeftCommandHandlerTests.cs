using FluentAssertions;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using Xunit;
using Moq;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Tests
{
    public class LeftCommandHandlerTests
	{
		private readonly LeftCommandHandler _handler;
		private readonly Mock<ISurface> _surface;
		private readonly Mock<IRobot> _robot;

		public LeftCommandHandlerTests()
		{
			_handler = new LeftCommandHandler();

			_surface = new Mock<ISurface>();
			_robot = new Mock<IRobot>();
		}

		[Theory]
		[InlineData(RobotCommandType.Report, false)]
		[InlineData(RobotCommandType.Place, false)]
		[InlineData(RobotCommandType.Move, false)]
		[InlineData(RobotCommandType.Left, true)]
		[InlineData(RobotCommandType.Right, false)]
		public void GivenSupports_WhenGivenCommandType_ThenOnlySupportsLeft(RobotCommandType commandType, bool result)
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
		public void GivenHandleCommand_WhenNoDirect_ThenInvalidResponse()
		{
			_robot.Setup(x => x.IsPlaced).Returns(true);
			_robot.Setup(x => x.Direction).Returns((RobotDirection?)null);
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Theory]
		[InlineData(RobotDirection.North, RobotDirection.West)]
		[InlineData(RobotDirection.East, RobotDirection.North)]
		[InlineData(RobotDirection.South, RobotDirection.East)]
		[InlineData(RobotDirection.West, RobotDirection.South)]
		public void GivenHandleCommand_WhenRobotHasDirect_ThenRobotTurned90DegreesRight(RobotDirection direction, RobotDirection expectedDirection)
		{
			_robot.Setup(x => x.IsPlaced).Returns(true);
			_robot.Setup(x => x.Direction).Returns(direction);
			_robot.SetupSet(x => x.Direction = expectedDirection).Verifiable();
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
			_robot.Verify();
		}
	}
}
