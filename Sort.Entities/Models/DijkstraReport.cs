using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Entities.Models
{
    public class DijkstraReport
    {
        public int Vertex { get; set; }
        public int DistanceFromSource { get; set; }
        public List<int> Path { get; set; }
    }

    public class DijkstraFullReport
    {
        public double SecondsElapsed { get; set; }
        public double MillisecondsElapsed { get; set; }
        public List<DijkstraReport> Reports { get; set; }
    }
}
