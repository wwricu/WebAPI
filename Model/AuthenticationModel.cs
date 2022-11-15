/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 17/09/2022
******************************************/

using WebAPI.Entity;

namespace WebAPI.Model
{
    public class PasswordLoginModel
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class JWTModel
    {
        public string? iss { get; set; }
        public string? exp { get; set; }
        public string? sub { get; set; }
        public PublicInfoModel? aud { get; set; }
        public string? iat { get; set; }
    }

}
