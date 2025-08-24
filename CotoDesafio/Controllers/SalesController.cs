using CotoDesafio.Application.Commands;
using CotoDesafio.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CotoDesafio.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            
        }

        [HttpGet("GetTotalSalesByCenterId/{centerId}")]
        public async Task<IActionResult> GetByCenter(Guid centerId)
        {
            
        }

        [HttpGet("GetTotalSales")]
        public async Task<IActionResult> GetTotal()
        {
            
        }

        [HttpGet("GetPercentages")]
        public async Task<IActionResult> GetPercentages()
        {
            
        }
    }
}
