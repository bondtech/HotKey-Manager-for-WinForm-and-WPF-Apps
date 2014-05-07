using System;
using System.Runtime.InteropServices;

namespace BondTech.HotkeyManagement.Win
{
    #region **Enums and structs.
    /// <summary>Defines the key to use as Modifier.
    /// </summary>
    [Flags]
    public enum Modifiers
    {
        /// <summary>Specifies that the key should be treated as is, without any modifier.
        /// </summary>
        None = 0x0000,
        /// <summary>Specifies that the Accelerator key (ALT) is pressed with the key.
        /// </summary>
        Alt = 0x0001,
        /// <summary>Specifies that the Control key is pressed with the key.
        /// </summary>
        Control = 0x0002,
        /// <summary>Specifies that the Shift key is pressed with the associated key.
        /// </summary>
        Shift = 0x0004,
        /// <summary>Specifies that the Window key is pressed with the associated key.
        /// </summary>
        Win = 0x0008
    }

    public enum RaiseLocalEvent
    {
        OnKeyDown = 0x100, //Also 256. Same as WM_KEYDOWN.
        OnKeyUp = 0x101 //Also 257, Same as WM_KEYUP.
    }

    internal enum KeyboardMessages : int
    {
        /// <summary>A key is down.
        /// </summary>
        WmKeydown = 0x0100,
        /// <summary>A key is released.
        /// </summary>
        WmKeyup = 0x0101,
        /// <summary> Same as KeyDown but captures keys pressed after Alt.
        /// </summary>
        WmSyskeydown = 0x0104,
        /// <summary>Same as KeyUp but captures keys pressed after Alt.
        /// </summary>
        WmSyskeyup = 0x0105,
        /// <summary> When a hotkey is pressed.
        /// </summary>
        WmHotKey = 0x0312
    }

    internal enum KeyboardHookEnum : int
    {
        KeyboardHook = 0xD,
        Keyboard_ExtendedKey = 0x1,
        Keyboard_KeyUp = 0x2
    }

    /// <summary>
    /// The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        /// <summary>
        /// Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
        /// </summary>
        public int VirtualKeyCode;
        /// <summary>
        /// Specifies a hardware scan code for the key. 
        /// </summary>
        public int ScanCode;
        /// <summary>
        /// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public int Flags;
        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public int Time;
        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        public int ExtraInfo;
    }

    public enum KeyboardEventNames
    {
        KeyDown,
        KeyUp
    }
    #endregion
}
