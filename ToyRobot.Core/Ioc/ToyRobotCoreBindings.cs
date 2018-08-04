using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using ToyRobot.Core.Commands;
using ToyRobot.Core.Commands.Handlers;
using ToyRobot.Core.Input;
using ToyRobot.Core.State;

[assembly: InternalsVisibleTo("ToyRobot.Core.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace ToyRobot.Core.Ioc
{
    public static class ToyRobotCoreBindings
    {
		public static IServiceCollection ConfigureBindings(this IServiceCollection services)
		{
			return services
				.AddSingleton<IRobotCommandProcessor, RobotCommandProcessor>()
				.AddSingleton<IRobotCommandParser, RobotCommandParser>()
				.AddSingleton<IHandleRobotCommand, PlaceCommandHandler>()
				.AddSingleton<IHandleRobotCommand, MoveCommandHandler>()
				.AddSingleton<IHandleRobotCommand, LeftCommandHandler>()
				.AddSingleton<IHandleRobotCommand, RightCommandHandler>()
				.AddSingleton<IHandleRobotCommand, ReportCommandHandler>()
				.AddSingleton<IRobotCommandInputHandler, RobotConsoleCommandInputHandler>()
				.AddSingleton<IRobotInputProcessor, RobotInputProcessor>()
				.AddSingleton<IRobotStateManager, RobotStateManager>()
				.AddSingleton<IRobot>(x => new State.ToyRobot())
				.AddSingleton<ISurface>(x => new Table(5, 5));
		}

    }
}
