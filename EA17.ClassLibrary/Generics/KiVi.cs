using System;
using System.Collections.Generic;

namespace EA17.ClassLibrary.Generics
{
    public class KiVi<K, V> : Dictionary<K, V>
    {
        protected readonly Func<K, V> k2v;

        public KiVi() { }
        public KiVi(Func<K, V> fill) => k2v = fill ?? throw new ArgumentNullException(nameof(fill));

        public V Get(K k)
        {
            if (!TryGetValue(k, out V v))
                this[k] = v = k2v(k);
            return v;
        }

        public V Get(K k, Func<V> create)
        {
            if (!TryGetValue(k, out V v))
                this[k] = v = create();
            return v;
        }
    }
}
