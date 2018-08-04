namespace ToyRobot.Core.State
{
	internal class Table : ISurface
	{
		public int Length { get; }
		public int Width { get; }

		public SurfaceType Type => SurfaceType.Flat;

		public bool IsValidPosition(int positionX, int positionY)
		{
			return positionX >= 0 && positionX <= (Width - 1)
				&& positionY >= 0 && positionY <= (Length - 1);
		}

		public Table(int length, int width)
		{
			Length = length;
			Width = width;
		}
	}
}
