
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMovieFinder.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public double Fees { get; set; }

        [Required]
        public string Description { get; set; }

        public string ActorsText { get; set; } = "Нет данных";

        public override bool Equals(object obj)
        {
            return obj is Movie movie &&
                   Id == movie.Id &&
                   Title == movie.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title);
        }

    }
}
