using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Shared.Messages
{
    public class CreateOrderMessageCommand
    {
        public string BuyerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }

    }
    public class OrderItem
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string PhotoUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
