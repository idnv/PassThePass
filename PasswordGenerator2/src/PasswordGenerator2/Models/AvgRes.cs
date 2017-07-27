using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator2.Models
{
    public class AvgRes
    {
        public int ID { get; set; }
        public string mail { get; set; }
        public int current_correctWords { get; set; }

        public int current_numOfWriitenWords { get; set; }
        public float current_accuracy { get; set; }
        public int current_numOfEntryErrors { get; set; }
        public int current_count { get; set; }
        public decimal current_time { get; set; }

        public int previous_correctWords { get; set; }

        public int previous_numOfWriitenWords { get; set; }
        public float previous_accuracy { get; set; }
        public int previous_numOfEntryErrors { get; set; }
        public int previous_count { get; set; }
        public decimal previous_time { get; set; }

        public AvgRes()
        {
            previous_count = 0;
            previous_correctWords = 0;
            previous_accuracy = 0;
            previous_numOfEntryErrors = 0;
            previous_numOfWriitenWords = 0;
            current_count = 0;
            current_time = 0;
            current_correctWords = 0;
            current_accuracy = 0;
            current_numOfEntryErrors = 0;
            current_numOfWriitenWords = 0;
            previous_time = 0;
            mail = "";

        }
    }
}
