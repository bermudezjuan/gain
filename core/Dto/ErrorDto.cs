namespace core.Dto
{
    public class ErrorDto(string propertyName, string errorMessage)
    {
        public string PropertyName { get; set; } = propertyName;
        public string ErrorMessage { get; set; } = errorMessage;
    }
}
