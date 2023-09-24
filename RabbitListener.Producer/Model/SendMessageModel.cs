using System.ComponentModel;

namespace RabbitListener.Producer.Model
{
    public class SendMessageModel
    {
        [Description("URL")]
        public string Url { get; set; }
    }
}
