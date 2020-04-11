using System;
using System.Collections.Generic;
using System.Linq;

namespace IdescatApi.Serveis
{
    public class Parameters
    {
        public Dictionary<string, string> Params = new Dictionary<string, string>();

        public void Add(string key, string value)
        {
            Params.Add(key, value);
        }

        public void Add<T> (string key, T value) where T: Enum
        {
            Params.Add(key, value.GetDescription());
        }

        public void Add<T>(string key, List<T> values) where T : Enum
        {
            if (values.Count > 0)
            {
                Params.Add(key, string.Join(",", values.Select(e => e.GetDescription()).ToList()));
            }
        }

        public override string ToString()
        {
            return string.Join("&", Params.ToList().Select(p => $"{p.Key}={p.Value}").ToList());
        }
    }
}
