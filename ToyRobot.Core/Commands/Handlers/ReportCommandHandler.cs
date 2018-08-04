using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands.Handlers
{
	internal class ReportCommandHandler : IHandleRobotCommand
	{
		public bool Supports(RobotCommandType commandType) => commandType == RobotCommandType.Report;

		public RobotCommandResponse HandleCommand(RobotCommand command, IRobot robot, ISurface surface)
		{
			if (!robot.IsPlaced)
				return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Report) { Output = "NOT PLACED" };

			return new RobotCommandResponse(RobotResponseType.Processed, RobotCommandType.Report) { Output = $"{robot.PositionX},{robot.PositionY},{robot.Direction?.ToString().ToUpperInvariant()}" };
		}
	}
}
