using Microsoft.AspNetCore.Mvc;
using Test_iRenta.Data.Interfaces;
using Test_iRenta.Data.Models.EntityModel;

namespace Test_iRenta.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrders orderService;

        public OrderController(IOrders ordersService)
        {
            this.orderService = ordersService;
        }

        /// <summary>
        /// ��������� ���� �������
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await orderService.GetOrders());
        }

        /// <summary>
        /// ��������� ���� ������� �� ����
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrdersForDate(DateTime forDate)
        {
            return Ok(await orderService.GetOrders(forDate));
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            return Ok(await orderService.GetOrder(id));

        }
        /// <summary>
        /// ������� �����
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Order model)
        {
            var message = await orderService.CreateOrder(model);
            if (message.Trim().StartsWith("������"))
                return BadRequest(message);
            return Ok(message);

        }

        /// <summary>
        /// �������� ����� ����� Put
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(Order model)
        {
            var message = await orderService.EdtiOrder(model);
            if (message.Trim().StartsWith("������"))
                return BadRequest(message);
            return Ok(message);

        }

        /// <summary>
        /// �������� ����� ����� Patch
        /// </summary>
        [HttpPatch]
        public async Task<IActionResult> Patch(Order model)
        {
            var message = await orderService.EdtiOrder(model);
            if (message.Trim().StartsWith("������"))
                return BadRequest(message);
            return Ok(message);
        }

        /// <summary>
        /// ������� �����
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await orderService.DeleteOrder(id);
            if (status)
                return Ok("����� ������");
            return BadRequest("������ ����� ������ �������");
        }
    }
}