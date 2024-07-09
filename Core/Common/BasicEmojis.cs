using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class BasicEmojis
    {
        public static string ParseEmojis(string content)
        {
            content = content.Replace(":)", Img("emoji1"));
            return content;
        }
        private static string Img(string imageName)
        {
            return ("<img class=\"emoji\" src=\"/images/emojis/" + imageName + "\">");
        }
    }
}
