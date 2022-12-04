namespace NorthWoodsAssessment
{
	public class Babysitter
	{
		private const int _earliestStartTimeHours = 17;
		private const int _latestEndTimeHours = 4;

		public double GetPayForNight(DateTime startTime, DateTime endTime, DateTime bedTime)
		{
			const double preBedTimePay = 12.00;
			const double postBedTimePay = 8.00;
			const double postMidnightPay = 16.00;


			DateTime roundedStartTime = RoundStartTime(startTime);
			DateTime roundedEndTime = RoundEndtime(endTime);
			DateTime roundedBedTime = RoundBedTime(bedTime);

			ValidateTimes(roundedStartTime, roundedEndTime);

			bool asleepAtStart = false;
			bool ignoreBedtime = false;
			if (bedTime < startTime)
			{
				asleepAtStart = true;
			}

			if (bedTime > roundedEndTime)
			{
				ignoreBedtime = true;
			}

			DateTime midnight = GetMidnight(roundedStartTime);

			var startToBed = roundedBedTime.Subtract(roundedStartTime).Hours;
			var bedToMidnightHours = midnight.Subtract(roundedBedTime).Hours;
			var midnightToEndHours = roundedEndTime.Subtract(midnight).Hours;
			var startToMidnightHours = midnight.Subtract(roundedStartTime).Hours;
			var startToEndHours = roundedEndTime.Subtract(roundedStartTime).Hours;
			var bedToEndHours = roundedEndTime.Subtract(roundedBedTime).Hours;

			if (roundedStartTime > midnight)
			{
				return startToEndHours * postMidnightPay;
			}

			if (asleepAtStart)
			{
				if (roundedEndTime < midnight)
				{
					return startToEndHours * postBedTimePay;
				}
				else
				{
					return startToMidnightHours * postBedTimePay + (midnightToEndHours > 0 ? midnightToEndHours * postMidnightPay : 0);
				}
			}

			if (ignoreBedtime)
			{
				if (roundedEndTime <= midnight)
				{
					return startToEndHours * preBedTimePay;
				}
				return startToMidnightHours * preBedTimePay + midnightToEndHours * postMidnightPay;
			}
			else
			{
				if (roundedEndTime <= midnight)
				{
					return startToBed * preBedTimePay + bedToEndHours * postBedTimePay;
				}
				else
				{
					if (bedTime > midnight)
					{
						return startToMidnightHours * preBedTimePay + midnightToEndHours * postMidnightPay;
					}
					return startToBed * preBedTimePay + bedToMidnightHours * postBedTimePay + midnightToEndHours * postMidnightPay;
				}

			}
		}

		private static DateTime GetMidnight(DateTime roundedStartTime)
		{
			DateTime midnight;
			if (roundedStartTime.Hour >= _earliestStartTimeHours)
			{
				// starts in the evening.
				midnight = new DateTime(
					roundedStartTime.Year,
					roundedStartTime.Month,
					roundedStartTime.Day + 1,
					hour: 0,
					minute: 0,
					second: 0
				);
			}
			else
			{
				// starts postMidnight
				midnight = new DateTime(
					roundedStartTime.Year,
					roundedStartTime.Month,
					roundedStartTime.Day,
					hour: 0,
					minute: 0,
					second: 0
				);
			}

			return midnight;
		}

		private static DateTime RoundStartTime(DateTime startTime)
		{
			var roundedStartTime = startTime;

			if (startTime.Minute > 0)
			{
				roundedStartTime = startTime.AddMinutes(-1 * startTime.Minute);
			}

			return roundedStartTime;
		}

		private static DateTime RoundBedTime(DateTime bedTime)
		{
			var roundedBedTime = bedTime;
			if (bedTime.Minute > 0)
			{
				roundedBedTime = bedTime.AddMinutes(-1 * bedTime.Minute);
				roundedBedTime = roundedBedTime.AddHours(1);
			}

			return roundedBedTime;
		}

		private static DateTime RoundEndtime(DateTime endTime)
		{
			var roundedEndTime = endTime;
			if (endTime.Minute > 0)
			{
				roundedEndTime = endTime.AddMinutes(-1 * endTime.Minute);
				roundedEndTime = roundedEndTime.AddHours(1);
			}

			return roundedEndTime;
		}

		private static void ValidateTimes(DateTime startTime, DateTime endTime)
		{
			if (startTime.Hour < _earliestStartTimeHours && startTime.Hour > _latestEndTimeHours)
			{
				throw new ArgumentOutOfRangeException(nameof(startTime), $"{nameof(startTime)} must happen at or after 5pm.");
			}

			if (endTime.Hour > 4 && endTime.Hour < _earliestStartTimeHours)
			{
				throw new ArgumentOutOfRangeException(nameof(endTime), $"{nameof(endTime)} must happen before 4am and after 5pm.");
			}

			if (startTime > endTime)
			{
				throw new InvalidOperationException($"{nameof(startTime)} must be before {nameof(endTime)}");
			}
		}
	}
}
