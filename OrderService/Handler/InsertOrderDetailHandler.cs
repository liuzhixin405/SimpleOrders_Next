using DotNetCore.CAP;
using MediatR;
using OrderService.Command;
using System.Threading;
using Ecomm.Models;
using System.Collections.Generic;

namespace OrderService.Handler
{
    public class InsertOrderDetailHandler : IRequestHandler<InsertOrderDetailCommand, InsertOrderDetailModel>
    {
        private readonly OrderDbContext context;
        private readonly ICapPublisher cap;
        public InsertOrderDetailHandler(OrderDbContext context, ICapPublisher cap)
        {
            this.context = context;
            this.cap = cap;
        }
        public async System.Threading.Tasks.Task<InsertOrderDetailModel> Handle(InsertOrderDetailCommand request, CancellationToken cancellationToken)
        {
            using(var trans =context.Database.BeginTransaction(cap))
            {
                var order =  context.Orders.Add(new Order
                {
                    UpdatedTime = System.DateTime.Today,
                    UserId = request.UserId,
                    UserName = request.UserName
                });
                var orderDetail = context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Entity.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    ProductName = request.ProductName,
                });
                 context.SaveChanges();
                
                cap.Publish<OrderRequest>("order.created", new OrderRequest
                {
                    OrderId = order.Entity.Id,
                    ProductId = orderDetail.Entity.ProductId,
                    Quantity = orderDetail.Entity.Quantity
                }, new Dictionary<string,string>()) ;
                 trans.Commit();
                await System.Threading.Tasks.Task.CompletedTask;
                return new InsertOrderDetailModel { OrderDetailid = orderDetail.Entity.Id, OrderId = order.Entity.Id, Success = true };
            }
        }
    }
}
