using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TNPW2_video_2_ASP_MVC.Class
{
    public static class Hash
    {
        public static string HashPassword(this string str)
        {
            SHA1 shA1 = SHA1.Create();
            byte[] bytes = new ASCIIEncoding().GetBytes(str);
            str = Convert.ToBase64String(shA1.ComputeHash(bytes));
            return str;
        }

    }
}