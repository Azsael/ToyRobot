namespace ToyRobot.Core.State
{
	internal interface ISurface
	{
		SurfaceType Type { get; }

		bool IsValidPosition(int positionX, int positionY);
	}
}
