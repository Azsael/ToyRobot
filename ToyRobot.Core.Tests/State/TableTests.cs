using FluentAssertions;
using ToyRobot.Core.State;
using Xunit;

namespace ToyRobot.Core.Tests
{
    public class TableTests
	{
		[Theory]
		[InlineData(5, 5, 0, 0)]
		[InlineData(5, 5, 4, 4)]
		[InlineData(5, 5, 3, 2)]
		[InlineData(10, 5, 7, 2)]
		public void GivenIsValidPosition_WhenPositionOnTable_ThenTrue(int width, int length, int x, int y)
		{
			var table = new Table(length, width);

			var isValid = table.IsValidPosition(x, y);

			isValid.Should().BeTrue();
		}
		
		[Theory]
		[InlineData(5, 5, -1, -1)]
		[InlineData(5, 5, 5, 5)]
		[InlineData(5, 5, 5, 4)]
		[InlineData(5, 5, 4, 5)]
		[InlineData(10, 5, 11, 2)]
		[InlineData(10, 15, 8, 16)]
		public void GivenIsValidPosition_WhenPositionOffTable_ThenFalse(int width, int length, int x, int y)
		{
			var table = new Table(length, width);

			var isValid = table.IsValidPosition(x, y);

			isValid.Should().BeFalse();
		}
	}
}
