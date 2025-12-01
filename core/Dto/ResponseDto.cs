namespace core.Dto
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Result {  get; set; }
        
        public static ResponseDto<T> Ok(T data, string message)
        {
            return new ResponseDto<T>
            {
                Success = true,
                Message = message,
                Result = data
            };
        }
    
        public static ResponseDto<T> Failure(string message)
        {
            return new ResponseDto<T>
            {
                Success = false,
                Message = message,
                Result = default 
            };
        }
    }
}
