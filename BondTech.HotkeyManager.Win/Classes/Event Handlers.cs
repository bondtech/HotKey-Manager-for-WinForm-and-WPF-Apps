﻿namespace BondTech.HotkeyManagement.Win
{
    #region **Event Handlers
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement GlobalHotKeyPressed event
    /// </summary>
    public delegate void GlobalHotKeyEventHandler(object sender, GlobalHotKeyEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement LocalHotKeyPressed event
    /// </summary>
    public delegate void LocalHotKeyEventHandler(object sender, LocalHotKeyEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement PreChordStarted event
    /// </summary>
    public delegate void PreChordHotkeyEventHandler(object sender, PreChordHotKeyEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement ChordHotKeyPressed event
    /// </summary>
    public delegate void ChordHotKeyEventHandler(object sender, ChordHotKeyEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement HotKeyIsSet event
    /// </summary>
    public delegate void HotKeyIsSetEventHandler(object sender, HotKeyIsSetEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement HotKeyPressed event
    /// </summary>
    public delegate void HotKeyEventHandler(object sender, HotKeyEventArgs e);
    /// <summary>Represents the method that will handle a BondTech.HotKeyManagement KeyboardHook event
    /// </summary>
    public delegate void KeyboardHookEventHandler(object sender, KeyboardHookEventArgs e);
    #endregion
}
