
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
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FSO.Common.Utils
{

    /// <summary>
    /// In:
    /// - String Containing BBCode
    /// 
    /// [color=red]Hello![/color]
    /// [color=#FF0000]Also [size=20]red[/size][/color]
    /// \[ escaped \] square brackets.
    /// 
    /// Out:
    /// - String Without BBCode
    /// - Ordered list of BBCodeCommands, and the index in the converted string they occur at.
    /// </summary>
    public class BBCodeParser
    {
        public string Stripped;
        public List<BBCodeCommand> Commands = new List<BBCodeCommand>();

        public BBCodeParser(string input)
        {
            var stripped = new StringBuilder();
            int index = 0;
            while (index < input.Length)
            {
                var newIndex = input.IndexOf('[', index);
                if (newIndex == -1)
                {
                    newIndex = input.Length;
                    //no more commands.
                    //render the rest of the string and break out.
                    stripped.Append(input.Substring(index, newIndex - index));
                    break;
                }
                stripped.Append(input.Substring(index, newIndex - index));
                //we found the start of a bbcode. is it escaped?
                if (newIndex > 0 && input[newIndex - 1] == '\\')
                {
                    //draw the bracket.
                    stripped[stripped.Length - 1] = '['; //replace the backslash with the leftbracket
                    index = newIndex + 1; //continue after the bracket
                } else
                {
                    //find our right bracket
                    var endIndex = input.IndexOf(']', newIndex);
                    if (endIndex == -1)
                    {
                        //fail safe. 
                        stripped.Append('[');
                        index = newIndex + 1; //continue after the bracket
                    } else
                    {
                        Commands.Add(new BBCodeCommand(input.Substring(newIndex + 1, (endIndex - newIndex) - 1), stripped.Length));
                        index = endIndex + 1;
                    }
                }
            }
            Stripped = stripped.ToString();
        }

        public static string SanitizeBB(string input)
        {
            if (input.LastOrDefault() == '\\') input += ' ';
            return input.Replace("[", "\\[");
        }
    }

    public class BBCodeCommand
    {
        public BBCodeCommandType Type;
        public string Parameter;
        public int Index;
        public bool Close;

        public BBCodeCommand(string cmd, int index)
        {
            Index = index;
            if (cmd[0] == '/')
            {
                Close = true;
                cmd = cmd.Substring(1);
            }
            var split = cmd.Split('=');
            if (split.Length > 1) Parameter = split[1];
            System.Enum.TryParse(split[0], out Type);
        }

        public Color ParseColor()
        {
            if (Parameter == null || Parameter.Length == 0) return Color.White;
            //todo: search color static members for named colours
            //for now we only support hex
            if (Parameter.Length == 7 && Parameter[0] == '#')
            {
                uint rgb;
                if (uint.TryParse(Parameter.Substring(1), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out rgb)) {
                    return new Color((byte)(rgb >> 16), (byte)(rgb >> 8), (byte)(rgb), (byte)255);
                }
            } else
            {
                var color = typeof(Color).GetProperties().Where(x => x.PropertyType == typeof(Color) && x.Name.ToLowerInvariant() == Parameter.ToLowerInvariant()).FirstOrDefault();
                if (color != null)
                {
                    return (Color)color.GetValue(null);
                }
            }
            return Color.White;
        }
    }

    public enum BBCodeCommandType
    {
        unknown,
        color,
        size,
        emoji,
        s
    }
}
