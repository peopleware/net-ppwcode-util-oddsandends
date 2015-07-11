﻿// Copyright 2010-2015 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    [Obsolete]
    public class GenuineDateTimeProvider : IDateTimeProvider
    {
        public GenuineDateTimeProvider()
        {
        }

        public static IDateTimeProvider CreateInstance()
        {
            return new GenuineDateTimeProvider();
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}