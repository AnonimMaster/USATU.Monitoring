namespace USATU.Monitoring.Web.RabbitMQ
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
    }
}