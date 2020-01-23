using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebShop.Models;

namespace WebShop.Extensions
{
    public static class SessionExtension
    {
        public static void SerializeCart(this ISession session, string key, Cart c)
        { 
            session.SetString(key, JsonConvert.SerializeObject(c));
        }


        public static Cart DeserializeCart(this ISession session, string key)
        {
            string jsonString = session.GetString(key);

            if (jsonString != null) 
            { 
                return JsonConvert.DeserializeObject<Cart>(jsonString);
            } 
            else
            {
                return null; 
            }

        }
    }
}
