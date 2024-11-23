namespace CleanArchitectrure.Application.UseCases.Commons.Exceptions
{
    public class NotFoundExceptionCustom : Exception
    {
        public NotFoundExceptionCustom()
            : base("The requested resource was not found.")
        {
        }

        public NotFoundExceptionCustom(string message)
            : base(message)
        {
        }
    }
}
