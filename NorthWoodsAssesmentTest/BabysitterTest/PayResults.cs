using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWoodsAssessment;

namespace NorthWoodsAssesmentTest.BabysitterTest
{
	[TestClass]
	public class PayResults : BabysitterTestBase
	{
		[TestMethod]
		public void TestHoursBeforeMidnight()
		{
			var startTime = GetDateTime(1, 17);
			// 3 hours of awake with children = $12 * 3 = $36
			var bedTime = GetDateTime(1, 20);
			// 3 hours with children sleeping = $8 * 3 = $24
			var endTime = GetDateTime(1, 23);

			// Expected Pay = $36 + $24 = $60
			var expectedResult = 60.00;

			var objectUnderTest = new Babysitter();

			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void TestHoursAfterMidnight()
		{
			var startTime = GetDateTime(1, 17);
			// 3 hours of awake with children = $12 * 3 = $36
			var bedTime = GetDateTime(1, 20);

			// 4 hours until midnight + 3 hours after = $8 * 4 + $16 * 3 = $80
			var endTime = GetDateTime(2, 3);

			// Expected Pay = $36 + $80 = 116
			var expectedResult = 116;

			var objectUnderTest = new Babysitter();

			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void TestHoursOnlyAfterMidnightWithAfterMidnightBedtime()
		{
			var startTime = GetDateTime(2, 0);
			// Because start time is after midnight should be payed $16/h off of the bat = 16 * 1 = $16
			var bedTime = GetDateTime(2, 1);
			// Bed time is meaningless here because we still get $16/h = 16 * 2 = 32
			var endTime = GetDateTime(2, 3);

			// Expected Pay = $16 + $32 = $48
			var expectedResult = 48.00;

			var objectUnderTest = new Babysitter();

			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void TestHoursAfterBedtimeOnly()
		{
			var bedTime = GetDateTime(1, 16);
			// Bed time is before start time so pay till midnight @ bedtime rate = 6 * $8 = $48
			var startTime = GetDateTime(1, 18);
			// Between midnight and leav time = $16 * 1 = 16
			var endTime = GetDateTime(2, 1);

			// Expected Pay = $16 + $48 = $64
			var expectedResult = 64.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}
	}
}
