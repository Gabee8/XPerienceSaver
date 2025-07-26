using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace XPerienceSaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                string firstArgument = e.Args[0].ToLower().Trim();
                string secondArgument = null;

                // Handle cases where arguments are separated by colon. 
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (e.Args.Length > 1)
                    secondArgument = e.Args[1];

                if (firstArgument == "/c")           // Configuration mode
                {
                    SettingsForm sfrm = new SettingsForm();
                    sfrm.Show();
                }
                else if (firstArgument == "/p")      // Preview mode
                {
                    if (secondArgument == null)
                    {
                        MessageBox.Show("Sorry, but the expected window handle was not provided.",
                            "ScreenSaver", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    IntPtr previewWndHandle = new IntPtr(long.Parse(secondArgument));
                    MainWindow main = new XPerienceSaver.MainWindow();
                    main.Show();
                    //Application.Run(new ScreenSaverForm(previewWndHandle));
                }
                else if (firstArgument == "/s")      // Full-screen mode
                {
                    ShowScreenSaver();
                    
                }
                else    // Undefined argument
                {
                    MessageBox.Show("Sorry, but the command line argument \"" + firstArgument +
                        "\" is not valid.", "ScreenSaver",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else    // No arguments - treat like /c
            {
                SettingsForm sfrm = new SettingsForm();
                sfrm.Show();
            }

        }

        static void ShowScreenSaver()
        {

            MainWindow main = new XPerienceSaver.MainWindow();
            main.Show(); 
        }

       
    }
}
