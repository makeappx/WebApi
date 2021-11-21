using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WebApi
{
    class TestDict
    {
        ConcurrentDictionary<string, string> Dict = new();

        public IEnumerable<string> GetAllKeys() => Dict.Where(x => x.Value != null).Select(x => x.Key);
        public string Add(string key, string value) => Dict.TryAdd(key, value) ? value : null;
        public string DeleteValue(string key) => Dict.TryRemove(key, out string value) ? value : null;
        public string GetValue(string key) => Dict.TryGetValue(key, out string value) ? value : null;
    }
}