using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Entities.Models
{
    public class SortReport
    {
        public int ArraySize { get; set; }
        public string SortType { get; set; }
        public double SecondsElapsed { get; set; }
        public double MillisecondsElapsed { get; set; }
        public int[] UnsortedArray { get; set; }
        public int[] SortedArray { get; set; }
    }
}
