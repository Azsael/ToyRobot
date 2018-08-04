using FluentAssertions;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using Xunit;
using Moq;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Tests
{
    public class ReportCommandHandlerTests
	{
		private readonly ReportCommandHandler _handler;
		private readonly Mock<ISurface> _surface;
		private readonly Mock<IRobot> _robot;

		public ReportCommandHandlerTests()
		{
			_handler = new ReportCommandHandler();

			_surface = new Mock<ISurface>();
			_robot = new Mock<IRobot>();
		}

		[Theory]
		[InlineData(RobotCommandType.Report, true)]
		[InlineData(RobotCommandType.Place, false)]
		[InlineData(RobotCommandType.Move, false)]
		[InlineData(RobotCommandType.Left, false)]
		[InlineData(RobotCommandType.Right, false)]
		public void GivenSupports_WhenGivenCommandType_ThenOnlySupportsReport(RobotCommandType commandType, bool result)
        {
			_handler.Supports(commandType).Should().Be(result);
		}

		[Fact]
		public void GivenHandleCommand_WhenNotPlaced_ThenNotPlacedOutput()
		{
			_robot.Setup(x => x.IsPlaced).Returns(false);
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
			response.Output.Should().Be("NOT PLACED");
		}

		[Fact]
		public void GivenHandleCommand_WhenPlaced_ThenPlacedOutput()
		{
			_robot.Setup(x => x.IsPlaced).Returns(true);
			_robot.Setup(x => x.PositionX).Returns(34);
			_robot.Setup(x => x.PositionY).Returns(53);
			_robot.Setup(x => x.Direction).Returns(RobotDirection.West);

			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
			response.Output.Should().Be("34,53,WEST");
		}
	}
}
