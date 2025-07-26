using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Threading;

namespace XPerienceSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Win32 API functions

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out System.Drawing.Rectangle lpRect);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        #endregion


        private Point mouseLocation;
        private bool previewMode = false;
        private Random rand = new Random();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int time = 5;
        string picture = "pack://application:,,,/XPerienceSaver;Component/Images/xp.bmp";
        int selLogo = 0;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += moveTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, time, 0);
            
            Canvas.SetLeft(logo, rand.Next(Math.Max(1, (int)this.Width - (int)logo.Width)));
            Canvas.SetTop(logo, rand.Next(Math.Max(1, (int)this.Height - (int)logo.Height)));
            dispatcherTimer.Start();

            Cursor = Cursors.None;
            this.Topmost = true;
        }

        

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {

            Canvas.SetLeft(logo, rand.Next(Math.Max(1, (int)this.Width - (int)logo.Width)));
            Canvas.SetTop(logo, rand.Next(Math.Max(1, (int)this.Height - (int)logo.Height)));
            // Move text to new location
            
        }

        private void LoadSettings()
        {
            // Use the string from the Registry if it exists
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\XP_ScreenSaver");
            if (key != null)
            {
                int logoIndex = int.Parse(key.GetValue("logo").ToString());

                if (logoIndex == 4)
                {
                    picture = (string)key.GetValue("imagePath");
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri(picture);
                    Img.EndInit();
                    logo.Source = Img;
                }
                if (logoIndex == 3)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/w7.bmp");
                    Img.EndInit();
                    logo.Source = Img;
                }
                if (logoIndex == 2)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/vista.bmp");
                    Img.EndInit();
                    logo.Source = Img;
                }
                if (logoIndex == 1)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/mce.bmp");
                    Img.EndInit();
                    logo.Source = Img;
                }
                if (logoIndex == 0)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/xp.bmp");
                    Img.EndInit();
                    logo.Source = Img;
                }
                time = int.Parse(key.GetValue("time").ToString());
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, time, 0);
            }
        }

        public void ExitScr()
        {
            if (this.IsLoaded)
            {
                if (previewMode)
                {
                    if (mouseLocation != null)
                    {
                        if (Math.Abs(mouseLocation.X - GetMousePosition().X) > 5 ||
                            Math.Abs(mouseLocation.Y - GetMousePosition().Y) > 5)
                            this.Close();
                    }
                    mouseLocation = GetMousePosition();
                }
            }
        }

        private void screensaverFrm_MouseMove(object sender, MouseEventArgs e)
        {
            ExitScr();
        }

        private void screensaverFrm_Loaded(object sender, RoutedEventArgs e)
        {

            LoadSettings();
        }

        private void screensaverFrm_ContentRendered(object sender, EventArgs e)
        {
            previewMode = true;
        }


        private void screensaverFrm_KeyUp(object sender, KeyEventArgs e)
        {
            ExitScr();
        }

        private void screensaverFrm_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExitScr();
        }

        private void screensaverFrm_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExitScr();
        }

        
    }
}
