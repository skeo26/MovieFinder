using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFinder.Database
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string ActorsText { get; set; }
        public string Director { get; set; }
        public double Fees { get; set; }
        public string Description { get; set; }
    }
}
