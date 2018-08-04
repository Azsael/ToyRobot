using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal interface IHandleRobotCommand
	{
		bool Supports(RobotCommandType commandType);

		RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface);
	}
}
