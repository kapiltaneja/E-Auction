using AspNetCoreMultipleProject.Commands;
using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly IMediator _mediator;

        public BuyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/getAllBuyer")]
        public async Task<IActionResult> GetAllBuyer()
        {
            //return Ok(await _businessProvider.GetAllBuyer());
            try
            {
                return Ok(await _mediator.Send(new GetAllBuyersQuery()));
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("/place-bid")]
        public async Task<IActionResult> PlaceBid([FromBody] BuyerInfoVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                // var result = await _businessProvider.AddBuyer(value);
                var result = await _mediator.Send(new AddBuyerCommand(value));
                return Created("/api/DataEventRecord", result);
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpPut("/update-bid/{productId}/{buyerEmailId}/{newBidAmt}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            try
            {
                if (productId == 0)
                {
                    return BadRequest();
                }
                //  await _businessProvider.UpdateBid(productId, buyerEmailId, newBidAmt);
                await _mediator.Send(new UpdateBidCommand(productId, buyerEmailId, newBidAmt));
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
           
        }

       
    }
}
