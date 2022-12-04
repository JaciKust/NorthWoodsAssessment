using System;

namespace NorthWoodsAssesmentTest.BabysitterTest
{
	public class BabysitterTestBase
	{
		protected static DateTime GetDateTime(int day, int hour, int minute = 0)
		{
			return new DateTime(2012, 01, day, hour, minute, 0);
		}
	}
}
