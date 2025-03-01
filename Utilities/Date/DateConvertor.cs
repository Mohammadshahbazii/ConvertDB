using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class DateConvertor
    {
        public static string GetPersianMonth(int number)
        {

            string month = "";
            switch (number)
            {
                case 1:
                    month = "فروردین";
                    break;
                case 2:
                    month = "اردیبهشت";
                    break;
                case 3:
                    month = "خرداد";
                    break;
                case 4:
                    month = "تیر";
                    break;
                case 5:
                    month = "مرداد";
                    break;
                case 6:
                    month = "شهریور";
                    break;
                case 7:
                    month = "مهر";
                    break;
                case 8:
                    month = "آبان";
                    break;
                case 9:
                    month = "آذر";
                    break;
                case 10:
                    month = "دی";
                    break;
                case 11:
                    month = "بهمن";
                    break;
                case 12:
                    month = "اسفند";
                    break;
            }
            return month;
        }

        public static string GetPersianDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            string DayName = "";
            if (date.DayOfWeek == DayOfWeek.Friday)
                DayName = "جمعه";
            else if (date.DayOfWeek == DayOfWeek.Saturday)
                DayName = "شنبه";
            else if (date.DayOfWeek == DayOfWeek.Sunday)
                DayName = "یکشنبه";
            else if (date.DayOfWeek == DayOfWeek.Monday)
                DayName = "دوشنبه";
            else if (date.DayOfWeek == DayOfWeek.Tuesday)
                DayName = "سه شنبه";
            else if (date.DayOfWeek == DayOfWeek.Wednesday)
                DayName = "چهار شنبه";
            else if (date.DayOfWeek == DayOfWeek.Thursday)
                DayName = "پنج شنبه";

            string result = DayName + "، " + pc.GetDayOfMonth(date).ToString("00") + " ";
            switch (pc.GetMonth(date))
            {
                case 1:
                    result = result + "فروردین ماه";
                    break;
                case 2:
                    result = result + "اردیبهشت ماه";
                    break;
                case 3:
                    result = result + "خرداد ماه";
                    break;
                case 4:
                    result = result + "تیر ماه";
                    break;
                case 5:
                    result = result + "مرداد ماه";
                    break;
                case 6:
                    result = result + "شهریور ماه";
                    break;
                case 7:
                    result = result + "مهر ماه";
                    break;
                case 8:
                    result = result + "آبان ماه";
                    break;
                case 9:
                    result = result + "آذر ماه";
                    break;
                case 10:
                    result = result + "دی ماه";
                    break;
                case 11:
                    result = result + "بهمن ماه";
                    break;
                case 12:
                    result = result + "اسفند ماه";
                    break;
            }
            result += " " + pc.GetYear(date) + " ";
            return result;
        }

        public static string PassDays(DateTime dateTime)
        {
            DateTime today = DateTime.Now.Date;
            int passYears = today.Year - dateTime.Year;
            int passDays = (today.DayOfYear - dateTime.DayOfYear) + (passYears * 365);
            if (passDays == 0)
            {
                return "امروز";
            }
            return $"{passDays} روز قبل";
        }

        public static string ToShamsi(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        public static string ToShamsiDetailed(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00") +
                " - " + pc.GetHour(date).ToString("00") + ":" + pc.GetMinute(date).ToString("00") + ":" + pc.GetSecond(date).ToString("00");
        }

        public static DateTime ToShamsiDate(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dateTime = new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));

            return dateTime.Date;
        }

        public static string ToShamsi(DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            else
            {
                DateTime time = Convert.ToDateTime(date);
                return ToShamsi(time);
            }
        }

        public static DateTime ToMiladi(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, new PersianCalendar());
        }

        public static DateTime ToMiladi(string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());
        }

        public static string GetLastDayOfCurrentYear()
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime now = DateTime.Now;

            // Get the current Persian year
            int persianYear = persianCalendar.GetYear(now);

            // Determine if it's a leap year in the Persian calendar
            bool isLeapYear = persianCalendar.IsLeapYear(persianYear);

            // Get the last day of the year
            int lastMonth = 12;
            int lastDay = isLeapYear ? 30 : 29;

            // Create a DateTime object for the last day of the Persian year
            DateTime lastDayOfYear = persianCalendar.ToDateTime(persianYear, lastMonth, lastDay, 0, 0, 0, 0);

            // Format the date as a string in the desired format
            string formattedDate = $"{persianYear}/{lastMonth:D2}/{lastDay:D2}";

            return formattedDate;
        }

        public static bool ComparePersianDates(string date1, string date2)
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();

                // Parse the first date
                DateTime parsedDate1 = ToMiladi(date1);

                // Parse the second date
                DateTime parsedDate2 = ToMiladi(date2);

                int compareResult = DateTime.Compare(parsedDate1, parsedDate2);

                // Compare the two dates
                if (compareResult <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetLocalDate()
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime dateTime = new DateTime(pc.GetYear(DateTime.Now), pc.GetMonth(DateTime.Now), pc.GetDayOfMonth(DateTime.Now));

            return dateTime.Year.ToString() + dateTime.Month.ToString().PadLeft(2, '0') +
                   dateTime.Day.ToString().PadLeft(2, '0');
        }


        public static string GetDate()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                   DateTime.Now.Day.ToString().PadLeft(2, '0');
        }

        public static string GetTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                   DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
    }
}
