using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSEPWebAPI.Helpers.Neo
{
    public class KeyString
    {
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public KeyString()
            : base()
        {

        }
        public KeyString(string keyName, string keyValue)
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }
    }

    public class KeyInt
    {
        public string KeyName { get; set; }
        public int KeyValue { get; set; }
        public KeyInt()
            : base()
        {

        }
        public KeyInt(string keyName, int keyValue)
        {
            this.KeyName = keyName;
            this.KeyValue = keyValue;
        }
    }
}