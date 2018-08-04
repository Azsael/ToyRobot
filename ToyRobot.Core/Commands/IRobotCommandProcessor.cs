using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands
{
	internal interface IRobotCommandProcessor
	{
		RobotCommandResponse ProcessCommand(RobotCommand command);
	}
}
