using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DbMapping
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }
        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error:" + e.Exception.Message);

            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
        }
    }
}
