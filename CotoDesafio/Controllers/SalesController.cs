using CotoDesafio.Application.Commands;
using CotoDesafio.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> PostRegister([FromBody] RegisterSaleCommand cmd)
        {
            if (cmd == null)
                return BadRequest("Request body is required.");

            try
            {
                var sale = await _mediator.Send(cmd);
                if (sale == null)
                    return UnprocessableEntity("Sale could not be registered.");

                return Created("Sale", sale.CarChassisNumber);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetTotalSalesByCenterId/{centerId}")]
        public async Task<IActionResult> GetTotalSalesByCenterId(Guid centerId)
        {
            if (centerId == Guid.Empty)
                return BadRequest("CenterId is required.");

            try
            {
                var saleTotalByCenter = await _mediator.Send(new GetTotalSalesByCenterQuery(centerId));
                if (saleTotalByCenter == null)
                    return NotFound("No sales found for the specified center.");

                return Ok(saleTotalByCenter);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetTotalSales")]
        public async Task<IActionResult> GetTotalSales()
        {
            try
            {
                var saleTotal = await _mediator.Send(new GetTotalSalesQuery());
                if(saleTotal.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(saleTotal);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("GetPercentages")]
        public async Task<IActionResult> GetPercentages()
        {
            try
            {
                var percentages = await _mediator.Send(new GetPercentagesQuery());
                return Ok(percentages);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}