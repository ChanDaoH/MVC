using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class JsonHandler
    {
        public static JsonMessage CreateMessage(int ptag, string pmessage, string pvalue)
        {
            JsonMessage json = new JsonMessage
            {
                tag = ptag,
                message = pmessage,
                value = pvalue
            };
            return json;
        }
        public static JsonMessage CreateMessage(int ptag, string pmessage)
        {
            JsonMessage json = new JsonMessage
            {
                tag = ptag,
                message = pmessage
            };
            return json;
        }
    }

    public class JsonMessage
    {
        public int tag { get; set; }
        public string message { get; set; }
        public string value { get; set; }
    }
}
