namespace ToyRobot.Core.Commands
{
	internal interface IRobotCommandParser
	{
		bool TryParse(string input, out RobotCommand command);
	}
}
