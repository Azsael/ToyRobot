using System;

namespace ToyRobot.Core.State
{
	internal class ToyRobot : IRobot
	{
		public bool IsPlaced { get; private set; }

		public RobotDirection? Direction { get; set; }

		public int? PositionX { get; private set; }

		public int? PositionY { get; private set; }
		public int Speed { get; }

		public ToyRobot(int speed = 1)
		{
			Speed = speed;
		}

		public Position GetNextPosition()
		{
			if (!IsPlaced)
				return null;

			if (!Direction.HasValue)
				return null;

			switch (Direction.Value)
			{
				case RobotDirection.North:
					return new Position { X = PositionX.GetValueOrDefault(), Y = PositionY.GetValueOrDefault() + Speed };
				case RobotDirection.East:
					return new Position { X = PositionX.GetValueOrDefault() + Speed, Y = PositionY.GetValueOrDefault() };
				case RobotDirection.South:
					return new Position { X = PositionX.GetValueOrDefault(), Y = PositionY.GetValueOrDefault() - Speed };
				case RobotDirection.West:
					return new Position { X = PositionX.GetValueOrDefault() - Speed, Y = PositionY.GetValueOrDefault() };
			}
			return null;
		}

		public void Move()
		{
			var newPosition = GetNextPosition();

			if (newPosition == null)
				return;

			PositionX = newPosition.X;
			PositionY = newPosition.Y;
		}

		public void Place(int positionX, int positionY, RobotDirection direction)
		{
			IsPlaced = true;
			PositionX = positionX;
			PositionY = positionY;
			Direction = direction;
		}
	}
}
