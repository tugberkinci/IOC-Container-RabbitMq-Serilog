using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitListener.Data.Services.Abstract;
using RabbitListener.Data.Services.Concrete;
using RabbitListener.Dto.ConfigurationModels;
using RabbitListener.Producer.Controllers;
using RabbitListener.Producer.Mapper;
using RabbitListener.Producer.Model;
using RabbitListener.Producer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Coverlet.Collector
{
    public  class RabbitProducerTest
    {
   
        readonly IRabbitProducerService _rabbitProducerService;
        readonly ProducerController _producerController;

        public RabbitProducerTest()
        {
            IOptions<RabbitConfiguration> options = Options.Create<RabbitConfiguration>(new RabbitConfiguration { Host = "localhost", QueueName = "urls" });
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            _rabbitProducerService = new RabbitProducerService(options);
            _producerController = new ProducerController(_rabbitProducerService,mapper);
        }

        [Fact]
        public async void SentMessageValid()
        {
            var model = new SendMessageModel { Url = "https://www.google.com/webhp?authuser=1" };
            var response = await _producerController.SentMessage(model);

            var resultType = response as OkObjectResult;
            var result = resultType.Value as GenericResponse;


            Assert.IsType<GenericResponse>(result);

           
            Assert.IsType<Details>(result.Details);

        }

        [Fact]
        public async void SentMessageInvalid()
        {
            var model = new SendMessageModel { Url = "invalidUrl" };
            var response = await _producerController.SentMessage(model);
            var resultType = response as BadRequestObjectResult;
            var result = resultType.Value as GenericResponse;


            Assert.IsType<GenericResponse>(result);
            Assert.IsType<Details>(result.Details);

        }

        

        [Fact]
        public async  void SentMessageEmptyModel()
        {
            var model = new SendMessageModel {};
            var response = await _producerController.SentMessage(model);

            var resultType = response as BadRequestObjectResult;
            var result = resultType.Value as GenericResponse;

            Assert.IsType<GenericResponse>(result);

            
            Assert.IsType<Details>(result.Details);

        }



    }
}
