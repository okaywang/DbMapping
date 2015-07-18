using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DbMapping
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".mdb";
            dlg.Filter = "access file|*.mdb";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                this.txtFileName.Text = filename;
                this.ShowTables(filename);
            }
        }

        private void Tables_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tb = sender as TextBlock;

            MessageBox.Show(tb.Text);
        }

        private void ShowTables(string accessFileName)
        {
            var tables = AccessHelper.GetTables(accessFileName);
            icTables.ItemsSource = tables;
        }


        private static void Test2002()
        {
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=student2002.mdb";
            var connection = new OleDbConnection(connectionString);
            var adapter = new OleDbDataAdapter("select * from person", connection);
            var ds = new DataSet();
            try
            {
                connection.Open();
                string[] restrictions = new string[4];
                restrictions[3] = "Table";
                var userTables = connection.GetSchema("Tables", restrictions);

                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void Test2007()
        {
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=student.accdb";
            var connection = new OleDbConnection(connectionString);
            var adapter = new OleDbDataAdapter("select * from person", connection);
            var ds = new DataSet();
            try
            {
                connection.Open();
                string[] restrictions = new string[4];
                restrictions[3] = "Table";
                var userTables = connection.GetSchema("Tables", restrictions);

                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
