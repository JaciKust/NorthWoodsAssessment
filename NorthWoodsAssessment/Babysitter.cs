namespace NorthWoodsAssessment
{
	public class Babysitter
	{
		public double GetPayForNight(DateTime startTime, DateTime endTime, DateTime bedTime)
		{

			const int earliestStartTimeHours = 17;
			const int latestEndTimeHours = 4;
			const double preBedTimePay = 12.00;
			const double postBedTimePay = 8.00;
			const double postMidnightPay = 16.00;

			var roundedStartTime = startTime;

			if (startTime.Minute > 0)
			{
				roundedStartTime = startTime.AddMinutes(-1 * startTime.Minute);
			}
			else
			{
			}

			var roundedEndTime = endTime;
			if (endTime.Minute > 0)
			{
				roundedEndTime = endTime.AddMinutes(-1 * endTime.Minute);
				roundedEndTime = roundedEndTime.AddHours(1);
			}

			var roundedBedTime = bedTime;
			if (bedTime.Minute > 0)
			{
				roundedBedTime = bedTime.AddMinutes(-1 * bedTime.Minute);
				roundedBedTime = roundedBedTime.AddHours(1);
			}

			if (roundedStartTime.Hour < earliestStartTimeHours && roundedStartTime.Hour > latestEndTimeHours)
			{
				throw new ArgumentOutOfRangeException(nameof(startTime), $"{nameof(startTime)} must happen at or after 5pm.");
			}

			if (roundedEndTime.Hour > 4 && roundedEndTime.Hour < earliestStartTimeHours)
			{
				throw new ArgumentOutOfRangeException(nameof(endTime), $"{nameof(endTime)} must happen before 4am and after 5pm.");
			}

			if (roundedStartTime > roundedEndTime)
			{
				throw new InvalidOperationException($"{nameof(startTime)} must be before {nameof(endTime)}");
			}

			if (bedTime < startTime)
			{
				roundedBedTime = startTime;
			}

			const int hoursInADay = 24;
			var startToBed = roundedBedTime.Subtract(roundedStartTime).Hours;
			var startToEnd = roundedEndTime.Subtract(roundedStartTime).Hours;
			var BedToEnd = roundedEndTime.Subtract(roundedBedTime).Hours;
			var BedToMidnight = new TimeSpan(hours: hoursInADay - roundedBedTime.Hour, minutes: 0, seconds: 0).Hours;
			var StartToMidnight = new TimeSpan(hours: hoursInADay - roundedStartTime.Hour, minutes: 0, seconds: 0).Hours;
			var MidnightToEnd = endTime.Hour < 4 ? new TimeSpan(hours: endTime.Hour, minutes: 0, seconds: 0).Hours : 0;

			if (endTime.Hour < earliestStartTimeHours)
			{
				// Ends after midnight
				if (bedTime.Hour < earliestStartTimeHours)
				{
					// bedtime is after midnight and we don't care about it
					return StartToMidnight * postBedTimePay + MidnightToEnd * postMidnightPay;
				}
				else
				{
					// bedtime is before midnight
					return startToBed * preBedTimePay + BedToMidnight * postBedTimePay + MidnightToEnd * postMidnightPay;
				}
			}
			else
			{
				// Ends before midnight
				if (bedTime >= endTime)
				{
					// Bedtime is after end time and we don't care about it. 
					return startToEnd * preBedTimePay;
				}
				else
				{
					// have a bed time we care about 
					return startToBed * preBedTimePay + BedToEnd * postBedTimePay;
				}
			}
		}
	}
}
