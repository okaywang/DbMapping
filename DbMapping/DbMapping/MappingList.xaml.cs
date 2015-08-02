using DbMapping.Entities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MappingList.xaml
    /// </summary>
    public partial class MappingList : Window
    {
        public MappingList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = AccessHelper.GetMappings();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var entity = ((ListViewItem)sender).Content as MappingEntity;
            var vm = new MappingViewModel
            {
                MappingName = entity.MappingName,
                SourceFileName = entity.SourceFileName,
                SourceTableName = entity.SourceTableName,
                SourceIndendityFieldName = entity.SourceIndendityFieldName,
                TargetDbName = entity.TargetDbName,
                TargetTableName = entity.TargetTableName,
                ImportingMaxCount = entity.ImportingMaxCount,
            };
            var srcFields = entity.SourceFields.Split(',');
            var tgtFields = entity.TargetFields.Split(',');
            for (int i = 0; i < srcFields.Length; i++)
            {
                vm.MappingEntries.Add(new MappingEntry { SourceField = srcFields[i], TargetField = tgtFields[i] });
            }            
            var view = new Mapping(vm);
            view.ShowDialog();
        }
    }
}
