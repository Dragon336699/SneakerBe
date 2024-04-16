namespace Sneaker_Be.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
