using System;

namespace Finals.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Author { get; set; } = "";
        public string Comment { get; set; } = "";
        public int Star { get; set; }        // 1..5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Admin reply fields
        public string? AdminReply { get; set; }
        public DateTime? RepliedAt { get; set; }
    }
}