using System.Collections.Generic;
using System.Linq;
using ToyRobot.Core.Commands.Handlers;
using ToyRobot.Core.State;

namespace ToyRobot.Core.Commands
{
	internal class RobotCommandProcessor : IRobotCommandProcessor
	{
		private readonly IList<IHandleRobotCommand> _commandHandlers;
		private readonly IRobotStateManager _stateManager;

		public RobotCommandProcessor(IRobotStateManager stateManager, IEnumerable<IHandleRobotCommand> commandHandlers)
		{
			_stateManager = stateManager;
			_commandHandlers = commandHandlers.ToList();
		}

		public RobotCommandResponse ProcessCommand(RobotCommand command)
		{
			var commandHandler = _commandHandlers.FirstOrDefault(x => x.Supports(command.CommandType));

			if (commandHandler == null)
				return new RobotCommandResponse(RobotResponseType.Unknown);

			return commandHandler.HandleCommand(command, _stateManager.Robot, _stateManager.Surface);
		}
	}
}
