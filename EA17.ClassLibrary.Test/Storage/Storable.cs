namespace EA17.ClassLibrary.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    public partial class Testing
    {
        [TestMethod]
        public void StorableIsDirty() => new Storage.StorableIsDirtyTest().Run();
    }
}

namespace EA17.ClassLibrary.Test.Storage
{
    using EA17.ClassLibrary.Storage;
    using System;
    using System.Collections.Generic;

    internal class StorableIsDirtyTest : Test
    {
        public StorableIsDirtyTest() : base(nameof(Storable)) { testEqualFunc = (t) => ((Storable)t).IsDirty; }

        protected override IEnumerable<object[]> TestEqualParams()
        {
            //{
            //    var s = StorableObject.FirstDateTime;
            //    yield return Params(s, true);
            //    s.Stored = s.Value;
            //    yield return Params(s, false);
            //}
            {
                var s = new Storable<Guid>();
                yield return Params(s, false);
                s.Stored = Guid.NewGuid();
                yield return Params(s, false);
                s.Value = Guid.NewGuid();
                yield return Params(s, true);
            }
            {
                var s = new StoredOnce<Guid>();
                yield return Params(s, false);
                s.Stored = Guid.NewGuid();
                yield return Params(s, false);
                s.Value = Guid.NewGuid();
                yield return Params(s, false);
            }

            //yield return Params(new StorableInt(), false);
            //yield return Params(new StorableInt(1), true);

            //yield return Params(new StorableInt(), false);
            //yield return Params(new StorableInt(1), true);
            //yield return Params(new StorableInt(17) { Stored = 17 }, false);
        }
    }
}
