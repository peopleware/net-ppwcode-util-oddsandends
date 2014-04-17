//Copyright 2004 - $Date: 2008-11-15 23:58:07 +0100 (za, 15 nov 2008) $ by PeopleWare n.v..

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

#region Using

using System;

#endregion

namespace PPWCode.Util.OddsAndEnds.II.DateTimeProvider
{
    /// <summary>
    /// DateTimeProvider is an abstract class that provides a DateTime for Now and Today.
    /// Using this class in code makes it easier to mock code that depends on Now and Today.
    /// 
    /// The DateTimeProvider class contains a static method <see cref="DateTimeProvider.Current"/>
    /// to retrieve the configured DateTimeProvider instance. By default, there is a DateTimeProvider
    /// instance configured that returns DateTime.Now and DateTime.Today. Another DateTimeProvider
    /// instance can be set, for example in unit tests.
    /// </summary>
    public abstract class DateTimeProvider
    {
        private static DateTimeProvider s_Current = new DefaultDateTimeProvider();

        /// <summary>
        /// Gets and sets the currently active DateTimeProvider instance.
        /// </summary>
        public static DateTimeProvider Current
        {
            get { return s_Current; }
            set { s_Current = value; }
        }

        /// <summary>
        /// Reset the currently active DateTimeProvider to the default.
        /// </summary>
        public static void Reset()
        {
            Current = new DefaultDateTimeProvider();
        }

        /// <summary>
        /// Returns a DateTime instance representing Today.
        /// </summary>
        public abstract DateTime Today { get; }

        /// <summary>
        /// Returns a DateTime instance representing Now.
        /// </summary>
        public abstract DateTime Now { get; }
    }
}