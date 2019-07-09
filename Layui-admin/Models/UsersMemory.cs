using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Layui_admin.Models
{
    public class UsersMemory<T> where T : class
    {
        private static UsersMemory<T> _dicOnlineUsers = null;
        private UsersMemory() { }
        public static UsersMemory<T> GetUsers()
        {
            if (_dicOnlineUsers == null)
            {
                _dicOnlineUsers = new Models.UsersMemory<T>();
            }
            return _dicOnlineUsers;
        }
        public Dictionary<string, T> CurentUserList { get; set; }
        public string Add(T uses)
        {
            try
            {
                if (CurentUserList == null)
                {
                    CurentUserList = new Dictionary<string, T>();
                }
                string key = Guid.NewGuid().ToString();
                CurentUserList.Add(key, uses);
                return key;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Remove(string key)
        {
            try
            {
                CurentUserList.Remove(key);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}