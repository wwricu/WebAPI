/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Weiran Wang
  Date   : 16/09/2022
******************************************/

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
using JWT.Builder;
using WebAPI.Entity;
using System.Diagnostics;
using WebAPI.Model;

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

        public static string CreateJWT(PublicInfoModel info)
        {
            var payload = new Dictionary<string, object>
            {
                { "iss","wwr"},
                { "exp", UnixTimeStampUTC(DateTime.Now.AddHours(48))},
                { "sub", "TOKEN LOGIN" },
                { "aud", info },
                { "iat", DateTime.Now.ToString() }
            };
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }
        public static PublicInfoModel ValidateJWT(string token)
        {
            PublicInfoModel publicInfo = new PublicInfoModel();
            try
            {
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
                string payload = decoder.Decode(token, secret, verify: true);
                Debug.WriteLine(payload);
                publicInfo = serializer.Deserialize<JWTModel>(payload).aud;
            }
            catch (TokenExpiredException ex)
            {
                throw ex;
            }
            catch (SignatureVerificationException ex)
            {
                throw ex;
            }
            return publicInfo;
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
