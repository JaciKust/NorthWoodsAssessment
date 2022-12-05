using System.Linq;

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

			DateTime midnight = GetMidnight(roundedStartTime);

			var timeLine = new Dictionary<TimeName, DateTime>(){
				{ TimeName.Start, roundedStartTime },
				{ TimeName.End, roundedEndTime},
				{ TimeName.Bed, roundedBedTime },
				{ TimeName.Midnight, midnight },
			}.OrderBy(x => x.Value);

			double totalPay = 0;
			
			bool isBedTime = false;
			bool isMidnight = false;
			bool isStarted = false;

			double currentPayRate = GetCurrentPayRate(isMidnight, isBedTime);
			for (int i = 0; i < timeLine.Count(); i++)
			{
				var key = timeLine.ElementAt(i).Key;
				var value = timeLine.ElementAt(i).Value;

				bool hasPreviousTime = i > 0;
				DateTime previousTime = DateTime.Now;
				if (hasPreviousTime)
				{
					previousTime = timeLine.ElementAt(i - 1).Value;
				}

				switch (key)
				{
					case TimeName.Start:
						isStarted = true;
						break;
					case TimeName.Midnight:
						if (isStarted)
						{
							totalPay += GetPay(isBedTime, isMidnight, value, previousTime);
						}
						isMidnight = true;

						break;
					case TimeName.Bed:
						if (isStarted)
						{
							totalPay += GetPay(isBedTime, isMidnight, value, previousTime);
						}
						isBedTime = true;
						break;
					case TimeName.End:
						totalPay += GetPay(isBedTime, isMidnight, value, previousTime);
						return totalPay;
				}
			}

			throw new Exception("No end found");
		}

		private double GetPay(bool isBedTime, bool isMidnight, DateTime value, DateTime previousTime)
		{
			var timePassed = value.Subtract(previousTime).Hours;
			return timePassed * GetCurrentPayRate(isMidnight, isBedTime);
		}

		private double GetCurrentPayRate(bool isMidnight, bool isBedTime){
			if (isMidnight)
				return 16;
			if (isBedTime)
				return 8;
			return 12;
		}

		private enum TimeName
		{
			Start,
			Bed,
			Midnight,
			End
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
