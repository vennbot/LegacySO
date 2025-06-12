// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using System.Collections.Generic;
using System.IO;

namespace FSO.Files
{
    public class IniFile : Dictionary<string, IniSection>
    {
        public static IniFile Read(string path){
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)){
                var result = new IniFile();
                result.Decode(stream);
                return result;
            }
        }


        public void Decode(Stream stream)
        {
            IniSection currentSection = null;

            using (var reader = new StreamReader(stream)){
                string line = null;

                while((line = reader.ReadLine()) != null){
                    line = line.TrimStart();

                    if (line.StartsWith(";")){
                        //Comment
                    }else if (line.StartsWith("[")){
                        //Section
                        currentSection = new IniSection();
                        currentSection.Name = line.Substring(1);
                        currentSection.Name = currentSection.Name.Substring(0, currentSection.Name.LastIndexOf("]"));
                        this.Add(currentSection.Name, currentSection);
                    }
                    else
                    {
                        //Could be a key / value
                        var split = line.IndexOf("=");
                        if(split != -1 && currentSection != null && split+1 < line.Length){
                            var key = line.Substring(0, split).Trim();
                            var value = line.Substring(split + 1).TrimStart();

                            if(key.Length > 0)
                            {
                                currentSection[key] = value;
                            }
                        }
                    }
                }
            }
        }
    }

    public class IniSection : Dictionary<string, string>
    {
        public string Name { get; set; }
    }
}
