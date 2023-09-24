using AutoMapper;
using RabbitListener.Dto.Concrete;
using RabbitListener.Producer.Model;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace RabbitListener.Producer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SendMessageModel,RabbitDto>().ReverseMap();
        }
    }
}
