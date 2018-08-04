using FluentAssertions;
using ToyRobot.Core.Commands;
using Xunit;
using Moq;
using ToyRobot.Core.Input;

namespace ToyRobot.Core.Tests
{
    public class RobotInputProcessorTests
	{
		private readonly RobotInputProcessor _processor;
		private readonly Mock<IRobotCommandProcessor> _commandProcessor;
		private readonly Mock<IRobotCommandParser> _commandParser;

		public RobotInputProcessorTests()
		{
			_commandProcessor = new Mock<IRobotCommandProcessor>();
			_commandParser = new Mock<IRobotCommandParser>();

			_processor = new RobotInputProcessor(_commandProcessor.Object, _commandParser.Object);
		}

		[Fact]
		public void GivenProcess_WhenInputNotParsed_ThenUnknownResponse()
		{
			_commandParser.Setup(x => x.TryParse("a test", out It.Ref<RobotCommand>.IsAny)).Returns(false);

			var response = _processor.Process("a test");

			response.Should().NotBeNull();
			response.ResponseType.Should().Be(RobotResponseType.Unknown);
		}

		[Fact]
		public void GivenProcess_WhenInputParsed_ThenCommandProcessed()
		{
			var aResponse = new RobotCommandResponse(RobotResponseType.Processed);
			var robotCommand = new RobotCommand();
			_commandParser.Setup(x => x.TryParse("REPORT", out robotCommand)).Returns(true);
			_commandProcessor.Setup(x => x.ProcessCommand(robotCommand)).Returns(aResponse);

			var response = _processor.Process("REPORT");

			response.Should().Be(aResponse);
		}
	}
}
