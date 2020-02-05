using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace YaSheduler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Exit += new ExitEventHandler(App_Exit);
        }
        void App_Exit(object sender, ExitEventArgs e)
        {
            //Debug.Assert(true, "EEEEEEEEnd"); //WriteLine("EEEEEEEEnd");
            //Trace.WriteLine("text");
            //System.Console.WriteLine("text2");
            //Trace.WriteLine("text3");
            // MessageBox.Show("!");
            
        }
        
    }
}
