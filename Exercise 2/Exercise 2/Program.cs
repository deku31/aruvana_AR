using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string waktu12Jam = "11:30:00PM";
            string waktu24Jam = timeConversion(waktu12Jam);
            Console.WriteLine(waktu24Jam);
        }

        public static string timeConversion(string s)
        {
            string jamString = s.Substring(0, 2);
            int jam = Int32.Parse(jamString);

            if (s.Contains("PM") && jam != 12)
            {
                jam += 12;
            }
            else if (s.Contains("AM") && jam == 12)
            {
                jam = 0;
            }

            string jam24String = jam.ToString("D2");
            string menitDetik = s.Substring(2, 6);

            return jam24String + menitDetik;
        }
    }
}
