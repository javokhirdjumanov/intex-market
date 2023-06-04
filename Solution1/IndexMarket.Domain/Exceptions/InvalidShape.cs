namespace IndexMarket.Domain.Exceptions
{
    public class InvalidShape : Exception
    {
        public InvalidShape(string message) 
            : base(message)
        { }
    }
}
