using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWoodsAssessment;

namespace NorthWoodsAssesmentTest.BabysitterTest
{
	[TestClass]
	public class RoundedResults : BabysitterTestBase
	{
		[TestMethod]
		public void RoundsStartTimeDown()
		{
			var startTime = GetDateTime(day: 1, hour: 18, minute: 10);
			var endTime = GetDateTime(day: 1, hour: 20);
			var bedTime = GetDateTime(day: 1, hour: 22);
			// Bedtime is after end time so pay should be $12 * 2 =  $24
			var expectedResult = 24.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void RoundsEndTimeUp(){
			var startTime = GetDateTime(day: 1, hour: 18);
			var endTime = GetDateTime(day: 1, hour: 19, minute: 6);
			var bedTime = GetDateTime(day: 1, hour: 22);
			// Bedtime is after end time so pay should be $12 * 2 = $24
			var expectedResult = 24.00;


			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		public void RoundsBedTimeUp(){
			var startTime = GetDateTime(day: 1, hour: 18);
			// 3 hours of pre-bed pay = 3 * $12 = $36
			var bedTime = GetDateTime(day: 1, hour: 20, minute: 10);
			// 2 hours of post-bed pay = 2 * $8 = $16
			var endTime = GetDateTime(day: 1, hour: 23);
			// Total pay = $36 + $16 = 52
			var expectedResult = 52.00;

			var objectUnderTest = new Babysitter();
			var result = objectUnderTest.GetPayForNight(startTime, endTime, bedTime);
			Assert.AreEqual(expectedResult, result);
		}
	}
}
