using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ToyRobot.Core.Input;
using ToyRobot.Core.Ioc;
using Xunit;
using FluentAssertions;

namespace ToyRobot.Core.Integration
{
    public class ToyRobotIntegrationTests
	{
		private readonly IRobotInputProcessor _processor;

		public ToyRobotIntegrationTests()
		{
			var serviceProvider = new ServiceCollection()
				.ConfigureBindings()
				.BuildServiceProvider();

			_processor = serviceProvider.GetService<IRobotInputProcessor>();
		}

		[Theory]
		[InlineData("NOT PLACED", "MOVE", "MOVE", "WEST")]
		[InlineData("0,1,NORTH", "PLACE 0,0,NORTH", "MOVE")]
		[InlineData("0,0,WEST", "PLACE 0,0,NORTH", "LEFT")]
		[InlineData("3,3,NORTH", "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE")]
		public void GivenCommands_WhenProcessed_ThenOutputIsAsExpected(string expectedOutput, params string[] commands)
        {
			commands.Select(x => _processor.Process(x)).ToList();

			var response = _processor.Process("REPORT");

			response.Output.Should().Be(expectedOutput);
        }
    }
}
