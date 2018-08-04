using System;

namespace ToyRobot.Core.State
{

	internal interface IRobot
	{
		bool IsPlaced { get; }
		RobotDirection? Direction { get; set; }
		int? PositionX { get; }
		int? PositionY { get; }

		void Place(int positionX, int positionY, RobotDirection direction);
		void Move();
		Position GetNextPosition();
	}
}
