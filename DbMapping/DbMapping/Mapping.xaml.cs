using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DbMapping
{
    /// <summary>
    /// Interaction logic for Mapping.xaml
    /// </summary>
    public partial class Mapping : Window
    {
        public Mapping()
        {
            InitializeComponent();

            var vm = new MappingViewModel();
            vm.SourceFileName = "c:\\aaaa.txt";
            vm.SourceTableName = "abc";
            vm.SourceIndendityFieldName = "the Id";
            vm.TargetDbName = "tar db";
            vm.TargetTableName = "tar table";
            vm.MappingEntries = new ObservableCollection<MappingEntry>();
            vm.MappingEntries.Add(new MappingEntry { SourceField = "a1", TargetField = "b1" });
            vm.MappingEntries.Add(new MappingEntry { SourceField = "a2", TargetField = "b2" });
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MappingViewModel;
            vm.MappingEntries.Add(new MappingEntry(this.SourceField.Text, this.TargetField.Text));

            this.SourceField.Clear();
            this.TargetField.Clear();
        }
    }

    public class MappingViewModel : DependencyObject
    {
        public static readonly DependencyProperty SourceFileNameProperty = DependencyProperty.Register("SourceFileName", typeof(string), typeof(Mapping));
        public static readonly DependencyProperty SourceTableNameProperty = DependencyProperty.Register("SourceTableName", typeof(string), typeof(Mapping));
        public static readonly DependencyProperty SourceIndendityFieldNameProperty = DependencyProperty.Register("SourceIndendityFieldName", typeof(string), typeof(Mapping));

        public static readonly DependencyProperty TargetDbNameProperty = DependencyProperty.Register("TargetDbName", typeof(string), typeof(Mapping));
        public static readonly DependencyProperty TargetTableNameProperty = DependencyProperty.Register("TargetTableName", typeof(string), typeof(Mapping));

        //public static readonly DependencyProperty MappingEntriesProperty = DependencyProperty.Register("MappingEntries", typeof(string), typeof(Mapping));
        public string SourceFileName
        {
            get { return this.GetValue(SourceFileNameProperty) as string; }
            set { this.SetValue(SourceFileNameProperty, value); }
        }

        public string SourceTableName
        {
            get { return this.GetValue(SourceTableNameProperty) as string; }
            set { this.SetValue(SourceTableNameProperty, value); }
        }

        public string SourceIndendityFieldName
        {
            get { return this.GetValue(SourceIndendityFieldNameProperty) as string; }
            set { this.SetValue(SourceIndendityFieldNameProperty, value); }
        }
        public string TargetDbName
        {
            get { return this.GetValue(TargetDbNameProperty) as string; }
            set { this.SetValue(TargetDbNameProperty, value); }
        }

        public string TargetTableName
        {
            get { return this.GetValue(TargetTableNameProperty) as string; }
            set { this.SetValue(TargetTableNameProperty, value); }
        }

        //public ObservableCollection<MappingEntry> MappingEntries3
        //{
        //    get { return this.GetValue(MappingEntriesProperty) as ObservableCollection<MappingEntry>; }
        //    set { this.SetValue(MappingEntriesProperty, value); }
        //}

        public ObservableCollection<MappingEntry> MappingEntries { get; set; }

        public ICommand SaveCommand
        {
            get { return new DelegateCommand(Save); }
        }

        public ICommand RemoveCommand
        {
            get { return new DelegateCommand(Remove); }
        }

        private void Remove(object parameter)
        {
            var item = this.MappingEntries.Single(i => i.TargetField == parameter.ToString());
            this.MappingEntries.Remove(item);
        }

        private void Save(object parameter)
        {
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Mapping.accdb";

            var fields = this.MappingEntries.Aggregate<MappingEntry, string>(string.Empty, (x, y) => string.Format("{0}>{1}", string.IsNullOrEmpty(x) ? y.SourceField : x + "," + y.SourceField, y.TargetField));
            var sql = string.Format(@"insert into Mapping(SourceFileName,SourceTableName,SourceIndendityFieldName,TargetDbName,TargetTableName,Fields)
                            values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        this.SourceFileName, this.SourceTableName, this.SourceIndendityFieldName, this.TargetDbName, this.TargetTableName, fields);
            using (var cnn = new OleDbConnection(connectionString))
            {
                using (var cmd = new OleDbCommand(sql, cnn))
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }


    public class MappingEntry : DependencyObject
    {
        public MappingEntry()
        {

        }
        public MappingEntry(string source, string target)
        {
            this.SourceField = source;
            this.TargetField = target;
        }
        public string SourceField { get; set; }
        public string TargetField { get; set; }

    }

    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;

        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
