using EA17.ClassLibrary.Fundamentals;
using EA17.ClassLibrary.Types;
using System.Collections.Generic;

namespace EA17.ClassLibrary.Storage
{
    public abstract class StorableObject
    {
        protected abstract void Init();

        protected ObjectProperties<StorableObject, Storable> Properties { get; }
        protected Dictionary<Tag, string> Tags { get; } = new Dictionary<Tag, string>();
        public FieldSelection FieldSelection { get; } = new FieldSelection();
        public FieldOrder FieldOrder { get; } = new FieldOrder();

        protected StorableObject() { Properties = ObjectProperties<StorableObject, Storable>.Get(GetType()); Init(); }

        public Storable this[string name] => Properties[name].Get(this);
        public Storable this[Tag tag] => Properties[Tags[tag]].Get(this);
        public string Untag(Tag tag) => Tags[tag];

        public static S NewInited<S>(params string[] propertyNames) where S : StorableObject, new() => InitNulls(new S(), propertyNames);

        public static S InitNulls<S>(S s, params string[] propertyNames) where S : StorableObject { s.InitNulls(propertyNames); return s; }

        public void InitNulls(params string[] propertyNames) => Properties.InitNulls(this, propertyNames);

        public IEnumerable<(ObjectProperty<StorableObject, Storable> property, object value)> EnumStorables(bool allProps)
        {
            foreach (var property in Properties)
                if (property[this] is Storable s)
                    if (allProps || s.IsDirty)
                        yield return (property, s.Value);
        }

        public override string ToString() => GetType().Name + ", " + Properties.ToString();

        // TODO move to the appropriate storage adapter
        public const string Xeq = "{0} eq {1}";
        public const string Xne = "{0} ne {1}";
        public const string Xlt = "{0} lt {1}";
        public const string Xle = "{0} le {1}";
        public const string Xgt = "{0} gt {1}";
        public const string Xge = "{0} ge {1}";
        //public static readonly string Xgen = $"({{0}} ge {{1}} or {{0}} le {(int)Ads.AdPrice.NotSpecified})";
        public const string Xgeo_distance = "geo.distance({0},geography'POINT({1})')";
        public const string Xnot_geo_distance = "not geo.distance({0},geography'POINT({1})')";
        public const string Xsearch_in = "search.in({0},'{1}')";
        public const string Xnot_search_in = "not search.in({0},'{1}')";

        public virtual IEnumerable<(ObjectProperty<StorableObject, Storable> property, string xop, object v)> EnumConditions()
        {
            foreach (var property in Properties)
                if (property[this] is Storable s && s.Value is object value)
                    yield return (property, Xeq, value);
        }

        //public static Storable<DateTimeOffset> LastDateTime => DateTimeOffset.UtcNow;
        //public static StoredOnce<DateTimeOffset> FirstDateTime => DateTimeOffset.UtcNow;
    }
}
