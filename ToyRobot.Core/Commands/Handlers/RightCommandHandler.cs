using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal class RightCommandHandler : IHandleRobotCommand
	{
		public bool Supports(RobotCommandType commandType) => commandType == RobotCommandType.Right;

		public RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface)
		{
			if (!robot.IsPlaced) return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Right);
			if (robot.Direction == null) return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Right);

			switch (robot.Direction.Value)
			{
				case RobotDirection.North:
					robot.Direction = RobotDirection.East;
					break;
				case RobotDirection.East:
					robot.Direction = RobotDirection.South;
					break;
				case RobotDirection.South:
					robot.Direction = RobotDirection.West;
					break;
				case RobotDirection.West:
					robot.Direction = RobotDirection.North;
					break;
			}
			return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Right);
		}
	}
}
