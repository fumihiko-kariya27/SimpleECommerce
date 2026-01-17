namespace SimpleECommerce.InfraStructure.Logging
{
    public interface IAppLogger<T>
    {
        void Debug(string message, object? data = null);

        void Info(string message, object? data = null);

        void Warn(string message, object? data = null);

        void Error(Exception ex, string message, object? data = null);
    }
}
