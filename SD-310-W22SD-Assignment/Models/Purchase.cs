using System;
using System.Collections.Generic;

namespace SD_310_W22SD_Assignment.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime PurchasedAt { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
