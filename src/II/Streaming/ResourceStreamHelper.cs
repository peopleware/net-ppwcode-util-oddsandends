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

using System;
using System.IO;
using System.Reflection;

namespace PPWCode.Util.OddsAndEnds.II.Streaming
{
    public static class ResourceStreamHelper
    {
        public static string WriteEmbeddedResourceToTempFile(
            Assembly assembly,
            string nameSpacename,
            string resourceName)
        {
            string temporaryFolder = Path.GetTempPath();
            string temporaryFileName = Path.Combine(
                temporaryFolder,
                string.Format("{0}-{1}", DateTime.Now.Ticks, resourceName));
            while (File.Exists(temporaryFileName))
            {
                temporaryFileName = Path.Combine(
                    temporaryFolder,
                    string.Format("{0}-{1}", DateTime.Now.Ticks, resourceName));
            }

            using (Stream resourceStream =
                assembly.GetManifestResourceStream(string.Concat(nameSpacename, resourceName)))
            {
                using (Stream file = File.Open(temporaryFileName, FileMode.CreateNew))
                {
                    CopyStream(resourceStream, file);
                }
            }

            return temporaryFileName;
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[64 * 1024];
            int length;
            while ((length = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, length);
            }
        }
    }
}