using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Finals.Models;

namespace Finals.Services
{
    public class AuthService
    {
        // keyed by email for uniqueness; lookups support username or email
        private readonly ConcurrentDictionary<string, User> _usersByEmail = new();
        private readonly ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        // Login by username or email
        public async Task<User?> LoginUserAsync(string usernameOrEmail, string password)
        {
            await Task.Delay(10); // simulate async

            var user = _usersByEmail.Values.FirstOrDefault(u =>
                string.Equals(u.Email, usernameOrEmail, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(u.Username, usernameOrEmail, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                _logger.LogWarning("Failed login: user not found for {Identifier}", usernameOrEmail);
                return null;
            }

            if (VerifyPassword(user.PasswordHash, password))
            {
                _logger.LogInformation("User {Username} logged in successfully", user.Username);
                return user;
            }

            _logger.LogWarning("Failed login attempt for {Identifier}", usernameOrEmail);
            return null;
        }

        // Register with username + email + password
        // added isAdmin parameter (default false)
        public async Task<bool> RegisterUserAsync(string username, string email, string password, bool isAdmin = false)
        {
            await Task.Delay(10); // simulate async

            if (_usersByEmail.Values.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)))
            {
                _logger.LogWarning("Registration failed: Username {Username} already exists", username);
                return false;
            }

            if (_usersByEmail.ContainsKey(email.ToLowerInvariant()))
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists", email);
                return false;
            }

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = CreatePasswordHash(password),
                CreatedAtUtc = DateTime.UtcNow,
                IsEmailVerified = false,
                IsAdmin = isAdmin
            };

            var added = _usersByEmail.TryAdd(email.ToLowerInvariant(), newUser);
            if (added)
            {
                _logger.LogInformation("User {Username} registered successfully (IsAdmin={IsAdmin})", username, isAdmin);
            }
            else
            {
                _logger.LogWarning("Registration failed: concurrency issue for {Email}", email);
            }

            return added;
        }

        // ---------------------
        // Password hashing helpers (PBKDF2)
        // Stored format: iterations.saltBase64.hashBase64
        // ---------------------
        private static string CreatePasswordHash(string password)
        {
            const int iterations = 100_000;
            const int saltSize = 16;
            const int hashSize = 32;

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(hashSize);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string storedHash, string password)
        {
            try
            {
                var parts = storedHash.Split('.', 3);
                if (parts.Length != 3) return false;
                var iterations = int.Parse(parts[0]);
                var salt = Convert.FromBase64String(parts[1]);
                var hash = Convert.FromBase64String(parts[2]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                var computed = pbkdf2.GetBytes(hash.Length);

                return CryptographicOperations.FixedTimeEquals(computed, hash);
            }
            catch
            {
                return false;
            }
        }
    }
}
