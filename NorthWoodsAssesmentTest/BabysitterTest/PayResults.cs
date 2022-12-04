using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWoodsAssessment;

namespace NorthWoodsAssesmentTest.BabysitterTest
{
	[TestClass]
	public class PayResults : BabysitterTestBase
	{
		[TestMethod]
		public void StartBedEndMidnight()
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
		public void StartBedMidnightEnd()
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
		public void MidnightStartBedEnd()
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
		public void BedStartMidnightEnd()
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

		[TestMethod]
		public void StartEndBedMidnight()
		{
			var startTime = GetDateTime(1, 17);
			// Start to end = 2 * $12 = $24
			var endTime = GetDateTime(1, 19);
			var bedTime = GetDateTime(1, 20);

			var expectedResult = 24.00;
			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void StartMidnightEndBed()
		{
			var startTime = GetDateTime(1, 17);
			// 7 hours till midnight which we don't care about the bedtime: 7 * $12 = $84
			// 3 hours post midnight which we don't care about the bedtime: 3 * $16 = $48
			var endTime = GetDateTime(2, 3);
			var bedTime = GetDateTime(2, 5);
			// $84 + $48 = $132
			var expectedResult = 132.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void StartMidnightBedEnd()
		{
			var startTime = GetDateTime(1, 17);
			// 7 hours till midnight which we don't care about the bedtime: 7 * $12 = $84
			var bedTime = GetDateTime(2, 1);
			// 3 hours post midnight which we don't care about the bedtime: 3 * $16 = $48
			var endTime = GetDateTime(2, 3);
			// $84 + $48 = $132
			var expectedResult = 132.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void BedStartEndMidnight()
		{
			var bedTime = GetDateTime(1, 16);
			var startTime = GetDateTime(1, 17);
			// 2 hours till end which we don't care about the bedtime: 2 * $8 = $16
			var endTime = GetDateTime(1, 19);
			var expectedResult = 16.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void BedMidnightStartEnd()
		{
			var bedTime = GetDateTime(1, 23);
			var startTime = GetDateTime(2, 1);
			var endTime = GetDateTime(2, 2);
			// One hour post midnight: $16
			var expectedResult = 16;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void MidnightStartEndBed()
		{
			var startTime = GetDateTime(2, 1);
			var endTime = GetDateTime(2, 2);
			var bedTime = GetDateTime(2, 4);

			// One hour post midnight: $16
			var expectedResult = 16;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void MidnightBedStartEnd()
		{
			var bedTime = GetDateTime(2, 0);
			var startTime = GetDateTime(2, 1);
			var endTime = GetDateTime(2, 2);

			// One hour post midnight: $16
			var expectedResult = 16;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}
	}
}
