﻿using System;
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
using System.Data;

namespace DbMapping
{
    /// <summary>
    /// Interaction logic for Importing.xaml
    /// </summary>
    public partial class Importing : Window
    {
        private int _maxId = 0;
        public Importing()
        {
            InitializeComponent();

            var vm = new ImportingViewModel();
            vm.Mappings = AccessHelper.GetMappings();
            this.DataContext = vm;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Mapping().ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new MappingList().ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ImportingViewModel;
            var entity = this.ComboBox1.SelectedValue as MappingEntity;

            var record = AccessHelper.GetMapping(entity.ID);
            var sql = string.Format("select top {0} {1} from {2} where {3} > {4}", entity.ImportingMaxCount, entity.SourceFields, entity.SourceTableName, entity.SourceIndendityFieldName, record.ImportedMaxIndendity);

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

            var data = AccessHelper.GetDataTable(sql, entity.SourceFileName);

            if (data.Rows.Count > 0)
            {
                _maxId = Convert.ToInt32(data.Rows[data.Rows.Count - 1][entity.SourceIndendityFieldName]);
            }
            this.ListView1.ItemsSource = data.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var entity = this.ComboBox1.SelectedValue as MappingEntity;
            var sql = string.Format("update Mapping set ImportedMaxIndendity={0} where ID={1}", _maxId, entity.ID);
            AccessHelper.ExecuteSql(sql, "Data/Mapping.accdb");

            MessageBox.Show("导入成功"); 
            this.ListView1.ItemsSource = null;
        }
    }

    public class ImportingViewModel
    {
        public MappingEntity[] Mappings { get; set; }
    }
}
