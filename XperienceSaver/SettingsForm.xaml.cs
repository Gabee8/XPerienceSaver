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
using Microsoft.Win32;

namespace XPerienceSaver
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        int selLogo = 0;

        private void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\XP_ScreenSaver");
            if (key != null)
            {
                CPicPath.Text = (string)key.GetValue("imagePath");
                string time = (string)key.GetValue("time");
                timeTxt.Text = time.ToString();
                selLogo = int.Parse(key.GetValue("logo").ToString());

                if (selLogo == 4)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri(CPicPath.Text);
                    Img.EndInit();
                    image1.Source = Img;
                    customCb.IsChecked = true;
                }
                if (selLogo == 3)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/w7.bmp");
                    Img.EndInit();
                    image1.Source = Img;
                    w7Cb.IsChecked = true;
                }
                if (selLogo == 2)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/vista.bmp");
                    Img.EndInit();
                    image1.Source = Img;
                    vistaCb.IsChecked = true;
                }
                if (selLogo == 1)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/mce.bmp");
                    Img.EndInit();
                    image1.Source = Img;
                    mceCb.IsChecked = true;
                }
                if (selLogo == 0)
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/xp.bmp");
                    Img.EndInit();
                    image1.Source = Img;
                    defaultCb.IsChecked = true;
                }
            }
        }

        private void defaultCb_Checked(object sender, RoutedEventArgs e)
        {
            selLogo = 0;
            if (image1 != null)
            {
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/xp.bmp");
                Img.EndInit();
                image1.Source = Img;
            }
            
        }

        private void mceCb_Checked(object sender, RoutedEventArgs e)
        {
            selLogo = 1;
            if (image1 != null)
            {
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/mce.bmp");
                Img.EndInit();
                image1.Source = Img;
            }
        }

        private void vistaCb_Checked(object sender, RoutedEventArgs e)
        {
            selLogo = 2;
            if (image1 != null)
            {
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/vista.bmp");
                Img.EndInit();
                image1.Source = Img;
            }
        }

        private void w7Cb_Checked(object sender, RoutedEventArgs e)
        {
            selLogo = 3;
            if (image1 != null)
            {
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.UriSource = new Uri("pack://application:,,,/XPerienceSaver;Component/Images/w7.bmp");
                Img.EndInit();
                image1.Source = Img;
            }
        }

        private void customCb_Checked(object sender, RoutedEventArgs e)
        {
            selLogo = 4;
            if (image1 != null)
            {
                BitmapImage Img = new BitmapImage();
                Img.BeginInit();
                Img.UriSource = new Uri(CPicPath.Text);
                Img.EndInit();
                image1.Source = Img;
            }
        }

        private void SaveSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\XP_ScreenSaver");
            if (CPicPath.Text != "")
            {
                key.SetValue("imagePath", CPicPath.Text);
            }
            key.SetValue("time", double.Parse(timeTxt.Text), RegistryValueKind.String);
            key.SetValue("logo", (double)selLogo,RegistryValueKind.String);

        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void applyBt_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void cancelBt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rstTime_Click(object sender, RoutedEventArgs e)
        {
            timeTxt.Text = "5";
        }

        private void browseBt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ofd.Filter = "Images (*.png;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    BitmapImage Img = new BitmapImage();
                    Img.BeginInit();
                    Img.UriSource = new Uri(ofd.FileName);
                    Img.EndInit();
                    image1.Source = Img;
                    CPicPath.Text = ofd.FileName;
                }
                catch (Exception)
                {
                    
                }
                

            }
        }

       
    }
}
