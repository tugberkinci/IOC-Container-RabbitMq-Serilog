using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitListener.Data.Services.Abstract;
using RabbitListener.Dto.Concrete;
using RabbitListener.Producer.Model;
using RabbitListener.Producer.Response;

namespace RabbitListener.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IRabbitProducerService _rabbitProducer;
        private readonly IMapper _mapper;
        public ProducerController(
            IRabbitProducerService rabbitProducer,
            IMapper mapper)
        {
            _mapper = mapper;
            _rabbitProducer = rabbitProducer;
        }

        /// <summary>
        /// RabbitMQ Producer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SentMessage([FromBody]SendMessageModel model)
        {
            var dto = _mapper.Map<RabbitDto>(model);
            var result = await Task.Run(()=> _rabbitProducer.ProduceAsync(dto));
            if (!result.Succeeded)
            {
                var errors = String.Join(",", result.Errors.Select(x => x.Message));
                return BadRequest(GenericResponse.Failed("400", errors));
            }
               
            return Ok(GenericResponse.Success());
        }


    }
}
