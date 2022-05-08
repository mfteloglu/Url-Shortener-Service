using Microsoft.AspNetCore.WebUtilities;
using Shortener.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Service.Services
{
    public class UrlHelper : IUrlHelper
    {
        // Id to short string -6 digit long-
        public string GetShortUrl(int id)
        {
            var temp = BitConverter.GetBytes(id);
            return WebEncoders.Base64UrlEncode(temp);
        }

        // Short string to id back
        public int GetId(string shortUrl)
        {
            var temp = WebEncoders.Base64UrlDecode(shortUrl);
            return BitConverter.ToInt32(temp);
        }
    }
}
