namespace gain_api.Helpers
{
    using System.Text.RegularExpressions;
    using core.Dto;
    using FluentValidation.Results;

    public class ValidatorsHelper
    {
        public static bool ValidateString(string input)
        {
            var regex = new Regex("^[a-zA-Z\\s]{3,}$");
            return regex.IsMatch(input);
        }
    
        public static bool ValidateLength(string? str)
        {
            if(string.IsNullOrEmpty(str)){ return true; }
            return !string.IsNullOrEmpty(str) && str.Length == 11 && str.All(char.IsDigit);
        }
    
        public static bool ValidateDigitsOnly(string str)
        {
            return str.All(char.IsDigit);
        }
    
        public static bool StartWithChar(string str)
        {
            var regex = new Regex("^[a-zA-Z]");
            return regex.IsMatch(str);
        }
    
        public static bool ValidatePhoneNumber(string? phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber)){ return true; }
            var regex = new Regex(@"^\d{4}-\d{3}-\d{4}$");
            return regex.IsMatch(phoneNumber);
        }
    
        public static bool ValidarRIF(string rif)
        {
            var regex = new Regex(@"^[jJgG]\d{8}[\d]$");
            return regex.IsMatch(rif);
        }
    
        public static bool BeValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        }

        public static List<ErrorDto> ValidationErros(ValidationResult validationResult)
        {
            try
            {
                var simplifiedErrors = validationResult.Errors
                    .Select(e => new ErrorDto(e.PropertyName, e.ErrorMessage))
                    .ToList();
                return simplifiedErrors;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return [];
            }
        }
    }
}
