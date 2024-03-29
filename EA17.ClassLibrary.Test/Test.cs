﻿//  # EA17.ClassLibrary
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EA17.ClassLibrary.Test
{
    public abstract class Test
    {
        protected readonly int count;
        protected readonly int seed;

        protected string Name { get; }

        public Test(string name, int count = 100, int seed = 17)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.count = count;
            this.seed = seed;
        }

        protected Func<object, object> testEqualFunc;

        protected virtual IEnumerable<(Func<object[], bool> Test, IEnumerable<object[]> Params, string TestName)> IsTrue()
        {
            if (testEqualFunc != null)
                yield return (TestEqual, TestEqualParams(), nameof(TestEqual));
        }

        protected virtual bool TestEqual(object[] values)
        {
            var s = values[0];
            var d = values[1];
            var r = testEqualFunc(s);
            return Equ.Al(d, r);
        }

        protected virtual IEnumerable<object[]> TestEqualParams() { yield break; }

        protected virtual bool TestNotEqual(object[] values)
        {
            var s = values[0];
            var d = values[1];
            var r = testEqualFunc(s);
            return !Equ.Al(d, r);
        }

        protected virtual IEnumerable<object[]> TestNotEqualParams() { yield break; }

        protected static string Os2S(object[] p) => "{" + string.Join(",", p.Select((o) => (o ?? String.Empty).ToString())) + "}";

        public void Run()
        {
            foreach (var (Test, Params, TestName) in IsTrue())
                foreach (var p in Params)
                    Assert.IsTrue(Test(p), "EnumerableObjectTestClass failed {0}.{1}({2})", Name, TestName, Os2S(p));
        }

        protected static object[] Params(params object[] values) => values;
        //protected static IEnumerable<T> Enumerable<T>(params T[] values) => values;
        //protected static object[] Trim1st(object[] os)
        //{
        //    var r = new object[os.Length - 1];
        //    Array.Copy(os, 1, r, 0, os.Length - 1);
        //    return r;
        //}

    }
}