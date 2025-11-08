using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Finals.Data;
using Finals.Models;

namespace Finals.Services
{
    public class OrderService
    {
        private readonly AppDbContext _db;
        public OrderService(AppDbContext db) => _db = db;

        public Task<List<Order>> GetAllAsync() =>
            _db.Orders.OrderByDescending(o => o.CreatedAt).ToListAsync();

        public Task<Order?> GetByIdAsync(int id) =>
            _db.Orders.FindAsync(id).AsTask();

        // Return the created order so caller can use the result
        public async Task<Order> AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task UpdateStatusAsync(int id, OrderStatus status, string? note = null)
        {
            var o = await _db.Orders.FindAsync(id);
            if (o == null) return;
            o.Status = status;
            if (note != null) o.AdminNote = note;
            o.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePaymentStatusAsync(int id, PaymentStatus pstatus)
        {
            var o = await _db.Orders.FindAsync(id);
            if (o == null) return;
            o.PaymentStatus = pstatus;
            o.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}