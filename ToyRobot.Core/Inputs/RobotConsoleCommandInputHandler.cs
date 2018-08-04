using System;

namespace ToyRobot.Core.Input
{
	internal class RobotConsoleCommandInputHandler : IRobotCommandInputHandler
	{
		private readonly IRobotInputProcessor _processor;

		public RobotConsoleCommandInputHandler(IRobotInputProcessor processor)
		{
			_processor = processor;
		}

		public void HandleInput()
		{
			do
			{
				var input = Console.ReadLine();

				if (input?.Equals("exit", StringComparison.OrdinalIgnoreCase) == true)
					break;

				var response = _processor.Process(input);

				if (!string.IsNullOrWhiteSpace(response?.Output))
					Console.WriteLine($"Output: {response.Output}");
			}
			while (true);
		}
	}
}
