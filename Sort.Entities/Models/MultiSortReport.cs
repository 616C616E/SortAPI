using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Entities.Models
{
    public class MultiSortReport
    {
        public SortReport BestIteration { get; set; }
        public SortReport MediumIteration { get; set; }
        public SortReport WorstIteration { get; set; }
    }
}
