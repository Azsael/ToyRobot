using ToyRobot.Core.Commands;
using Xunit;
using FluentAssertions;

namespace ToyRobot.Core.Tests
{
    public class RobotCommandParserTests
	{
		private readonly RobotCommandParser _parser;

		public RobotCommandParserTests()
		{
			_parser = new RobotCommandParser();
		}

		[Theory]
		[InlineData("")]
		[InlineData("   ")]
		[InlineData(null)]
		public void GivenTryParse_WhenInputNullOrWhiteSpace_ThenFalse(string input)
		{
			var isParsed = _parser.TryParse(input, out var command);

			isParsed.Should().BeFalse();
			command.Should().BeNull();
		}

		[Theory]
		[InlineData("south")]
		[InlineData("help")]
		public void GivenTryParse_WhenInputIsNotEnum_ThenFalse(string input)
		{
			var isParsed = _parser.TryParse(input, out var command);

			isParsed.Should().BeFalse();
			command.Should().BeNull();
		}

		[Theory]
		[InlineData("PLACE 0,0,WEST", RobotCommandType.Place, "0,0,WEST")]
		[InlineData("REPORT", RobotCommandType.Report, null)]
		[InlineData("MOVE", RobotCommandType.Move, null)]
		public void GivenTryParse_WhenInputIsEnum_ThenTrueAndCommandSet(string input, RobotCommandType commandType, string secondaryArgs)
		{
			var isParsed = _parser.TryParse(input, out var command);

			isParsed.Should().BeTrue();
			command.Should().NotBeNull();
			command.CommandType.Should().Be(commandType);
			command.SecondaryArguments.Should().Be(secondaryArgs);
		}
	}
}
