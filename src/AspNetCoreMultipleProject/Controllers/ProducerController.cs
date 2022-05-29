using AspNetCoreMultipleProject.Commands;
using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("e-auction/api/v1/producer/[controller]")]
    [ApiController]
public class ProducerController : ControllerBase
{
        private readonly ProducerConfig _producerConfig;
        private readonly IMediator _mediator;

        public ProducerController(ProducerConfig producerConfig,IMediator mediator)
        {

            _producerConfig = producerConfig;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("/place-bid-messaging")]
        public async Task<IActionResult> PlaceBid(string topic,[FromBody] BuyerInfoVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                // var result = await _businessProvider.AddBuyer(value);
                string sereliazebid = JsonConvert.SerializeObject(value);
                using (var producer = new ProducerBuilder<Null,string>(_producerConfig).Build())
                {
                    await producer.ProduceAsync(topic, new Message<Null, string> { Value = sereliazebid });
                    producer.Flush(TimeSpan.FromSeconds(10));
                    return Ok(true);
                }
               // var result = await _mediator.Send(new AddBuyerCommand(value));
                //return Created("/api/DataEventRecord", result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpGet]
        [Route("/show-bids-messaging/{productId}")]
        public async Task<IActionResult> ShowAllBids(string topic,int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
           
          var data =  await _mediator.Send(new GetAllBidsQuery(productId));
            string sereliazeallbids = JsonConvert.SerializeObject(data);
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = sereliazeallbids });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}
