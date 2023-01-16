using Microsoft.AspNetCore.Mvc;
using Test_iRenta.Data.Interfaces;
using Test_iRenta.Data.Models.EntityModel;

namespace Test_iRenta.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProducts productsService;
        public ProductController(IProducts productsService)
        {
            this.productsService = productsService;
        }

        /// <summary>
        /// ��������� ���� ������� ��
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     Get /Goods
        ///     [
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "Price": 5
        ///     },
        ///     {
        ///        "id": 1,
        ///        "name": "Item #2",
        ///        "Price": 15
        ///     },
        ///     ]
        ///
        /// </remarks>
        /// <response code="200">�������! �� ��������� ������ � ��������/</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Goods()
        {
            /*
             * ��� ������������ ����� ������������� Swagger. 
             * � ���� ���� �������� ��� ������, �� �� ����� ��� ���� 5 ��� ������ 
             * ����� ������� ������ ���� ����������.
            */
            return Ok(await productsService.Goods());
        }

        [HttpGet]
        public async Task<IActionResult> Good(sbyte id)
        {
            var product = await productsService.GetProduct(id);

            if (product == null)
                return BadRequest("�� ������� ID ����� �� ������!");
            return Ok(await productsService.Goods());
        }
    }
}