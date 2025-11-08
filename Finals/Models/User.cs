using System;

namespace Finals.Models
{
    public class User
    {
        public int Id { get; set; }

        // added Username to match login/signup UI
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public bool IsEmailVerified { get; set; }

        // new: role flag used to route after login/signup
        public bool IsAdmin { get; set; } = false;
    }
}
