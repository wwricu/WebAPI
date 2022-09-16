namespace WebAPI.Model
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string? Message { get; set; }
    }
    public class SuccessResponseModel : ResponseModel
    {
        public SuccessResponseModel()
        {
            Status = "success";
        }
        public object? obj { get; set; }
    }

    public class FailureResponseModel : ResponseModel
    {
        public FailureResponseModel()
        {
            Status = "failure";
        }
    }

}
