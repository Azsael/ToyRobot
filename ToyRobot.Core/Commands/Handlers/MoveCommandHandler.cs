using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal class MoveCommandHandler : IHandleRobotCommand
	{
		public bool Supports(RobotCommandType commandType) => commandType == RobotCommandType.Move;

		public RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface)
		{
			if (!robot.IsPlaced)
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Move);

			var newPosition = robot.GetNextPosition();

			if (!surface.IsValidPosition(newPosition.X, newPosition.Y))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Move);

			robot.Move();

			return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Move);
		}
	}
}
