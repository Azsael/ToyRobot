using System;

namespace ToyRobot.Core.Commands
{

	internal class RobotCommandParser : IRobotCommandParser
	{
		public bool TryParse(string input, out RobotCommand command)
		{
			command = null;

			if (string.IsNullOrWhiteSpace(input))
				return false;

			var inputArray = input.Split(new[] { ' ' }, 2);

			if (!Enum.TryParse<RobotCommandType>(inputArray[0], true, out var commandType))
				return false;
			
			command = new RobotCommand
			{
				CommandType = commandType,
				SecondaryArguments = inputArray.Length > 1 ? inputArray[1] : null
			};
			return true;
		}

	}
}
