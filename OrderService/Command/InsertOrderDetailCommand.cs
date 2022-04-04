using MediatR;

namespace OrderService.Command
{
    public class InsertOrderDetailCommand : IRequest<InsertOrderDetailModel>
    {
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        //public InsertOrderDetailCommand(int quantity, string productName, int productId, int userId, string userName)
        //{
        //    Quantity = quantity;
        //    ProductName = productName;
        //    ProductId = productId;
        //    UserId = userId;
        //    UserName = userName;
        //}
    }
    public class InsertOrderDetailModel
    {
        public int OrderId { get; set; }
        public int OrderDetailid { get; set; }
        public bool Success { get; set; }

        //public int Quantity { get; set; }
        //public string ProductName { get; set; }
        //public int ProductId { get; set; }
        //public int UserId { get; set; }
        //public string UserName { get; set; }

        //public InsertOrderDetailModel(int quantity, string productName, int productId, int userId, string userName)
        //{
        //    Quantity = quantity;
        //    ProductName = productName;
        //    ProductId = productId;
        //    UserId = userId;
        //    UserName = userName;
        //}
    }

}
