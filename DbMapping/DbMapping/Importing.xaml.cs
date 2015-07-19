using System;
using System.Collections.Generic;
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
using DbMapping.Entities;

namespace DbMapping
{
    /// <summary>
    /// Interaction logic for Importing.xaml
    /// </summary>
    public partial class Importing : Window
    {
        public Importing()
        {
            InitializeComponent();

            var vm = new ImportingViewModel();
            vm.Mappings = AccessHelper.GetMappings();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ImportingViewModel;
            var entity = this.ComboBox1.SelectedValue as MappingEntity;

            var sql = string.Format("select {0} from {1}", entity.SourceFields, entity.SourceTableName);

            var gridView = new GridView();
            var fields = entity.SourceFields.Split(',');
            for (int i = 0; i < fields.Length; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = fields[i],
                    DisplayMemberBinding = new Binding(fields[i])
                });
            }
            
            this.ListView1.View = gridView;

            var data = AccessHelper.GetDataTable(sql,entity.SourceFileName);
            this.ListView1.ItemsSource = data.DefaultView;
        }
    }

    public class ImportingViewModel
    {
        public MappingEntity[] Mappings { get; set; }
    }
}
