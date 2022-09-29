using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string CourseId { get; private set; }
        public string CourseName { get; private set; }
        public string PhotoUrl { get; private set; }
        public Decimal Price { get; private set; }

        public OrderItem()
        {
        }
        public OrderItem(string courseId, string courseName, string photoUrl, decimal price)
        {
            CourseId = courseId;
            CourseName = courseName;
            PhotoUrl = photoUrl;
            Price = price;
        }

        public void UpdateOrderItem(string courseName, string photoUrl, decimal price)
        {
            CourseName = courseName;
            PhotoUrl = photoUrl;
            Price = price;
        }
    }
}
