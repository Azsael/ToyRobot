using System;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal class PlaceCommandHandler : IHandleRobotCommand
	{
		public bool Supports(RobotCommandType commandType) => commandType == RobotCommandType.Place;

		public RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface)
		{
			if (string.IsNullOrWhiteSpace(command.SecondaryArguments))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			var parameters = command.SecondaryArguments.Split(new[] { ',' }, 3);

			if (parameters.Length != 3)
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			if (!int.TryParse(parameters[0], out var positionX))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			if (!int.TryParse(parameters[1], out var positionY))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			if (!Enum.TryParse<RobotDirection>(parameters[2], true, out var direction))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			if (!surface.IsValidPosition(positionX, positionY))
				return new RobotCommandResponse(RobotResponseType.Invalid, RobotCommandType.Place);

			robot.Place(positionX, positionY, direction);

			return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Place);
		}
	}
}
