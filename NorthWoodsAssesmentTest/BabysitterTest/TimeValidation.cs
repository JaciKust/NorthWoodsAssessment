using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWoodsAssessment;

namespace NorthWoodsAssesmentTest.BabysitterTest
{
	[TestClass]
	public class TimeValidation : BabysitterTestBase
	{

		private static DateTime _validBedTime = GetDateTime(day: 1, hour: 11);

		[TestMethod]
		public void InvalidStartTimeThrowsException()
		{
			// 3pm start time.
			var invalidStartTime = GetDateTime(day: 1, hour: 16, minute: 59);
			var validEndTime = GetDateTime(day: 1, hour: 19);

			var objectUnderTest = new Babysitter();

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => objectUnderTest.GetPayForNight(invalidStartTime, validEndTime, _validBedTime));
		}

		[TestMethod]
		public void InvalidEndTimeThrowsException()
		{
			var validStartTime = GetDateTime(day: 1, hour: 19);
			var invalidEndTime = GetDateTime(day: 2, hour: 4, minute: 1);

			var objectUnderTest = new Babysitter();

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => objectUnderTest.GetPayForNight(validStartTime, invalidEndTime, _validBedTime));
		}

		[TestMethod]
		public void EdgeCasesAreValid()
		{
			var validStartTime = GetDateTime(day: 1, hour: 17);
			var validEndTime = GetDateTime(day: 2, hour: 4);

			var objectUnderTest = new Babysitter();

			try
			{
				objectUnderTest.GetPayForNight(validStartTime, validEndTime, _validBedTime);
			}
			catch (Exception e)
			{
				Assert.Fail($"Expected no exception but received {e.GetType().Name} with message: {e.Message}");
			}
		}

		[TestMethod]
		public void StartTimeMustBeBeforeEndTime()
		{
			var validStartTime = GetDateTime(day: 1, hour: 20);
			var invalidEndTime = GetDateTime(day: 1, hour: 19);

			var objectUnderTest = new Babysitter();
			Assert.ThrowsException<InvalidOperationException>(() => objectUnderTest.GetPayForNight(validStartTime, invalidEndTime, _validBedTime));
		}
	}
}