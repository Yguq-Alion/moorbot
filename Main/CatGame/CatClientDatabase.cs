using System;
using System.Collections.Generic;
using System.Text;

namespace moorbot
{
    class CatClientDatabase 
    {
        public Dictionary<long, CatClientData> database { get;set; }
        public CatClientDatabase()
        {

            database = new Dictionary<long, CatClientData>();
        }
    }
}
