﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSPP.Models
{
    public class MonthModel
    {
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string Mac { get; set; }
        public string Apr { get; set; }
        public string Mei { get; set; }
        public string Jun { get; set; }
        public string Julai { get; set; }
        public string Ogos { get; set; }
        public string Sept { get; set; }
        public string Okt { get; set; }
        public string Nov { get; set; }
        public string Dis { get; set; }
    }

    public class BonusSambilanMonthModel
    {
        public int Nombor { get; set; }
        public int MonthNumber { get; set; }

        public string MonthName
        {
            get
            {
                string _monthName;
                switch (MonthNumber)
                {
                    case (1):
                        _monthName = "Jan";
                        break;
                    case (2):
                        _monthName = "Feb";
                        break;
                    case (3):
                        _monthName = "Mac";
                        break;
                    case (4):
                        _monthName = "Apr";
                        break;
                    case (5):
                        _monthName = "Mei";
                        break;
                    case (6):
                        _monthName = "Jun";
                        break;
                    case (7):
                        _monthName = "Jul";
                        break;
                    case (8):
                        _monthName = "Ogos";
                        break;
                    case (9):
                        _monthName = "Sept";
                        break;
                    case (10):
                        _monthName = "Okt";
                        break;
                    case (11):
                        _monthName = "Nov";
                        break;
                    case (12):
                        _monthName = "Dis";
                        break;
                    default:
                        _monthName = string.Empty;
                        break;
                }
                return _monthName;
            }
        }
        public int MonthValue { get; set; }
    }

}