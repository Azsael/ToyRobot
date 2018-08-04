namespace ToyRobot.Core.State
{
	internal interface IRobotStateManager
	{
		ISurface Surface { get; }
		IRobot Robot { get; }
	}
}
