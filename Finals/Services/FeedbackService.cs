using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Finals.Data;
using Finals.Models;

namespace Finals.Services
{
    public class FeedbackService
    {
        private readonly AppDbContext _db;
        public FeedbackService(AppDbContext db) => _db = db;

        public Task<List<Feedback>> GetAllAsync() =>
            _db.Feedbacks.OrderByDescending(f => f.CreatedAt).ToListAsync();

        public async Task AddAsync(Feedback f)
        {
            _db.Feedbacks.Add(f);
            await _db.SaveChangesAsync();
        }

        public Task<Feedback?> GetByIdAsync(int id) =>
            _db.Feedbacks.FindAsync(id).AsTask();

        public async Task ReplyAsync(int id, string reply)
        {
            var fb = await _db.Feedbacks.FindAsync(id);
            if (fb == null) return;
            fb.AdminReply = reply;
            fb.RepliedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}