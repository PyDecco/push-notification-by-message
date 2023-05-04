namespace PushNotificationByMessage.Ports.In
{
    public interface IJWTCore
    {
        Task<string> GeradorDeJwt(string login);
    }
}
