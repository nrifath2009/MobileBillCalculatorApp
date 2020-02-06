using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBillCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                double peakHourRate = 0.3;
                int pulseDurationInSecond = 20;

                Console.Write("Start time: ");
                string startTime = Console.ReadLine();

                Console.Write("End time: ");
                string endTime = Console.ReadLine();

                DateTime startTimeDate = DateTime.Parse(startTime);
                DateTime endTimeDate = DateTime.Parse(endTime);

                DateTime startTimeDateOnly = startTimeDate.Date;
                DateTime EndTimeDateOnly = endTimeDate.Date;

                List<MobileCallRate> mobileCallRates = new List<MobileCallRate>
                {
                    new MobileCallRate
                    {
                        StartTime = startTimeDateOnly.Date.Add(DateTime.Parse("9:00:00 am").TimeOfDay),
                        EndTime = EndTimeDateOnly.Date.Add(DateTime.Parse("10:59:59 pm").TimeOfDay),
                        Rate = 0.30,
                        DurationType = DurationTypeEnum.PeakHour
                    },
                    new MobileCallRate
                    {
                        StartTime = startTimeDateOnly.Date.Add(DateTime.Parse("12:00:00 am").TimeOfDay),
                        EndTime = EndTimeDateOnly.Date.Add(DateTime.Parse("08:59:59 am").TimeOfDay),
                        Rate = 0.20,
                        DurationType = DurationTypeEnum.OffPeakHour
                    },
                    new MobileCallRate
                    {
                        StartTime = startTimeDateOnly.Date.Add(DateTime.Parse("11:00:00 pm").TimeOfDay),
                        EndTime = EndTimeDateOnly.Date.Add(DateTime.Parse("11:59:59 pm").TimeOfDay),
                        Rate = 0.20,
                        DurationType = DurationTypeEnum.OffPeakHour
                    }
                };

                double callPrice = 0.0;
                double remainingTimeInSeconds = 0;
                do
                {
                    double callRate = 0;
                    remainingTimeInSeconds = (endTimeDate - startTimeDate).TotalSeconds;
                    double timeToCalculate = remainingTimeInSeconds >= pulseDurationInSecond ? pulseDurationInSecond : remainingTimeInSeconds;

                    if (timeToCalculate == 0)
                        break;

                    var callDurationHour = mobileCallRates.FirstOrDefault(c => c.StartTime <= startTimeDate.AddSeconds(timeToCalculate) && c.EndTime >= startTimeDate.AddSeconds(timeToCalculate));

                    bool isTimeWithinPeakHour = mobileCallRates.Any(c => c.StartTime <= startTimeDate.AddSeconds(timeToCalculate) && c.EndTime >= startTimeDate.AddSeconds(timeToCalculate) && c.DurationType == DurationTypeEnum.PeakHour);
                    bool isTimeWithinOffPeakHour = mobileCallRates.Any(c => c.StartTime <= startTimeDate.AddSeconds(timeToCalculate) && c.EndTime >= startTimeDate.AddSeconds(timeToCalculate) && c.DurationType == DurationTypeEnum.OffPeakHour);

                    if (isTimeWithinPeakHour && isTimeWithinOffPeakHour)
                    {
                        callRate = peakHourRate;
                    }
                    else
                    {
                        callRate = callDurationHour.Rate;
                    }

                    callPrice += callRate;

                    Console.WriteLine("Pulse: " + timeToCalculate + "Seconds, Rate: " + callRate * 100 + " Paisa, Duration: " + callDurationHour.DurationType.ToString());

                    startTimeDate = startTimeDate.AddSeconds(timeToCalculate);

                }while(remainingTimeInSeconds > 0);

                Console.WriteLine("--------------------------------------\n");
                Console.WriteLine("Call duration cost: " + callPrice + " Tk");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


    }
}
