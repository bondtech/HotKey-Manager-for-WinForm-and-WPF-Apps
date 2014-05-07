using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Threading;

using Microsoft.Win32; //Contains the file picker dialog.
using BondTech.HotKeyManagement.WPF;

namespace GlobalShortcutCS.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HotKeyManager MyHotKeyManager;

        string CurrentFile;
        bool IsDragging = false;
        bool FileisPlaying = false;
        DispatcherTimer timer;

        public delegate void timerTick();
        timerTick tick;

        #region **HotKeys
        GlobalHotKey ghkPlay = new GlobalHotKey("ghkPlay", ModifierKeys.Control | ModifierKeys.Shift, Keys.P, true);
        GlobalHotKey ghkStop = new GlobalHotKey("ghkStop", ModifierKeys.Shift | ModifierKeys.Alt, Keys.S, true);
        GlobalHotKey ghkFile = new GlobalHotKey("ghkFile", ModifierKeys.Shift | ModifierKeys.Control, Keys.F, true);

        LocalHotKey lhkPlay = new LocalHotKey("lhkPlay", 32);
        LocalHotKey lhkStop = new LocalHotKey("lhkStop", Keys.S);
        LocalHotKey lhkFile = new LocalHotKey("lhkFile", Keys.Enter);
        LocalHotKey lhkScreen = new LocalHotKey("lhkScreen", Keys.F);

        ChordHotKey chotPlay = new ChordHotKey("chotPlay", ModifierKeys.Control, Keys.P, ModifierKeys.Control, Keys.M);
        ChordHotKey chotStop = new ChordHotKey("chotStop", ModifierKeys.Control, Keys.S, ModifierKeys.Control, Keys.M);
        ChordHotKey chotFile = new ChordHotKey("chotFile", ModifierKeys.Control, Keys.F, ModifierKeys.Control, Keys.M);
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            tick = new timerTick(changeStatus);

            hotKeyControl1.HotKeyIsSet += (s, e) =>
            {

                if (MyHotKeyManager.HotKeyExists(e.Shortcut, HotKeyManager.CheckKey.LocalHotKey))
                {
                    
                    e.Cancel = true;
                    MessageBox.Show("This HotKey has already been registered");
                }
            };

            hotKeyControl2.HotKeyIsSet += (o, ex) =>
            {
                if (MyHotKeyManager.HotKeyExists(ex.Shortcut, HotKeyManager.CheckKey.LocalHotKey))
                {
                    ex.Cancel = true;
                    MessageBox.Show("This HotKey has already been registered");
                }
            };
        }

        void MyHotKeyManager_GlobalHotKeyPressed(object sender, GlobalHotKeyEventArgs e)
        {
            switch (e.HotKey.Name.ToLower())
            {
                case "ghkplay":
                    PlayPause();
                    break;

                case "ghkstop":
                    Stop();
                    break;

                case "ghkfile":
                    File();
                    break;
            }
        }

        void MyHotKeyManager_ChordPressed(object sender, ChordHotKeyEventArgs e)
        {
            switch (e.HotKey.Name.ToLower())
            {
                case "chotplay":
                    PlayPause();
                    break;

                case "chotstop":
                    Stop();
                    break;

                case "chotfile":
                    File();
                    break;
            }
        }

        void MyHotKeyManager_LocalHotKeyPressed(object sender, LocalHotKeyEventArgs e)
        {
            switch (e.HotKey.Name.ToLower())
            {
                case "lhkplay":
                    PlayPause();
                    break;

                case "lhkstop":
                    Stop();
                    break;

                case "lhkfile":
                    File();
                    break;

                case "lhkscreen":
                    if (this.WindowState == System.Windows.WindowState.Maximized)
                        this.WindowState = System.Windows.WindowState.Normal;
                    else
                        this.WindowState = System.Windows.WindowState.Maximized;

                    break;
            }
        }

        void PlayPause()
        {
            if (!FileisPlaying)
            {
                if (!string.IsNullOrEmpty(CurrentFile) && System.IO.File.Exists(CurrentFile))
                {
                    myMediaPlayer.Source = new Uri(CurrentFile);
                    myMediaPlayer.Play();

                    FileisPlaying = true;
                    timer.Start();

                    Thread.Sleep(1000);
                    double duration = myMediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                    TimeLine.Maximum = duration;

                    myMediaPlayer.Volume = volumeControl.Value;
                }
            }
            else
            {
                FileisPlaying = false;
                myMediaPlayer.Pause();
                timer.Stop();
            }
        }

        void Stop()
        {
            FileisPlaying = false;
            timer.Stop();
            myMediaPlayer.Source = null;
            myMediaPlayer.Stop();
            TimeLine.Value = 0;
        }

        void File()
        {
            OpenFileDialog MediaPicker = new OpenFileDialog();
            MediaPicker.Filter = "Media Files(*.avi;*.3gp;*.mp3;*.mp4)|*.avi;*.3gp;*.mp3;*.mp4";
            if (MediaPicker.ShowDialog() != null)
            { CurrentFile = MediaPicker.FileName; myMediaPlayer.Close(); FileisPlaying = false; PlayPause(); }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterHotKeys();
            File();
        }

        void RegisterHotKeys()
        {
            MyHotKeyManager = new HotKeyManager(this);

            MyHotKeyManager.AddGlobalHotKey(ghkPlay);
            MyHotKeyManager.AddGlobalHotKey(ghkStop);
            MyHotKeyManager.AddGlobalHotKey(ghkFile);

            MyHotKeyManager.AddLocalHotKey(lhkPlay);
            MyHotKeyManager.AddLocalHotKey(lhkStop);
            MyHotKeyManager.AddLocalHotKey(lhkFile);
            MyHotKeyManager.AddLocalHotKey(lhkScreen);

            MyHotKeyManager.AddChordHotKey(chotPlay);
            MyHotKeyManager.AddChordHotKey(chotStop);
            MyHotKeyManager.AddChordHotKey(chotFile);

            MyHotKeyManager.GlobalHotKeyPressed +=new GlobalHotKeyEventHandler(MyHotKeyManager_GlobalHotKeyPressed);
            MyHotKeyManager.LocalHotKeyPressed +=new LocalHotKeyEventHandler(MyHotKeyManager_LocalHotKeyPressed);
            MyHotKeyManager.ChordPressed +=new ChordHotKeyEventHandler(MyHotKeyManager_ChordPressed);

        }

        #region **Media Player.

        void timer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(tick);
        }

        void changeStatus()
        {
            if (FileisPlaying)
            { TimeLine.Value = myMediaPlayer.Position.TotalMilliseconds; }
        }

        private void volumeControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myMediaPlayer.Volume = volumeControl.Value;
        }

        private void TimeLine_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsDragging = true;
            FileisPlaying = false;
        }

        private void TimeLine_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)TimeLine.Value);
                changePostion(ts);
                FileisPlaying = true;
            }
            IsDragging = false;
        }

        void changePostion(TimeSpan ts)
        {
            myMediaPlayer.Position = ts;
        }

        private void TimeLine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)TimeLine.Value);

            changePostion(ts);
        }

        #endregion

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            File();
        }
    }
}
