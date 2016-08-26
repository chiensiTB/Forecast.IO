using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

using ForecastIOPortable;
using Newtonsoft.Json;
using ForecastIOPortable.Models;

namespace Forecast_IO
{
    class Program
    {
        public const string API_KEY = "0ec5e42cf88237150472e4a1ec516bb1";
        static void Main(string[] args)
        {
            //Toronto City Center
            double lat = 43.6534405;
            double lng = -79.3845036;


            SimpleTime start = new SimpleTime(2015, 1, 1);
            SimpleTime end = new SimpleTime(2015, 12, 31);
            var hourlyData = GetHourlyWeatherOverPeriod(start, end, lat, lng);
            Console.WriteLine("Hourly data lenght {0}", hourlyData.Count);
            
        }

        public static List<ForecastHourly> GetHourlyWeatherOverPeriod(SimpleTime start, SimpleTime end, double lat, double lng)
        {
            LocalDateTime begin = new LocalDateTime(start.Year, start.Month, start.Day, 0, 0);
            LocalDateTime stop = new LocalDateTime(end.Year, end.Month, end.Day, 0, 0);
            Period period = Period.Between(begin, stop, PeriodUnits.Days);

            List<ForecastHourly> hourlyData = new List<ForecastHourly>();
            for(int i = 0; i <= period.Days; i++)
            {
                LocalDateTime localtime = begin.PlusDays(i);

                string timezone = "America/Toronto";
                DateTimeZone tz = DateTimeZoneProviders.Tzdb[timezone];

                ZonedDateTime zt = tz.AtLeniently(localtime);
                var tz_offset = zt.ToDateTimeOffset();

                Task.Run(async () =>
                {
                    var client = new ForecastApi(API_KEY);
                    Forecast result = await client.GetTimeMachineWeatherAsync(lat, lng, tz_offset);
                    var hourly = result.Hourly.Hours;
                    for(int h = 0; h < hourly.Count(); h++)
                    {
                        ForecastHourly store_hourly = new ForecastHourly();
                        store_hourly.ApparentTemperature = hourly[h].ApparentTemperature;
                        store_hourly.CloudCover = hourly[h].CloudCover;
                        store_hourly.DewPoint = hourly[h].DewPoint;
                        store_hourly.Humidity = hourly[h].Humidity;
                        store_hourly.Icon = hourly[h].Icon;
                        store_hourly.Ozone = hourly[h].Ozone;
                        store_hourly.PrecipitationIntensity = hourly[h].PrecipitationIntensity;
                        store_hourly.PrecipitationProbability = hourly[h].PrecipitationProbability;
                        store_hourly.Pressure = hourly[h].Pressure;
                        store_hourly.Summary = hourly[h].Summary;
                        store_hourly.Temperature = hourly[h].Temperature;
                        store_hourly.Time = hourly[h].Time;
                        //Instant instant = Instant.FromDateTimeUtc(hourly[h].Time.UtcDateTime); //don't need this, already defined above
                        store_hourly.LocalTime = zt.LocalDateTime;
                        store_hourly.Visibility = hourly[h].Visibility;
                        store_hourly.WindBearing = hourly[h].WindBearing;
                        store_hourly.WindSpeed = hourly[h].WindSpeed;
                        hourlyData.Add(store_hourly);
                    } 
                }).Wait();
            }
            return hourlyData;
        }
    }
}
