using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCollector.Objects
{
    public class ApiKeys
    {   
        public readonly int ID;

        public ExternalAPI externalApi;

        public string Key1;

        public string Key2;

        public string Key3;

        public string Key4;

        public int RemainingCredits;

        public ApiKeys(int ID, ExternalAPI externalAPI, int remainingCredits, string key1, string key2 = null, string key3 = null, string key4 = null)
        {
            this.ID = ID;
            externalApi = externalAPI;
            RemainingCredits = remainingCredits;
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
            Key4 = key4;
        }
    }

    public enum ExternalAPI
    {
        MeaningCloud
        ,IBM
    }
}
