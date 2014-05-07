using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BondTech.HotkeyManagement.Win
{
    //A class to keep share procedures
    public static class HotKeyShared
    {
        /// <summary>Checks if a string is a valid Hotkey name.
        /// </summary>
        /// <param name="text">The string to check</param>
        /// <returns>true if the name is valid.</returns>
        public static bool IsValidHotkeyName(string text)
        {
            //If the name starts with a number, contains space or is null, return false.
            if (string.IsNullOrEmpty(text)) return false;

            if (text.Contains(" ") || char.IsDigit((char)text.ToCharArray().GetValue(0)))
                return false;

            return true;
        }
        /// <summary>Parses a shortcut string like 'Control + Alt + Shift + V' and returns the key and modifiers.
        /// </summary>
        /// <param name="text">The shortcut string to parse.</param>
        /// <returns>The Modifier in the lower bound and the key in the upper bound.</returns>
        public static object[] ParseShortcut(string text)
        {
            bool HasAlt = false; bool HasControl = false; bool HasShift = false; bool HasWin = false;

            Modifiers Modifier = Modifiers.None;		//Variable to contain modifier.
            Keys key = 0;           //The key to register.
            int current = 0;

            string[] result;
            string[] separators = new string[] { " + " };
            result = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Iterate through the keys and find the modifier.
            foreach (string entry in result)
            {
                //Find the Control Key.
                if (entry.Trim() == Keys.Control.ToString())
                {
                    HasControl = true;
                }
                //Find the Alt key.
                if (entry.Trim() == Keys.Alt.ToString())
                {
                    HasAlt = true;
                }
                //Find the Shift key.
                if (entry.Trim() == Keys.Shift.ToString())
                {
                    HasShift = true;
                }

                //Find the Window key.
                if (entry.Trim() == Keys.LWin.ToString() && current != result.Length - 1)
                {
                    HasWin = true;
                }

                current++;
            }

            if (HasControl) { Modifier |= Modifiers.Control; }
            if (HasAlt) { Modifier |= Modifiers.Alt; }
            if (HasShift) { Modifier |= Modifiers.Shift; }
            if (HasWin) { Modifier |= Modifiers.Win; }

            KeysConverter keyconverter = new KeysConverter();
            key = (Keys)keyconverter.ConvertFrom(result.GetValue(result.Length - 1));

            return new object[] { Modifier, key };
        }
        /// <summary>Parses a shortcut string like 'Control + Alt + Shift + V' and returns the key and modifiers.
        /// </summary>
        /// <param name="text">The shortcut string to parse.</param>
        /// <param name="separator">The delimiter for the shortcut.</param>
        /// <returns>The Modifier in the lower bound and the key in the upper bound.</returns>
        public static object[] ParseShortcut(string text, string separator)
        {
            bool HasAlt = false; bool HasControl = false; bool HasShift = false; bool HasWin = false;

            Modifiers Modifier = Modifiers.None;		//Variable to contain modifier.
            Keys key = 0;           //The key to register.
            int current = 0;

            string[] result;
            string[] separators = new string[] { separator };
            result = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Iterate through the keys and find the modifier.
            foreach (string entry in result)
            {
                //Find the Control Key.
                if (entry.Trim() == Keys.Control.ToString())
                {
                    HasControl = true;
                }
                //Find the Alt key.
                if (entry.Trim() == Keys.Alt.ToString())
                {
                    HasAlt = true;
                }
                //Find the Shift key.
                if (entry.Trim() == Keys.Shift.ToString())
                {
                    HasShift = true;
                }
                //Find the Window key.
                if (entry.Trim() == Keys.LWin.ToString() && current != result.Length - 1)
                {
                    HasWin = true;
                }

                current++;
            }

            if (HasControl) { Modifier |= Modifiers.Control; }
            if (HasAlt) { Modifier |= Modifiers.Alt; }
            if (HasShift) { Modifier |= Modifiers.Shift; }
            if (HasWin) { Modifier |= Modifiers.Win; }

            KeysConverter keyconverter = new KeysConverter();
            key = (Keys)keyconverter.ConvertFrom(result.GetValue(result.Length - 1));

            return new object[] { Modifier, key };
        }
        /// <summary>Combines the modifier and key to a shortcut.
        /// Changes Control;Shift;Alt;T to Control + Shift + Alt + T
        /// </summary>
        /// <param name="mod">The modifier.</param>
        /// <param name="key">The key.</param>
        /// <returns>A string representation of the modifier and key.</returns>
        public static string CombineShortcut(Modifiers mod, Keys key)
        {
            string hotkey = "";
            foreach (Modifiers a in new HotKeyShared.ParseModifier((int)mod))
            {
                hotkey += a.ToString() + " + ";
            }

            if (hotkey.Contains(Modifiers.None.ToString())) hotkey = "";
            hotkey += key.ToString();
            return hotkey;
        }
        /// <summary>Combines the modifier and key to a shortcut.
        /// Changes Control;Shift;Alt; to Control + Shift + Alt
        /// </summary>
        /// <param name="mod">The modifier.</param>
        /// <returns>A string representation of the modifier</returns>
        public static string CombineShortcut(Modifiers mod)
        {
            string hotkey = "";
            foreach (Modifiers a in new HotKeyShared.ParseModifier((int)mod))
            {
                hotkey += a.ToString() + " + ";
            }

            if (hotkey.Contains(Modifiers.None.ToString())) hotkey = "";
            if (hotkey.Trim().EndsWith("+")) hotkey = hotkey.Trim().Substring(0, hotkey.Length - 1);

            return hotkey;
        }
        /// <summary>Allows the conversion of an integer to its modifier representation.
        /// </summary>
        public struct ParseModifier : IEnumerable
        {
            private List<Modifiers> Enumeration;
            public bool HasAlt;
            public bool HasControl;
            public bool HasShift;
            public bool HasWin;

            /// <summary>Initializes this class.
            /// </summary>
            /// <param name="Modifier">The integer representation of the modifier to parse.</param>
            public ParseModifier(int Modifier)
            {
                Enumeration = new List<Modifiers>();
                HasAlt = false;
                HasWin = false;
                HasShift = false;
                HasControl = false;
                switch (Modifier)
                {
                    case 0:
                        Enumeration.Add(Modifiers.None);
                        break;
                    case 1:
                        HasAlt = true;
                        Enumeration.Add(Modifiers.Alt);
                        break;
                    case 2:
                        HasControl = true;
                        Enumeration.Add(Modifiers.Control);
                        break;
                    case 3:
                        HasAlt = true;
                        HasControl = true;
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Alt);
                        break;
                    case 4:
                        HasShift = true;
                        Enumeration.Add(Modifiers.Shift);
                        break;
                    case 5:
                        HasShift = true;
                        HasAlt = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Alt);
                        break;
                    case 6:
                        HasShift = true;
                        HasControl = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Control);
                        break;
                    case 7:
                        HasControl = true;
                        HasShift = true;
                        HasAlt = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Alt);
                        break;
                    case 8:
                        HasWin = true;
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 9:
                        HasAlt = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Alt);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 10:
                        HasControl = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 11:
                        HasControl = true;
                        HasAlt = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Alt);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 12:
                        HasShift = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 13:
                        HasShift = true;
                        HasAlt = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Alt);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 14:
                        HasShift = true;
                        HasControl = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    case 15:
                        HasShift = true;
                        HasControl = true;
                        HasAlt = true;
                        HasWin = true;
                        Enumeration.Add(Modifiers.Shift);
                        Enumeration.Add(Modifiers.Control);
                        Enumeration.Add(Modifiers.Alt);
                        Enumeration.Add(Modifiers.Win);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("The argument is parsed is more than the expected range", "Modifier");
                }
            }
            /// <summary>Initializes this class.
            /// </summary>
            /// <param name="mod">the modifier to parse.</param>
            public ParseModifier(Modifiers mod) : this((int)mod) { }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Enumeration.GetEnumerator();
            }
        }
    }
}
