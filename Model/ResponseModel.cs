/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 17/09/2022
******************************************/

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
        public FailureResponseModel(string message)
        {
            Status = "failure";
            Message = message;
        }
    }

}
