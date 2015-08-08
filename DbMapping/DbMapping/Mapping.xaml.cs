using Microsoft.Win32;
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

            this.cbRuleTypes.ItemsSource = AppHelper.GetRules();
            this.cbRuleTypes.DisplayMemberPath = "Name";
            this.cbRuleTypes.SelectedValuePath = "Value";

            this.cbRuleTypes.SelectedValue = TargetModel.TargetTableType.工分表;

            //var vm = new MappingViewModel
            //{
            //    SourceIndendityFieldName = "ID",
            //    ImportingMaxCount = 20,
            //    MappingEntries = new ObservableCollection<MappingEntry>()
            //};
            //var props = typeof(TargetModel.GongFenModel).GetProperties();
            //foreach (var prop in props)
            //{
            //    vm.MappingEntries.Add(new MappingEntry { TargetField = prop.Name });
            //}
            //this.DataContext = vm;
        }

        //public Mapping(MappingViewModel model)
        //{
        //    InitializeComponent();
        //    this.DataContext = model;
        //}

        private void cbRuleTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = new MappingViewModel();
            vm.TargetTableType = (TargetModel.TargetTableType)((sender as ComboBox).SelectedValue);
            var entity = AccessHelper.GetRule(vm.TargetTableType);
            if (entity != null)
            {
                vm.ID = entity.ID;
                //vm.MappingName = entity.MappingName;
                vm.SourceFileName = entity.SourceFileName;
                vm.SourceTableName = entity.SourceTableName;
                vm.SourceIndendityFieldName = entity.SourceIndendityFieldName;
                vm.ImportingMaxCount = entity.ImportingMaxCount;

                var srcFields = entity.SourceFields.Split(',');
                var tgtFields = entity.TargetFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tgtFields.Length; i++)
                {
                    vm.MappingEntries.Add(new MappingEntry { SourceField = srcFields[i], TargetField = tgtFields[i] });
                }
            }

            this.DataContext = vm;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var vm = this.DataContext as MappingViewModel;
        //    vm.MappingEntries.Add(new MappingEntry(this.SourceField.Text, this.TargetField.Text));

        //    this.SourceField.Clear();
        //    this.TargetField.Clear();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var vm = this.DataContext as MappingViewModel;
                vm.SourceFileName = dialog.FileName;
            }
        }

    }

    public class MappingViewModel : DependencyObject
    {
        //public static readonly DependencyProperty MappingNameProperty = DependencyProperty.Register("MappingName", typeof(string), typeof(MappingViewModel));
        public static readonly DependencyProperty ImportingMaxCountProperty = DependencyProperty.Register("ImportingMaxCount", typeof(int), typeof(MappingViewModel));
        public static readonly DependencyProperty SourceFileNameProperty = DependencyProperty.Register("SourceFileName", typeof(string), typeof(MappingViewModel));
        public static readonly DependencyProperty SourceTableNameProperty = DependencyProperty.Register("SourceTableName", typeof(string), typeof(MappingViewModel));
        public static readonly DependencyProperty SourceIndendityFieldNameProperty = DependencyProperty.Register("SourceIndendityFieldName", typeof(string), typeof(MappingViewModel));

        //public static readonly DependencyProperty TargetDbNameProperty = DependencyProperty.Register("TargetDbName", typeof(string), typeof(MappingViewModel));
        //public static readonly DependencyProperty TargetTableNameProperty = DependencyProperty.Register("TargetTableName", typeof(string), typeof(MappingViewModel));
        public static readonly DependencyProperty TargetTableTypeProperty = DependencyProperty.Register("TargetTableType", typeof(TargetModel.TargetTableType), typeof(MappingViewModel));

        //public static readonly DependencyProperty MappingEntriesProperty = DependencyProperty.Register("MappingEntries", typeof(string), typeof(Mapping));
        public MappingViewModel()
        {
            MappingEntries = new ObservableCollection<MappingEntry>();
        }

        public int ID { get; set; }

        //public string MappingName
        //{
        //    get { return this.GetValue(MappingNameProperty) as string; }
        //    set { this.SetValue(MappingNameProperty, value); }
        //}

        public int ImportingMaxCount
        {
            get { return (int)this.GetValue(ImportingMaxCountProperty); }
            set { this.SetValue(ImportingMaxCountProperty, value); }
        }

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

        public TargetModel.TargetTableType TargetTableType
        {
            get { return (TargetModel.TargetTableType)this.GetValue(TargetTableTypeProperty); }
            set { this.SetValue(TargetTableTypeProperty, value); }
        }

        //public string TargetDbName
        //{
        //    get { return this.GetValue(TargetDbNameProperty) as string; }
        //    set { this.SetValue(TargetDbNameProperty, value); }
        //}

        //public string TargetTableName
        //{
        //    get { return this.GetValue(TargetTableNameProperty) as string; }
        //    set { this.SetValue(TargetTableNameProperty, value); }
        //}

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
            var sourceFields = this.MappingEntries.Aggregate<MappingEntry, string>(string.Empty, (x, y) => string.IsNullOrEmpty(x) ? y.SourceField : x + "," + y.SourceField);
            var targetFields = this.MappingEntries.Aggregate<MappingEntry, string>(string.Empty, (x, y) => string.IsNullOrEmpty(x) ? y.TargetField : x + "," + y.TargetField);

            string sql = string.Empty;
            if (this.ID == 0)
            {
                sql = string.Format(@"insert into Mapping(ImportingMaxCount,SourceFileName,SourceTableName,SourceIndendityFieldName,TargetTableType,SourceFields,TargetFields)
                            values('{0}',{1},'{2}','{3}','{4}','{5}','{6}')",
                       this.ImportingMaxCount, this.SourceFileName, this.SourceTableName,
                       this.SourceIndendityFieldName, (int)this.TargetTableType, sourceFields, targetFields);
            }
            else
            {
                sql = string.Format(@"update Mapping 
                                        set ImportingMaxCount={0},
                                            SourceFileName='{1}',
                                            SourceTableName='{2}',
                                            SourceIndendityFieldName='{3}',
                                            TargetTableType='{4}',
                                            SourceFields='{5}',
                                            TargetFields='{6}'
                                    where ID={7}",
                       this.ImportingMaxCount, this.SourceFileName, this.SourceTableName, this.SourceIndendityFieldName, (int)this.TargetTableType, sourceFields, targetFields, this.ID);
            }
            using (var cnn = new OleDbConnection(AppConsts.AppConnectionString))
            {
                using (var cmd = new OleDbCommand(sql, cnn))
                {
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("保存成功！");
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
