using System.Collections.Generic;
using System.Linq;

namespace EA17.ClassLibrary.Storage
{
    public class FieldOrder
    {
        private Dictionary<string, bool> dict = new Dictionary<string, bool>();
        public FieldOrder Add(string name, bool descending) { dict[name] = descending; return this; }
        public FieldOrder Asc(string name) => Add(name, false);
        public FieldOrder Desc(string name) => Add(name, true);
        public FieldOrder Clear() { dict.Clear(); return this; }

        public List<string> List => new List<string>(Enumerate());
        public IEnumerable<string> Enumerate() => dict.Select((f) => f.Key + (f.Value ? " desc" : " asc"));
        public override string ToString() => string.Join(", ", Enumerate());
    }
}
