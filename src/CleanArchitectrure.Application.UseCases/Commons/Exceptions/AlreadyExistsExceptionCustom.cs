namespace CleanArchitectrure.Application.UseCases.Commons.Exceptions
{
    public class AlreadyExistsExceptionCustom : Exception
    {
        public AlreadyExistsExceptionCustom(string message)
            : base(message)
        {
        }
    }
}
