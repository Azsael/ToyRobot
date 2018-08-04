namespace ToyRobot.Core
{

	public class RobotCommandResponse
	{
		public RobotResponseType ResponseType { get; }
		public RobotCommandType? CommandType { get; set; }
		public string Output { get; set; }

		public RobotCommandResponse(RobotResponseType responseType, RobotCommandType? commandType = null)
		{
			ResponseType = responseType;
			CommandType = commandType;
		}
	}
}
