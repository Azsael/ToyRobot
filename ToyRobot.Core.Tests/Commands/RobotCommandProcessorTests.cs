using FluentAssertions;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using Xunit;
using Moq;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Tests
{
    public class RobotCommandProcessorTests
	{
		private readonly RobotCommandProcessor _processor;

		private readonly Mock<IRobotStateManager> _stateManager;
		private readonly Mock<IHandleRobotCommand> _handlerOne;
		private readonly Mock<IHandleRobotCommand> _handlerTwo;

		public RobotCommandProcessorTests()
		{
			_stateManager = new Mock<IRobotStateManager>();
			_handlerOne = new Mock<IHandleRobotCommand>();
			_handlerTwo = new Mock<IHandleRobotCommand>();

			_processor = new RobotCommandProcessor(_stateManager.Object, new[] { _handlerOne.Object, _handlerTwo.Object });
		}

		[Fact]
		public void GivenProcessCommand_WhenNoHandlersSupport_ThenUnknownResponse()
		{
			_handlerOne.Setup(x => x.Supports(RobotCommandType.Move)).Returns(false);
			_handlerTwo.Setup(x => x.Supports(RobotCommandType.Move)).Returns(false);
			var response = _processor.ProcessCommand(new RobotCommand { CommandType = RobotCommandType.Move });

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Unknown);
		}

		[Fact]
		public void GivenProcessCommand_WhenHandlersSupport_ThenCommandHandled()
		{
			var command = new RobotCommand { CommandType = RobotCommandType.Left };

			_handlerOne.Setup(x => x.Supports(RobotCommandType.Left)).Returns(false);
			_handlerTwo.Setup(x => x.Supports(RobotCommandType.Left)).Returns(true);
			_handlerTwo.Setup(x => x.HandleCommand(command, It.IsAny<IRobot>(), It.IsAny<ISurface>()))
				.Returns(new RobotCommandResponse(RobotResponseType.Processed));
			var response = _processor.ProcessCommand(command);

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Processed);
		}
	}
}
