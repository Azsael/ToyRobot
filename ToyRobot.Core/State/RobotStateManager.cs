namespace ToyRobot.Core.State
{
	internal class RobotStateManager : IRobotStateManager
	{
		public ISurface Surface { get; }

		public IRobot Robot { get; }

		public RobotStateManager(ISurface surface, IRobot robot)
		{
			Surface = surface;
			Robot = robot;
		}
	}

}
