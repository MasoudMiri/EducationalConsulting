namespace EducationalConsulting.DTOs
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ServiceResult Ok(string message = null) =>
            new ServiceResult { Success = true, Message = message };

        public static ServiceResult Fail(string message) =>
            new ServiceResult { Success = false, Message = message };

        public static ServiceResult<T> Ok<T>(T data, string message = null) =>
            new ServiceResult<T> { Success = true, Message = message, Data = data };

        public static ServiceResult<T> Fail<T>(string message) =>
            new ServiceResult<T> { Success = false, Message = message };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
}