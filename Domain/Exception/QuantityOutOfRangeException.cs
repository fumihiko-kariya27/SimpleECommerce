namespace SimpleECommerce.Domain.Exception
{
    public class QuantityOutOfRangeException : DomainException
    {
        public QuantityOutOfRangeException(string message) : base(message)
        {
        }
    }
}
