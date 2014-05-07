/* If you have suggestions, find bugs or feel there is something that could be added.
 * Leave a message and I'll get back to you as soon as possible.
 * Thanks, Bond. :D
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BondTech.HotkeyManagement.Win
{
    #region **HotKeyManager.
    /// <summary>Initializes a new instance of this class.
    /// The HotKeyManager needed for working with HotKeys.
    /// </summary>
    public sealed class HotKeyManager : IMessageFilter, IDisposable // , IEnumerable, IEnumerable<GlobalHotKey>, IEnumerable<LocalHotKey>, IEnumerable<ChordHotKey>
    {
        #region **Properties and enum.
        /// <summary>Specifies the container to search in the HotKeyExists function.
        /// </summary>
        public enum CheckKey
        {
            /// <summary>Specifies that the HotKey should be checked against Local and Global HotKeys.
            /// </summary>
            Both = 0,
            /// <summary>Specifies that the HotKey should be checked against GlobalHotKeys only.
            /// </summary>
            GlobalHotKey = 1,
            /// <summary>Specifies that the HotKey should be checked against LocalHotKeys only.
            /// </summary>
            LocalHotKey = 2
        }

        IntPtr FormHandle; //Will hold the handle of the form.
        private static readonly SerialCounter idGen = new SerialCounter(-1); //Will keep track of all the registered GlobalHotKeys
        private IntPtr hookId;
        private Win32.HookProc callback;
        private bool hooked;
        private bool autodispose;
        static bool InChordMode; //Will determine if a chord has started.
        private Form ManagerForm
        {
            get
            {
                return (Form)Form.FromHandle(this.FormHandle);
            }
        }

        private List<GlobalHotKey> GlobalHotKeyContainer = new List<GlobalHotKey>(); //Will hold our GlobalHotKeys
        private List<LocalHotKey> LocalHotKeyContainer = new List<LocalHotKey>(); //Will hold our LocalHotKeys.
        private List<ChordHotKey> ChordHotKeyContainer = new List<ChordHotKey>(); //Will hold our ChordHotKeys.

        //Keep the previous key and modifier that started a chord.
        Keys PreChordKey;
        Modifiers PreChordModifier;

        /// <summary>Determines if exceptions should be raised when an error occurs.
        /// </summary>
        public bool SuppressException { get; set; } //Determines if you want exceptions to be thrown.
        /// <summary>Gets or sets if the Manager should still function when its owner form is inactive.
        /// </summary>
        public bool DisableOnManagerFormInactive { get; set; }
        /// <summary>Determines if the HotKeymanager should be automatically disposed when the manager form is closed.
        /// </summary>
        public bool AutoDispose
        {
            get
            {
                return autodispose;
            }
            set
            {
                if (value)
                    this.ManagerForm.FormClosed += delegate { this.Dispose(); };
                else
                    this.ManagerForm.FormClosing -= delegate { this.Dispose(); };

                autodispose = value;
            }
        }
        /// <summary>Determines if the manager is active.
        /// </summary>
        public bool Enabled { get; set; } //Refuse to listen to any windows message.
        /// <summary>Specifies if the keyboard has been hooked.
        /// </summary>
        public bool KeyboardHooked { get { return hooked; } }
        /// <summary>Returns the total number of registered GlobalHotkeys.
        /// </summary>
        public int GlobalHotKeyCount { get; private set; }
        /// <summary>Returns the total number of registered LocalHotkeys.
        /// </summary>
        public int LocalHotKeyCount { get; private set; }
        /// <summary>Returns the total number of registered ChordHotKeys.
        /// </summary>
        public int ChordHotKeyCount { get; private set; }
        /// <summary>Returns the total number of registered HotKey with the HotKeyManager.
        /// </summary>
        public int HotKeyCount { get { return LocalHotKeyCount + GlobalHotKeyCount + ChordHotKeyCount; } }
        #endregion

        #region **Constructors.
        /// <summary>Creates a new HotKeyManager object
        /// </summary>
        /// <param name="form">The form to associate hotkeys with. Must not be null.</param>
        public HotKeyManager(IWin32Window form) : this(form, false) { }
        /// <summary>Creates a new HotKeyManager object.
        /// </summary>
        /// <param name="form">The form to associate hotkeys with. Must not be null.</param>
        /// <param name="SuppressExceptions">Specifies if you want exceptions to be handled.</param>
        /// <exception cref="System.ArgumentNullException">thrown if the form is null.</exception>
        public HotKeyManager(IWin32Window form, bool SuppressExceptions)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            this.SuppressException = SuppressExceptions;
            this.FormHandle = form.Handle;
            this.Enabled = true;
            this.AutoDispose = true;

            Application.AddMessageFilter(this); //Allow this class to receive Window messages.
        }
        #endregion

        #region **Event Handlers
        /// <summary>Will be raised if a registered GlobalHotKey is pressed
        /// </summary>
        public event GlobalHotKeyEventHandler GlobalHotKeyPressed;
        /// <summary>Will be raised if an local Hotkey is pressed.
        /// </summary>
        public event LocalHotKeyEventHandler LocalHotKeyPressed;
        /// <summary>Will be raised if a Key is help down on the keyboard.
        /// The keyboard has to be hooked for this event to be raised.
        /// </summary>
        public event KeyboardHookEventHandler KeyBoardKeyDown;
        /// <summary>Will be raised if a key is released on the keyboard.
        /// The keyboard has to be hooked for this event to be raised.
        /// </summary>
        public event KeyboardHookEventHandler KeyBoardKeyUp;
        /// <summary>Will be raised if a key is pressed on the keyboard.
        /// The keyboard has to be hooked for this event to be raised.
        /// </summary>
        public event KeyboardHookEventHandler KeyBoardKeyEvent;
        /// <summary>Will be raised if a key is pressed in the current application.
        /// </summary>
        public event HotKeyEventHandler KeyPressEvent;
        /// <summary>Will be raised if a Chord has started.
        /// </summary>
        public event PreChordHotkeyEventHandler ChordStarted;
        /// <summary>Will be raised if a chord is pressed.
        /// </summary>
        public event ChordHotKeyEventHandler ChordPressed;
        #endregion

        #region **Enumerations.
        /// <summary>Use for enumerating through all GlobalHotKeys.
        /// </summary>
        public IEnumerable EnumerateGlobalHotKeys { get { return GlobalHotKeyContainer; } }
        /// <summary>Use for enumerating through all LocalHotKeys.
        /// </summary>
        public IEnumerable EnumerateLocalHotKeys { get { return LocalHotKeyContainer; } }
        /// <summary>Use for enumerating through all ChordHotKeys.
        /// </summary>
        public IEnumerable EnumerateChordHotKeys { get { return ChordHotKeyContainer; } }

        //IEnumerator<GlobalHotKey> IEnumerable<GlobalHotKey>.GetEnumerator()
        //{
        //    return GlobalHotKeyContainer.GetEnumerator();
        //}

        //IEnumerator<LocalHotKey> IEnumerable<LocalHotKey>.GetEnumerator()
        //{
        //    return LocalHotKeyContainer.GetEnumerator();
        //}

        //IEnumerator<ChordHotKey> IEnumerable<ChordHotKey>.GetEnumerator()
        //{
        //    return ChordHotKeyContainer.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    yield break;
        //    //return (IEnumerator)((IEnumerable<GlobalHotKey>)this).GetEnumerator();
        //}
        #endregion

        #region **Handle Property Changing.
        void GlobalHotKeyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var kvPair = sender as GlobalHotKey;
            if (kvPair != null)
            {
                if (e.PropertyName == "Enabled")
                {
                    if (kvPair.Enabled)
                        RegisterGlobalHotKey(kvPair.Id, kvPair);
                    else
                        UnregisterGlobalHotKey(kvPair.Id);
                }
                else if (e.PropertyName == "Key" || e.PropertyName == "Modifier")
                {
                    if (kvPair.Enabled)
                    {
                        UnregisterGlobalHotKey(GlobalHotKeyContainer.IndexOf(kvPair));
                        RegisterGlobalHotKey(kvPair.Id, kvPair);
                    }
                }
            }
        }
        #endregion

        #region **Events, Methods and Helpers
        private void RegisterGlobalHotKey(int id, GlobalHotKey hotKey)
        {
            if ((int)FormHandle != 0)
            {
                if (hotKey.Key == Keys.LWin && (hotKey.Modifier & Modifiers.Win) == Modifiers.None)
                    Win32.RegisterHotKey(FormHandle, id, (int)(hotKey.Modifier | Modifiers.Win), (int)hotKey.Key);
                else
                    Win32.RegisterHotKey(FormHandle, id, (int)hotKey.Modifier, (int)(hotKey.Key));

                int error = Marshal.GetLastWin32Error();
                if (error != 0)
                {
                    if (!this.SuppressException)
                    {
                        Exception e = new Win32Exception(error);

                        if (error == 1409)
                            throw new HotKeyAlreadyRegisteredException(e.Message, hotKey, e);
                        else if (error != 2)
                            throw e; //ToDo: Fix here: File not found exception
                    }
                }
            }
            else
                if (!this.SuppressException)
                {
                    throw new InvalidOperationException("Handle is invalid");
                }
        }
        private void UnregisterGlobalHotKey(int id)
        {
            if ((int)FormHandle != 0)
            {
                Win32.UnregisterHotKey(FormHandle, id);
                int error = Marshal.GetLastWin32Error();
                if (error != 0 && error != 2)
                    if (!this.SuppressException)
                    {
                        throw new HotKeyUnregistrationFailedException("The hotkey could not be unregistered", GlobalHotKeyContainer[id], new Win32Exception(error));
                    }
            }
        }

        private class SerialCounter
        {
            public SerialCounter(int start)
            {
                Current = start;
            }

            public int Current { get; private set; }

            public int Next()
            {
                return ++Current;
            }
        }

        /// <summary>Registers a GlobalHotKey if enabled.
        /// </summary>
        /// <param name="hotKey">The hotKey which will be added. Must not be null and can be registered only once.</param>
        /// <exception cref="HotKeyAlreadyRegisteredException">Thrown is a GlobalHotkey with the same name, and or key and modifier has already been added.</exception>
        /// <exception cref="System.ArgumentNullException">thrown if a the HotKey to be added is null, or the key is not specified.</exception>
        public bool AddGlobalHotKey(GlobalHotKey hotKey)
        {
            if (hotKey == null)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value");

                return false;
            }
            if (hotKey.Key == 0)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value.Key");

                return false;
            }
            if (GlobalHotKeyContainer.Contains(hotKey))
            {
                if (!this.SuppressException)
                    throw new HotKeyAlreadyRegisteredException("HotKey already registered!", hotKey);

                return false;
            }

            int id = idGen.Next();
            if (hotKey.Enabled)
                RegisterGlobalHotKey(id, hotKey);
            hotKey.Id = id;
            hotKey.PropertyChanged += GlobalHotKeyPropertyChanged;
            GlobalHotKeyContainer.Add(hotKey);
            ++GlobalHotKeyCount;
            return true;
        }
        /// <summary>Registers a LocalHotKey.
        /// </summary>
        /// <param name="hotKey">The hotKey which will be added. Must not be null and can be registered only once.</param>
        /// <exception cref="HotKeyAlreadyRegisteredException">thrown if a LocalHotkey with the same name and or key and modifier has already been added.</exception>
        public bool AddLocalHotKey(LocalHotKey hotKey)
        {
            if (hotKey == null)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value");

                return false;
            }
            if (hotKey.Key == 0)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value.Key");

                return false;
            }

            //Check if a chord already has its BaseKey and BaseModifier.
            bool ChordExits = ChordHotKeyContainer.Exists
            (
                delegate(ChordHotKey f)
                {
                    return (f.BaseKey == hotKey.Key && f.BaseModifier == hotKey.Modifier);
                }
            );

            if (LocalHotKeyContainer.Contains(hotKey) || ChordExits)
            {
                if (!this.SuppressException)
                    throw new HotKeyAlreadyRegisteredException("HotKey already registered!", hotKey);

                return false;
            }

            LocalHotKeyContainer.Add(hotKey);
            ++LocalHotKeyCount;
            return true;
        }
        /// <summary>Registers a ChordHotKey.
        /// </summary>
        /// <param name="hotKey">The hotKey which will be added. Must not be null and can be registered only once.</param>
        /// <returns>True if registered successfully, false otherwise.</returns>
        /// <exception cref="HotKeyAlreadyRegisteredException">thrown if a LocalHotkey with the same name and or key and modifier has already been added.</exception>
        public bool AddChordHotKey(ChordHotKey hotKey)
        {
            if (hotKey == null)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value");

                return false;
            }
            if (hotKey.BaseKey == 0 || hotKey.ChordKey == 0)
            {
                if (!this.SuppressException)
                    throw new ArgumentNullException("value.Key");

                return false;
            }

            //Check if a LocalHotKey already has its Key and Modifier.
            bool LocalExists = LocalHotKeyContainer.Exists
            (
                delegate(LocalHotKey f)
                {
                    return (f.Key == hotKey.BaseKey && f.Modifier == hotKey.BaseModifier);
                }
            );

            if (ChordHotKeyContainer.Contains(hotKey) || LocalExists)
            {
                if (!this.SuppressException)
                    throw new HotKeyAlreadyRegisteredException("HotKey already registered!", hotKey);

                return false;
            }

            ChordHotKeyContainer.Add(hotKey);
            ++ChordHotKeyCount;
            return true;
        }

        /// <summary>Unregisters a GlobalHotKey.
        /// </summary>
        /// <param name="hotKey">The hotKey to be removed</param>
        /// <returns>True if success, otherwise false</returns>
        public bool RemoveGlobalHotKey(GlobalHotKey hotKey)
        {
            if (GlobalHotKeyContainer.Remove(hotKey) == true)
            {
                --GlobalHotKeyCount;

                if (hotKey.Enabled)
                    UnregisterGlobalHotKey(hotKey.Id);

                hotKey.PropertyChanged -= GlobalHotKeyPropertyChanged;
                return true;
            }
            else { return false; }

        }
        /// <summary>Unregisters a LocalHotKey.
        /// </summary>
        /// <param name="hotKey">The hotKey to be removed</param>
        /// <returns>True if success, otherwise false</returns>
        public bool RemoveLocalHotKey(LocalHotKey hotKey)
        {
            if (LocalHotKeyContainer.Remove(hotKey) == true)
            { --LocalHotKeyCount; return true; }
            else { return false; }
        }
        /// <summary>Unregisters a ChordHotKey.
        /// </summary>
        /// <param name="hotKey">The hotKey to be removed</param>
        /// <returns>True if success, otherwise false</returns>
        public bool RemoveChordHotKey(ChordHotKey hotKey)
        {
            if (ChordHotKeyContainer.Remove(hotKey) == true)
            { --ChordHotKeyCount; return true; }
            else { return false; }
        }
        /// <summary>Removes the hotkey(Local, Chord or Global) with the specified name.
        /// </summary>
        /// <param name="name">The name of the hotkey.</param>
        /// <returns>True if successful and false otherwise.</returns>
        public bool RemoveHotKey(string name)
        {
            LocalHotKey local = LocalHotKeyContainer.Find
                (
                delegate(LocalHotKey l)
                {
                    return (l.Name == name);
                }
            );

            if (local != null) { return RemoveLocalHotKey(local); }

            ChordHotKey chord = ChordHotKeyContainer.Find
                (
                delegate(ChordHotKey c)
                {
                    return (c.Name == name);
                }
            );

            if (chord != null) { return RemoveChordHotKey(chord); }

            GlobalHotKey global = GlobalHotKeyContainer.Find
                (
                delegate(GlobalHotKey g)
                {
                    return (g.Name == name);
                }
            );

            if (global != null) { return RemoveGlobalHotKey(global); }

            return false;
        }

        /// <summary>Checks if a HotKey has been registered.
        /// </summary>
        /// <param name="name">The name of the HotKey.</param>
        /// <returns>True if the HotKey has been registered, false otherwise.</returns>
        public bool HotKeyExists(string name)
        {
            LocalHotKey local = LocalHotKeyContainer.Find
                (
                delegate(LocalHotKey l)
                {
                    return (l.Name == name);
                }
            );

            if (local != null) { return true; }

            ChordHotKey chord = ChordHotKeyContainer.Find
                (
                delegate(ChordHotKey c)
                {
                    return (c.Name == name);
                }
            );

            if (chord != null) { return true; }

            GlobalHotKey global = GlobalHotKeyContainer.Find
                (
                delegate(GlobalHotKey g)
                {
                    return (g.Name == name);
                }
            );

            if (global != null) { return true; }

            return false;
        }
        /// <summary>Checks if a ChordHotKey has been registered.
        /// </summary>
        /// <param name="chordhotkey">The ChordHotKey to check.</param>
        /// <returns>True if the ChordHotKey has been registered, false otherwise.</returns>
        public bool HotKeyExists(ChordHotKey chordhotkey)
        {
            return ChordHotKeyContainer.Exists
                (
                delegate(ChordHotKey c)
                {
                    return (c == chordhotkey);
                }
            );
        }
        /// <summary>Checks if a hotkey has already been registered as a Local or Global HotKey.
        /// </summary>
        /// <param name="shortcut">The hotkey string to check.</param>
        /// <param name="ToCheck">The HotKey type to check.</param>
        /// <returns>True if the HotKey is already registered, false otherwise.</returns>
        public bool HotKeyExists(string shortcut, CheckKey ToCheck)
        {
            Keys Key = (Keys)HotKeyShared.ParseShortcut(shortcut).GetValue(1);
            Modifiers Modifier = (Modifiers)HotKeyShared.ParseShortcut(shortcut).GetValue(0);
            switch (ToCheck)
            {
                case CheckKey.GlobalHotKey:
                    return GlobalHotKeyContainer.Exists
                        (
                        delegate(GlobalHotKey g)
                        {
                            return (g.Key == Key && g.Modifier == Modifier);
                        }
                    );

                case CheckKey.LocalHotKey:
                    return (LocalHotKeyContainer.Exists
                        (
                        delegate(LocalHotKey l)
                        {
                            return (l.Key == Key && l.Modifier == Modifier);
                        }
                    )
                    |
                    ChordHotKeyContainer.Exists
                    (
                    delegate(ChordHotKey c)
                    {
                        return (c.BaseKey == Key && c.BaseModifier == Modifier);
                    }));

                case CheckKey.Both:
                    return (HotKeyExists(shortcut, CheckKey.GlobalHotKey) ^ HotKeyExists(shortcut, CheckKey.LocalHotKey));
            }
            return false;
        }
        /// <summary>Checks if a hotkey has already been registered as a Local or Global HotKey.
        /// </summary>
        /// <param name="key">The key of the HotKey.</param>
        /// <param name="modifier">The modifier of the HotKey.</param>
        /// <param name="ToCheck">The HotKey type to check.</param>
        /// <returns>True if the HotKey is already registered, false otherwise.</returns>
        public bool HotKeyExists(Keys key, Modifiers modifier, CheckKey ToCheck)
        {
            return (HotKeyExists(HotKeyShared.CombineShortcut(modifier, key), ToCheck));
        }
        #endregion

        #region **Listen to Window Messages.
        public bool PreFilterMessage(ref Message m)
        {
            if (!Enabled) { return false; }

            //Check if the form that the HotKeyManager is registered to is inactive.
            if (DisableOnManagerFormInactive)
                if (Form.ActiveForm != null && this.ManagerForm != Form.ActiveForm) { return false; }

            //For LocalHotKeys, determine if modifiers Alt, Shift and Control is pressed.
            Microsoft.VisualBasic.Devices.Keyboard UserKeyBoard = new Microsoft.VisualBasic.Devices.Keyboard();
            bool AltPressed = UserKeyBoard.AltKeyDown;
            bool ControlPressed = UserKeyBoard.CtrlKeyDown;
            bool ShiftPressed = UserKeyBoard.ShiftKeyDown;

            Modifiers LocalModifier = Modifiers.None;
            if (AltPressed) { LocalModifier = Modifiers.Alt; }
            if (ControlPressed) { LocalModifier |= Modifiers.Control; }
            if (ShiftPressed) { LocalModifier |= Modifiers.Shift; }

            switch ((KeyboardMessages)m.Msg)
            {
                case (KeyboardMessages.WmSyskeydown):
                case (KeyboardMessages.WmKeydown):
                    Keys keydownCode = (Keys)(int)m.WParam & Keys.KeyCode;

                    if (KeyPressEvent != null)
                        KeyPressEvent(this, new HotKeyEventArgs(keydownCode, LocalModifier, RaiseLocalEvent.OnKeyDown));

                    //Check if a chord has started.
                    if (InChordMode)
                    {
                        //Check if the Key down is a modifier, we'll have to wait for a real key.
                        switch (keydownCode)
                        {
                            case Keys.Control:
                            case Keys.ControlKey:
                            case Keys.LControlKey:
                            case Keys.RControlKey:
                            case Keys.Shift:
                            case Keys.ShiftKey:
                            case Keys.LShiftKey:
                            case Keys.RShiftKey:
                            case Keys.Alt:
                            case Keys.Menu:
                            case Keys.LMenu:
                            case Keys.RMenu:
                            case Keys.LWin:
                                return true;
                        }

                        ChordHotKey ChordMain = ChordHotKeyContainer.Find
                        (
                        delegate(ChordHotKey cm)
                        {
                            return ((cm.BaseKey == PreChordKey) && (cm.BaseModifier == PreChordModifier) && (cm.ChordKey == keydownCode) && (cm.ChordModifier == LocalModifier));
                        }
                    );

                        if (ChordMain != null)
                        {
                            ChordMain.RaiseOnHotKeyPressed();

                            if (ChordPressed != null && ChordMain.Enabled == true)
                                ChordPressed(this, new ChordHotKeyEventArgs(ChordMain));

                            InChordMode = false;
                            return true;
                        }

                        InChordMode = false;
                        new Microsoft.VisualBasic.Devices.Computer().Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
                        return true;
                    }

                    //Check for a LocalHotKey.
                    LocalHotKey KeyDownHotkey = LocalHotKeyContainer.Find
                        (
                        delegate(LocalHotKey d)
                        {
                            return ((d.Key == keydownCode) && (d.Modifier == LocalModifier)
                                && (d.WhenToRaise == RaiseLocalEvent.OnKeyDown));
                        }
                    );

                    if (KeyDownHotkey != null)
                    {
                        KeyDownHotkey.RaiseOnHotKeyPressed();
                        if (LocalHotKeyPressed != null && KeyDownHotkey.Enabled == true)
                            LocalHotKeyPressed(this, new LocalHotKeyEventArgs(KeyDownHotkey));

                        return KeyDownHotkey.SuppressKeyPress;
                    }

                    //Check for ChordHotKeys.
                    ChordHotKey ChordBase = ChordHotKeyContainer.Find
                        (
                        delegate(ChordHotKey c)
                        {
                            return ((c.BaseKey == keydownCode) && (c.BaseModifier == LocalModifier));
                        }
                    );

                    if (ChordBase != null)
                    {
                        PreChordKey = ChordBase.BaseKey;
                        PreChordModifier = ChordBase.BaseModifier;

                        var e = new PreChordHotKeyEventArgs(new LocalHotKey(ChordBase.Name, ChordBase.BaseModifier, ChordBase.BaseKey));
                        if (ChordStarted != null)
                            ChordStarted(this, e);

                        InChordMode = !e.HandleChord;
                        return true;
                    }

                    InChordMode = false;
                    return false;

                case (KeyboardMessages.WmSyskeyup):
                case (KeyboardMessages.WmKeyup):
                    Keys keyupCode = (Keys)(int)m.WParam & Keys.KeyCode;

                    if (KeyPressEvent != null)
                        KeyPressEvent(this, new HotKeyEventArgs(keyupCode, LocalModifier, RaiseLocalEvent.OnKeyDown));

                    LocalHotKey KeyUpHotkey = LocalHotKeyContainer.Find
                        (
                        delegate(LocalHotKey u)
                        {
                            return ((u.Key == keyupCode) && (u.Modifier == LocalModifier)
                                && (u.WhenToRaise == RaiseLocalEvent.OnKeyUp));
                        }
                    );

                    if (KeyUpHotkey != null)
                    {
                        KeyUpHotkey.RaiseOnHotKeyPressed();
                        if (LocalHotKeyPressed != null && KeyUpHotkey.Enabled == true)
                            LocalHotKeyPressed(this, new LocalHotKeyEventArgs(KeyUpHotkey));

                        return KeyUpHotkey.SuppressKeyPress;
                    }
                    return false;

                case (KeyboardMessages.WmHotKey):
                    //var lpInt = (int)m.LParam;
                    //Keys Key = (Keys)((lpInt >> 16) & 0xFFFF);
                    //Modifiers modifier = (Modifiers)(lpInt & 0xFFFF);

                    int Id = (int)m.WParam;

                    GlobalHotKey Pressed = GlobalHotKeyContainer.Find
                        (
                        delegate(GlobalHotKey g)
                        {
                            return ((g.Id == (int)Id));
                        }
                    );

                    Pressed.RaiseOnHotKeyPressed();
                    if (GlobalHotKeyPressed != null)
                        GlobalHotKeyPressed(this, new GlobalHotKeyEventArgs(Pressed));

                    return true;

                default: return false;
            }
        }
        #endregion

        #region **Keyboard Hook.
        private void OnKeyboardKeyDown(KeyboardHookEventArgs e)
        {
            if (KeyBoardKeyDown != null)
                KeyBoardKeyDown(this, e);
            OnKeyboardKeyEvent(e);
        }

        private void OnKeyboardKeyUp(KeyboardHookEventArgs e)
        {
            if (KeyBoardKeyUp != null)
                KeyBoardKeyUp(this, e);
            OnKeyboardKeyEvent(e);
        }

        private void OnKeyboardKeyEvent(KeyboardHookEventArgs e)
        {
            if (KeyBoardKeyEvent != null)
                KeyBoardKeyEvent(this, e);
        }

        /// <summary>Allows the application to listen to all keyboard messages.
        /// </summary>
        public void KeyBoardHook()
        {
            callback = KeyboardHookCallback;
            hookId = Win32.SetWindowsHook((int)KeyboardHookEnum.KeyboardHook, callback);
            hooked = true;
        }
        /// <summary>Stops the application from listening to all keyboard messages.
        /// </summary>
        public void KeyBoardUnHook()
        {
            try
            {
                if (!hooked) return;
                Win32.UnhookWindowsHookEx(hookId);
                callback = null;
                hooked = false;
            }
            catch (MarshalDirectiveException)
            {
                //if (!SuppressException) throw (e);
            }
        }
        /// <summary>
        /// This is the call-back method that is called whenever a keyboard event is triggered.
        /// We use it to call our individual custom events.
        /// </summary>
        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (!Enabled) return Win32.CallNextHookEx(hookId, nCode, wParam, lParam);

            if (nCode >= 0)
            {
                var lParamStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                var e = new KeyboardHookEventArgs(lParamStruct);
                switch ((KeyboardMessages)wParam)
                {
                    case KeyboardMessages.WmSyskeydown:
                    case KeyboardMessages.WmKeydown:
                        e.KeyboardEventName = KeyboardEventNames.KeyDown;
                        OnKeyboardKeyDown(e);
                        break;

                    case KeyboardMessages.WmSyskeyup:
                    case KeyboardMessages.WmKeyup:
                        e.KeyboardEventName = KeyboardEventNames.KeyUp;
                        OnKeyboardKeyUp(e);
                        break;
                }

                if (e.Handled) { return (IntPtr)(-1); }
            }
            return Win32.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
        #endregion

        #region **Simulation.
        /// <summary>Simulates pressing a key.
        /// </summary>
        /// <param name="key">The key to press.</param>
        public void SimulateKeyDown(Keys key)
        {
            Win32.keybd_event(ParseKey(key), 0, 0, 0);
        }
        /// <sum.mary>Simulates releasing a key
        /// </summary>
        /// <param name="key">The key to release.</param>
        public void SimulateKeyUp(Keys key)
        {
            Win32.keybd_event(ParseKey(key), 0, (int)KeyboardHookEnum.Keyboard_KeyUp, 0);
        }
        /// <summary>Simulates pressing a key. The key is pressed, then released.
        /// </summary>
        /// <param name="key">The key to press.</param>
        public void SimulateKeyPress(Keys key)
        {
            SimulateKeyDown(key);
            SimulateKeyUp(key);
        }

        static byte ParseKey(Keys key)
        {
            // Alt, Shift, and Control need to be changed for API function to work with them
            switch (key)
            {
                case Keys.Alt:
                    return (byte)18;
                case Keys.Control:
                    return (byte)17;
                case Keys.Shift:
                    return (byte)16;
                default:
                    return (byte)key;
            }
        }
        #endregion

        #region **Destructor
        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Application.RemoveMessageFilter(this);
                this.SuppressException = true;
            }

            for (int i = GlobalHotKeyContainer.Count - 1; i >= 0; i--)
            {
                RemoveGlobalHotKey(GlobalHotKeyContainer[i]);
            }

            LocalHotKeyContainer.Clear();
            ChordHotKeyContainer.Clear();
            KeyBoardUnHook();
            disposed = true;
        }
        /// <summary>Destroys and releases all memory used by this class.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HotKeyManager()
        {
            this.Dispose(false);
        }

        #endregion
    }
    #endregion
}