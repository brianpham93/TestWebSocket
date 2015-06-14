using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Models.Neo
{
    public class Key
    {
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public Key()
            : base()
        {

        }
        public Key(string keyName, string keyValue)
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }
    }
}