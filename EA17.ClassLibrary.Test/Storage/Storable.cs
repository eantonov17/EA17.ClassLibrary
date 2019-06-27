//  # EA17.ClassLibrary
//  C# .Net Core general class library 
//
//  Copyright(C) 2018-2019 Eugene Antonov
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of version 3 of the GNU General Public License
//  as published by the Free Software Foundation.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.

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
