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

using System;

namespace EA17.ClassLibrary.Types
{
    public static class Extensions
    {
        public static string TypeFN(this object v) => v?.GetType()?.FullName;

        public static object MustBe(this object v, Type t) => t.IsInstanceOfType(v) ? v : throw new InvalidOperationException($"Object '{v}' must have type {t.TypeFN()}");

    }
}
