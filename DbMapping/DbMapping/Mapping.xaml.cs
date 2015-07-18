using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            vm.MappingEntries = new ObservableCollection<MappingEntry>();
            vm.MappingEntries.Add(new MappingEntry { SourceField = "a1", TargetField = "b1" });
            vm.MappingEntries.Add(new MappingEntry { SourceField = "a2", TargetField = "b2" });
            this.DataContext = vm;
        }
    }

    public class MappingViewModel : DependencyObject
    {
        public static readonly DependencyProperty SourceFileNameProperty = DependencyProperty.Register("SourceFileName", typeof(string), typeof(Mapping));
        public static readonly DependencyProperty SourceTableNameProperty = DependencyProperty.Register("SourceTableName", typeof(string), typeof(Mapping));
        public static readonly DependencyProperty SourceIndendityFieldNameProperty = DependencyProperty.Register("SourceIndendityFieldName", typeof(string), typeof(Mapping));
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
            this.MappingEntries.RemoveAt(0);
        }
          
        private void Save(object parameter)
        {

        }
    }


    public class MappingEntry : DependencyObject
    {
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
