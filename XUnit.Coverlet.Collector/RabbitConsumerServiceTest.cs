using Microsoft.Extensions.Options;
using RabbitListener;
using RabbitListener.Data.Services.Concrete;
using RabbitListener.Dto.ConfigurationModels;
using RabbitListener.Producer.Controllers;
using RabbitListener.Producer.Model;
using RabbitListener.Producer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XUnit.Coverlet.Collector
{
    public class RabbitConsumerServiceTest
    {
    
        private RabbitConsumerService _consumer;
        public RabbitConsumerServiceTest()
        {
            IOptions<RabbitConfiguration> options = Options.Create<RabbitConfiguration>(new RabbitConfiguration { Host = "localhost",QueueName="urls"});
            _consumer = new RabbitConsumerService(options);
        }


        [Fact]
        public void ConsumerNullTest()
        {
            try
            {
                _consumer.Consume(null);
                return;
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Fact]
        
        public void ConsumerEmptyTest()
        {
            try
            {
                _consumer.Consume("");
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        //Seted the comment line because both cases are valid, service listens the queue when testing and test never ends
        //[Fact]
        //public void ConsumerValidTest()
        //{
        //    try
        //    {
        //        _consumer.Consume("TestService");
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //}

        //[Fact]
        //public void ConsumerNonGenericCharactersTest()
        //{
        //    try
        //    {
        //        _consumer.Consume("$()a?)=-");
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message);
        //    }
        //}

    }
}
