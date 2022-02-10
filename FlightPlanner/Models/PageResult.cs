using System.Collections.Generic;

namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }

        public PageResult(List<Flight> input)
        {
            Page = 0;
            Items = input;
            TotalItems = input.Count;
            //new instance for every class initialization
        }
    }
}