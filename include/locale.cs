using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using runnerDotNet;
namespace runnerDotNet
{
	public partial class CommonFunctions
	{
		public static void InitLocale()
		{
			// locale settings

			GlobalVars.locale_info = new XVar();

			GlobalVars.locale_info["LOCALE_LANGNAME"] = "en";
			GlobalVars.locale_info["LOCALE_CTRYNAME"] = "US";
			// date settings
			GlobalVars.locale_info["LOCALE_ICENTURY"] = "1";
			GlobalVars.locale_info["LOCALE_IDATE"] = "0";
			GlobalVars.locale_info["LOCALE_ILDATE"] = "0";
			GlobalVars.locale_info["LOCALE_SDATE"] = "/";
			GlobalVars.locale_info["LOCALE_SLONGDATE"] = "dddd, MMMM d, yyyy";
			GlobalVars.locale_info["LOCALE_SSHORTDATE"] = "M/d/yyyy";
			// weekday names
			GlobalVars.locale_info["LOCALE_IFIRSTDAYOFWEEK"] = "6";
			GlobalVars.locale_info["LOCALE_SDAYNAME1"] = "Monday";
			GlobalVars.locale_info["LOCALE_SDAYNAME2"] = "Tuesday";
			GlobalVars.locale_info["LOCALE_SDAYNAME3"] = "Wednesday";
			GlobalVars.locale_info["LOCALE_SDAYNAME4"] = "Thursday";
			GlobalVars.locale_info["LOCALE_SDAYNAME5"] = "Friday";
			GlobalVars.locale_info["LOCALE_SDAYNAME6"] = "Saturday";
			GlobalVars.locale_info["LOCALE_SDAYNAME7"] = "Sunday";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME1"] = "Mon";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME2"] = "Tue";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME3"] = "Wed";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME4"] = "Thu";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME5"] = "Fri";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME6"] = "Sat";
			GlobalVars.locale_info["LOCALE_SABBREVDAYNAME7"] = "Sun";
			// month names
			GlobalVars.locale_info["LOCALE_SMONTHNAME1"] = "January";
			GlobalVars.locale_info["LOCALE_SMONTHNAME2"] = "February";
			GlobalVars.locale_info["LOCALE_SMONTHNAME3"] = "March";
			GlobalVars.locale_info["LOCALE_SMONTHNAME4"] = "April";
			GlobalVars.locale_info["LOCALE_SMONTHNAME5"] = "May";
			GlobalVars.locale_info["LOCALE_SMONTHNAME6"] = "June";
			GlobalVars.locale_info["LOCALE_SMONTHNAME7"] = "July";
			GlobalVars.locale_info["LOCALE_SMONTHNAME8"] = "August";
			GlobalVars.locale_info["LOCALE_SMONTHNAME9"] = "September";
			GlobalVars.locale_info["LOCALE_SMONTHNAME10"] = "October";
			GlobalVars.locale_info["LOCALE_SMONTHNAME11"] = "November";
			GlobalVars.locale_info["LOCALE_SMONTHNAME12"] = "December";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME1"] = "Jan";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME2"] = "Feb";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME3"] = "Mar";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME4"] = "Apr";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME5"] = "May";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME6"] = "Jun";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME7"] = "Jul";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME8"] = "Aug";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME9"] = "Sep";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME10"] = "Oct";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME11"] = "Nov";
			GlobalVars.locale_info["LOCALE_SABBREVMONTHNAME12"] = "Dec";
			// time settings
			GlobalVars.locale_info["LOCALE_ITIME"] = "0";
			GlobalVars.locale_info["LOCALE_ITIMEMARKPOSN"] = "0";
			GlobalVars.locale_info["LOCALE_ITLZERO"] = "0";
			GlobalVars.locale_info["LOCALE_S1159"] = "AM";
			GlobalVars.locale_info["LOCALE_S2359"] = "PM";
			GlobalVars.locale_info["LOCALE_STIME"] = ":";
			GlobalVars.locale_info["LOCALE_STIMEFORMAT"] = "h:mm:ss tt";
			// currency settings
			GlobalVars.locale_info["LOCALE_ICURRDIGITS"] = "2";
			GlobalVars.locale_info["LOCALE_ICURRENCY"] = "0";
			GlobalVars.locale_info["LOCALE_INEGCURR"] = "0";
			GlobalVars.locale_info["LOCALE_SCURRENCY"] = "$";
			GlobalVars.locale_info["LOCALE_SMONDECIMALSEP"] = ".";
			GlobalVars.locale_info["LOCALE_SMONGROUPING"] = "3;0";
			GlobalVars.locale_info["LOCALE_SMONTHOUSANDSEP"] = ",";
			// numbers formatting settings
			GlobalVars.locale_info["LOCALE_IDIGITS"] = "2";
			GlobalVars.locale_info["LOCALE_INEGNUMBER"] = "1";
			GlobalVars.locale_info["LOCALE_SDECIMAL"] = ".";
			GlobalVars.locale_info["LOCALE_SGROUPING"] = "3;0";
			GlobalVars.locale_info["LOCALE_SNEGATIVESIGN"] = "-";
			GlobalVars.locale_info["LOCALE_SPOSITIVESIGN"] = "";
			GlobalVars.locale_info["LOCALE_STHOUSAND"] = ",";

			GlobalVars.locale_info.InitAndSetArrayItem(CommonFunctions.GetLongDateFormat(), "LOCALE_ILONGDATE");
		}
		
		public static XVar db2time(XVar _param_str)
		{
			#region pass-by-value parameters
			XVar str = _param_str.Clone();
			#endregion

			int day, hour, minute, month, second, year;
            bool havedate = false, 
                havetime = false,
                isdst = DateTime.Now.IsDaylightSavingTime();

            if (str.Type == typeof(DateTime))
            {
                year = ((DateTime)str.Value).Year;
                month = ((DateTime)str.Value).Month;
                day = ((DateTime)str.Value).Day;
                hour = ((DateTime)str.Value).Hour;
                minute = ((DateTime)str.Value).Minute;
                second = ((DateTime)str.Value).Second;
                havedate = true;
                havetime = hour > 0 || minute > 0 || second > 0;
            }
            else
            {
                if (str.IsNumeric())
                {
                    XVar len;
                    string pattern;
                    havedate = true;
                    len = str.Length();
                    if (10 <= len)
                    {
                        havetime = true;
                    }
                    switch (len.ToInt())
                    {
                        case 14:
                            pattern = "(\\d{4})(\\d{2})(\\d{2})(\\d{2})(\\d{2})(\\d{2})";
                            break;
                        case 12:
                            pattern = "(\\d{2})(\\d{2})(\\d{2})(\\d{2})(\\d{2})(\\d{2})";
                            break;
                        case 10:
                            pattern = "(\\d{2})(\\d{2})(\\d{2})(\\d{2})(\\d{2})";
                            break;
                        case 18:
                            pattern = "(\\d{4})(\\d{2})(\\d{2})";
                            break;
                        case 6:
                            pattern = "(\\d{2})(\\d{2})(\\d{2})";
                            break;
                        case 4:
                            pattern = "(\\d{2})(\\d{2})";
                            break;
                        case 2:
                            pattern = "(\\d{2})";
                            break;
                        default:
                            return new XVar();
                    }
                    Match parsed = Regex.Match(str, pattern);
                    if (parsed.Groups[1].Value != "")
                    {
                        year = int.Parse(parsed.Groups[1].Value);
                        month = parsed.Groups.Count > 2 ? int.Parse(parsed.Groups[2].Value) : 1;
                        day = parsed.Groups.Count > 3 ? int.Parse(parsed.Groups[3].Value) : 1;
                        hour = parsed.Groups.Count > 4 ? int.Parse(parsed.Groups[4].Value) : 0;
                        minute = parsed.Groups.Count > 5 ? int.Parse(parsed.Groups[5].Value) : 0;
                        second = parsed.Groups.Count > 6 ? int.Parse(parsed.Groups[6].Value) : 0;
                    }
                    else
                    {
                        return new XVar();
                    }
                }
                else
                {
                    if (str.IsString())
                    {
                        if (Regex.IsMatch(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2}) (\\d{1,2}):(\\d{1,2}):(\\d{1,2})"))
                        {
                            Match parsed = Regex.Match(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2}) (\\d{1,2}):(\\d{1,2}):(\\d{1,2})");
                            year = int.Parse(parsed.Groups[1].Value);
                            month = int.Parse(parsed.Groups[2].Value);
                            day = int.Parse(parsed.Groups[3].Value);
                            hour = int.Parse(parsed.Groups[4].Value);
                            minute = int.Parse(parsed.Groups[5].Value);
                            second = int.Parse(parsed.Groups[6].Value);
                            havedate = true;
                            havetime = true;
                        }
                        else
                        {
                            if (Regex.IsMatch(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2})"))
                            {
                                Match parsed = Regex.Match(str, "(\\d{4})-(\\d{1,2})-(\\d{1,2})");
                                year = int.Parse(parsed.Groups[1].Value);
                                month = int.Parse(parsed.Groups[2].Value);
                                day = int.Parse(parsed.Groups[3].Value);
                                hour = 0;
                                minute = 0;
                                second = 0;
                                havedate = true;
                            }
                            else
                            {
                                if (Regex.IsMatch(str, "(\\d{2})-(\\d{1,2})-(\\d{1,2})"))
                                {
                                    Match parsed = Regex.Match(str, "(\\d{2})-(\\d{1,2})-(\\d{1,2})");
                                    year = DateTime.Now.Year;
                                    month = DateTime.Now.Month;
                                    day = DateTime.Now.Day;
                                    hour = int.Parse(parsed.Groups[1].Value);
                                    minute = int.Parse(parsed.Groups[2].Value);
                                    second = int.Parse(parsed.Groups[3].Value);
                                    havetime = true;
                                }
                                else
                                {
                                    return new XVar();
                                }
                            }
                        }
                    }
                    else
                    {
                        return new XVar();
                    }
                }
            }
			if(!havetime)
			{
				hour = 0;
				minute = 0;
				second = 0;
			}
			if(!havedate)
			{
				year = DateTime.Now.Year;
				month = DateTime.Now.Month;
				day = DateTime.Now.Day;
			}
			return new XVar(0, year, 1, month, 2, day, 3, hour, 4, minute, 5, second);
		}
	}
}
