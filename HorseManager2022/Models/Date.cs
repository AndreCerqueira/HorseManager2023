﻿using HorseManager2022.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseManager2022.Models
{
    // Enum
    public enum Month
    {
        Spring,
        Summer,
        Autumn,
        Winter,
    }

    /*
        In game date class, used to keep track of the date in game. Each year have 4 months, one for each season, each month have 30 days.
     */
    internal class Date
    {
        // Properties
        public int day;
        public Month month;
        public int year;

        // Constructor
        public Date(int day, Month month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        // Constructor for 1ºst day of the calendar
        public Date()
        {
            this.day = 1;
            this.month = Month.Spring;
            this.year = 1;
        }


        // Methods

        // Check if date is after a date
        static public bool IsDateBeforeDate(Date date1, Date date2)
        {
            if (date1.year < date2.year)
            {
                return true;
            }
            else if (date1.year == date2.year)
            {
                if (date1.month < date2.month)
                {
                    return true;
                }
                else if (date1.month == date2.month)
                {
                    if (date1.day < date2.day)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        
        // Check if date is the given month calendar page
        static public bool IsDateInThisMonth(Date date, Month month, int year) =>
            (date.year == year && date.month == month);
      

        /*
            Get the date as a string
         */
        public override string ToString()
        {
            string date = "";

            // Add year
            date += this.day + " ";

            // Add month
            date += ToString(month);

            // Add day
            date += " " + (this.year + Calendar.BASE_YEAR);
            
            return date;
        }
        
        static public string? ToString(Month month) => Enum.GetName(typeof(Month), month);

        public string ToSaveFormat() => this.day + ";" + this.month + ";" + this.year;
    }
}
