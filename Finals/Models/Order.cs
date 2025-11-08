using System;

namespace Finals.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Completed,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

    public class Order
    {
        // primary key (int matches typical EF conventions)
        public int Id { get; set; }

        // fields expected by the customer/admin UI
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        // serialized items or description
        public string ItemsJson { get; set; } = string.Empty;

        // totals
        public decimal Total { get; set; }

        // statuses
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        // admin notes and timestamps
        public string? AdminNote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}