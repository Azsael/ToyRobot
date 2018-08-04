namespace ToyRobot.Core.Input
{
	public interface IRobotInputProcessor
	{
		RobotCommandResponse Process(string input);
	}
}
