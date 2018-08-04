using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal class LeftCommandHandler : IHandleRobotCommand
	{
		public bool Supports(RobotCommandType commandType) => commandType == RobotCommandType.Left;

		public RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface)
		{
			if (!robot.IsPlaced) return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Left);
			if (robot.Direction == null) return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Left);

			switch (robot.Direction.Value)
			{
				case RobotDirection.North:
					robot.Direction = RobotDirection.West;
					break;
				case RobotDirection.East:
					robot.Direction = RobotDirection.North;
					break;
				case RobotDirection.South:
					robot.Direction = RobotDirection.East;
					break;
				case RobotDirection.West:
					robot.Direction = RobotDirection.South;
					break;
			}
			return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Left);
		}
	}
}
