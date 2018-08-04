using FluentAssertions;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using Xunit;
using Moq;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Tests
{
    public class PlaceCommandHandlerTests
	{
		private readonly PlaceCommandHandler _handler;
		private readonly Mock<ISurface> _surface;
		private readonly Mock<IRobot> _robot;

		public PlaceCommandHandlerTests()
		{
			_handler = new PlaceCommandHandler();

			_surface = new Mock<ISurface>();
			_robot = new Mock<IRobot>();
		}

		[Theory]
		[InlineData(RobotCommandType.Report, false)]
		[InlineData(RobotCommandType.Place, true)]
		[InlineData(RobotCommandType.Move, false)]
		[InlineData(RobotCommandType.Left, false)]
		[InlineData(RobotCommandType.Right, false)]
		public void GivenSupports_WhenGivenCommandType_ThenOnlySupportsPlace(RobotCommandType commandType, bool result)
		{
			_handler.Supports(commandType).Should().Be(result);
		}

		[Fact]
		public void GivenHandleCommand_WhenNoSecondaryArgs_ThenInvalidResponse()
		{
			var response = _handler.HandleCommand(new RobotCommand(), _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenNotEnoughArguments_ThenInvalidResponse()
		{
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "1,2" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenCannotParseX_ThenInvalidResponse()
		{
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "ONE,2,west" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenCannotParseY_ThenInvalidResponse()
		{
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "1,Two,east" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenCannotParseDirection_ThenInvalidResponse()
		{
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "1,2,left" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenInvalidPosition_ThenInvalidResponse()
		{
			_surface.Setup(x => x.IsValidPosition(-1, -1)).Returns(false);
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "-1,-1,west" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Invalid);
		}

		[Fact]
		public void GivenHandleCommand_WhenValidPosition_ThenRobotPlacedAndProcessedResponse()
		{
			_surface.Setup(x => x.IsValidPosition(1, 2)).Returns(true);
			_robot.Setup(x => x.Place(1, 2, RobotDirection.North)).Verifiable();
			var response = _handler.HandleCommand(new RobotCommand { SecondaryArguments = "1,2,north" }, _robot.Object, _surface.Object);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
			_robot.Verify();
		}
	}
}
