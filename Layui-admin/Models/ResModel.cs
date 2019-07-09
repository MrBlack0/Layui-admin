using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layui_admin.Models
{
    public class ResModel
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
    public class ResModel<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public T[] data { get; set; }
    }
    public class ResModelFactory
    {
        public static ResModel ResDefault()
        {
            return new ResModel()
            {
                code = "000",
                msg = "success"
            };
        }
        public static ResModel ResError(string msg = null)
        {
            return new ResModel()
            {
                code = "999",
                msg = string.IsNullOrEmpty(msg) ? "error" : msg
            };
        }
        public static ResModel<T> ResDefaultData<T>() where T : class
        {
            return new ResModel<T>()
            {
                code = 0,
                msg = "success",
                count = 0,
                data = null
            };
        }
    }
}