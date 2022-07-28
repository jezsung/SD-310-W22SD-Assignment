using System;
using System.Collections.Generic;

namespace SD_310_W22SD_Assignment.Models
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Purchases = new HashSet<Purchase>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Money { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
