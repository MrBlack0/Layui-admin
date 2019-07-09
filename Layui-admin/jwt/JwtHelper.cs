using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Layui_admin.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Layui_admin.jwt
{
    public class JwtHelper
    {
        private static string secret = ConfigurationManager.AppSettings["Secret"].ToString();
        public static string SetJwtEncode(string userName, string pwd)
        {

            //格式如下
            var payload = new Dictionary<string, object>
            {
                { "UserName",userName },
                { "PassWord", pwd }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return token;
        }

        /// <summary>
        /// 根据jwtToken  获取实体
        /// </summary>
        /// <param name="token">jwtToken</param>
        /// <returns></returns>
        public static Admin_User GetJwtDecode(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var userInfo = decoder.DecodeToObject<Admin_User>(token, secret, verify: true);//token为之前生成的字符串
            return userInfo;
        }
    }
}