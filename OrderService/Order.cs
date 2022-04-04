using System;

namespace OrderService
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
