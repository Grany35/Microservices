using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Dtos
{
    public class OrderItemDto
    {
        public string CourseId { get;  set; }
        public string CourseName { get;  set; }
        public string PhotoUrl { get;  set; }
        public Decimal Price { get;  set; }
    }
}
