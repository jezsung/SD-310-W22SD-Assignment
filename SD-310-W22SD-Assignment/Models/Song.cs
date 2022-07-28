using System;
using System.Collections.Generic;

namespace SD_310_W22SD_Assignment.Models
{
    public partial class Song
    {
        public Song()
        {
            Collections = new HashSet<Collection>();
            Purchases = new HashSet<Purchase>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }
        public int Price { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
