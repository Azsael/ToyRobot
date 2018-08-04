using Microsoft.Extensions.DependencyInjection;
using ToyRobot.Core.Input;
using ToyRobot.Core.Ioc;

namespace ToyRobot.App
{
    class Program
    {
        static void Main(string[] args)
        {
			var serviceProvider = new ServiceCollection()
				.ConfigureBindings()
				.BuildServiceProvider();

			var robotConsoleHandler = serviceProvider.GetService<IRobotCommandInputHandler>();
			
			robotConsoleHandler.HandleInput();
        }
    }
}