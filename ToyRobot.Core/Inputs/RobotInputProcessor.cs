using ToyRobot.Core.Commands;

namespace ToyRobot.Core.Input
{
	internal class RobotInputProcessor : IRobotInputProcessor
	{
		private readonly IRobotCommandProcessor _commandProcessor;
		private readonly IRobotCommandParser _commandParser;

		public RobotInputProcessor(IRobotCommandProcessor commandProcessor, IRobotCommandParser commandParser)
		{
			_commandProcessor = commandProcessor;
			_commandParser = commandParser;
		}

		public RobotCommandResponse Process(string input)
		{
			if (!_commandParser.TryParse(input, out var command))
				return new RobotCommandResponse(RobotResponseType.Unknown);

			return _commandProcessor.ProcessCommand(command);
		}
	}
}
