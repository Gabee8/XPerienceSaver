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
using System.Windows.Interop;

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

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

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

        public System.Drawing.Rectangle Bounds { get; set; }


        private Point mouseLocation;
        private bool previewMode = false;
        private bool loaded = false;
        private Random rand = new Random();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int time = 5;
        string picture = "pack://application:,,,/XPerienceSaver;Component/Images/xp.png";
        int selLogo = 0;
        IntPtr PHandle;
        int BoundsWidth = 0;
        int BoundsHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
        }


        public MainWindow(System.Drawing.Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;
            BoundsWidth = Bounds.Width;
            BoundsHeight = Bounds.Height;
        }

        public MainWindow(IntPtr PreviewWndHandle)
        {
            InitializeComponent();
            PHandle = PreviewWndHandle;
            this.Bounds = Bounds;
            previewMode = true;

        }

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {

            Canvas.SetLeft(logo, rand.Next(Math.Max(1, BoundsWidth - (int)logo.ActualWidth)));
            Canvas.SetTop(logo, rand.Next(Math.Max(1, BoundsHeight - (int)logo.ActualHeight)));
            // Move text to new location
            
        }

        private void LoadSettings()
        {
            if (previewMode)
            {
                WindowInteropHelper wih = new WindowInteropHelper(this);
                // Set the preview window as the parent of this window
                SetParent(wih.Handle, PHandle);

                // Make this a child window so it will close when the parent dialog closes
                SetWindowLong(wih.Handle, -16, new IntPtr(GetWindowLong(wih.Handle, -16) | 0x40000000));

                // Place our window inside the parent
                System.Drawing.Rectangle ParentRect;
                GetClientRect(PHandle, out ParentRect);

                MoveWindow(wih.Handle, 0, 0, ParentRect.Width, ParentRect.Height, true);

                logo.Width = 32;
                logo.Height = 24;
                BoundsWidth = ParentRect.Size.Width;
                BoundsHeight = ParentRect.Size.Height;
                this.Width = ParentRect.Size.Width;
                this.Height = ParentRect.Size.Height;
              
            }

            Canvas.SetLeft(logo, rand.Next(Math.Max(1, BoundsWidth - (int)logo.ActualWidth)));
            Canvas.SetTop(logo, rand.Next(Math.Max(1, BoundsHeight - (int)logo.ActualHeight)));
            

            Cursor = Cursors.None;
            this.Topmost = true;
            try
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
                        Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/w7.png");
                        Img.EndInit();
                        logo.Source = Img;
                    }
                    if (logoIndex == 2)
                    {
                        BitmapImage Img = new BitmapImage();
                        Img.BeginInit();
                        Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/vista.png");
                        Img.EndInit();
                        logo.Source = Img;
                    }
                    if (logoIndex == 1)
                    {
                        BitmapImage Img = new BitmapImage();
                        Img.BeginInit();
                        Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/mce.png");
                        Img.EndInit();
                        logo.Source = Img;
                    }
                    if (logoIndex == 0)
                    {
                        BitmapImage Img = new BitmapImage();
                        Img.BeginInit();
                        Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/xp.png");
                        Img.EndInit();
                        logo.Source = Img;
                    }
                    time = int.Parse(key.GetValue("time").ToString());

                    
                }
            }
            catch (Exception)
            {
                
            }
           
          
        }

        System.Drawing.Point point;
        public void ExitScr()
        {
            if (loaded)
            {
                if (!previewMode)
                {
                    this.Close();
                }
            }
        }


        private void screensaverFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (loaded)
            {
                if (!previewMode)
                {

                    if (mouseLocation != new Point(-1,-1))
                    {

                        if (Math.Abs(mouseLocation.X - e.GetPosition(this).X) > 5 ||
                            Math.Abs(mouseLocation.Y - e.GetPosition(this).Y) > 5)
                        {

                            this.Close();
                        }


                    }

                    mouseLocation = e.GetPosition(this);
                }
            }
           
        }

        private void screensaverFrm_Loaded(object sender, RoutedEventArgs e)
        {
            mouseLocation = new Point(-1, -1);
            LoadSettings();
            dispatcherTimer.Tick += moveTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, time, 0);
            dispatcherTimer.Start();
        }

        private void screensaverFrm_ContentRendered(object sender, EventArgs e)
        {
            loaded = true;
           
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
