using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using System.Security.Cryptography;
using System.Text;
using JWT.Exceptions;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Encodings.Web;
using System;

namespace WebAPI.Service
{
    public class SecurityService
    {   // http://www.manongjc.com/detail/19-sxnyzssuhaynasa.html
        /* JWT area begins */
        static IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        static IJsonSerializer serializer = new JsonNetSerializer();
        static IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        static IDateTimeProvider provider = new UtcDateTimeProvider();
        const string secret = "8888888888888888888888888888888888888888";

        public static string CreateJWT(Dictionary<string, object> payload)
        {
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }
        public static bool ValidateJWT(string token, out string payload, out string message)
        {
            bool isValidated = false;
            payload = "";
            try
            {
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                payload = decoder.Decode(token, secret, verify: true);
 
                isValidated = true;
 
                message = "token pass";
            }
            catch (TokenExpiredException ex)
            {
                message = "token expired";
            }
            catch (SignatureVerificationException ex)
            {
                message = "token failed";
            }
            return isValidated;
        }
        public static long UnixTimeStampUTC(DateTime dateTime)
        {
            Int32 unixTimeStamp;
            DateTime zuluTime = dateTime.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
            return unixTimeStamp;
        }
        /* JWT area ends */
        public static string GetMD5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 MD5Hash = MD5.Create();
            byte[] data = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string GeneratePassword()
        {
            return Convert.ToString(
                           new Random().Next(Convert.ToInt32(
                                                     DateTime.Now.Millisecond)));
        }

        public static string GenerateSalt()
        {
            return GetMD5Hash(Convert.ToString(
                                  new Random().Next(Convert.ToInt32(
                                               DateTime.Now.Millisecond))));
        }
    }
}
