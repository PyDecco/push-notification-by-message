namespace PushNotificationByMessage.Ports.In
{
    public interface IBCrypt
    {
        string HashPassword(string password, string salt);
        bool Verify(string password, string hash);
    }
}
