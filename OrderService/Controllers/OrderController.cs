using System.Collections.Generic;
using System.Threading.Tasks;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using MediatR;
using OrderService.Command;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailsProvider orderDetailsProvider;
        private readonly Plain.RabbitMQ.IPublisher publisher;
        private readonly IOrderCreator orderCreator;
        private readonly IMediator mediator;
        public OrderController(IOrderDetailsProvider orderDetailsProvider, Plain.RabbitMQ.IPublisher publisher, IOrderCreator orderCreator,IMediator mediator)
        {
            this.orderDetailsProvider = orderDetailsProvider;
            this.publisher = publisher;
            this.orderCreator = orderCreator;
            this.mediator = mediator;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<OrderDetail> Get()
        {
            return orderDetailsProvider.Get();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task Post([FromBody] OrderDetail orderDetail)
        {
            var id = await orderCreator.Create(orderDetail);
            publisher.Publish(JsonConvert.SerializeObject(new OrderRequest { 
                OrderId = id,
                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity


            }), "order.created", null);
        }

        [HttpPost("SendOrder")]
        public async Task<ActionResult<InsertOrderDetailModel>> SendOrder([FromBody] InsertOrderDetailCommand model)
        {
            var res = await mediator.Send(model);
            return Ok(res);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
