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
using System.Data;
using Newtonsoft.Json;
using TargetModel;

namespace DbMapping
{
    /// <summary>
    /// Interaction logic for Importing.xaml
    /// </summary>
    public partial class Importing : Window
    {
        private int _maxId = 0;
        private MappingEntity _rule;
        public Importing()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Mapping().ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new MappingList().ShowDialog();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var vm = this.DataContext as ImportingViewModel;
        //    var entity = this.ComboBox1.SelectedValue as MappingEntity;

        //    var record = AccessHelper.GetMapping(entity.ID);
        //    var sql = string.Format("select top {0} {1} from {2} where {3} > {4}", entity.ImportingMaxCount, entity.SourceFields, entity.SourceTableName, entity.SourceIndendityFieldName, record.ImportedMaxIndendity);

        //    var gridView = new GridView();
        //    var fields = entity.SourceFields.Split(',');
        //    for (int i = 0; i < fields.Length; i++)
        //    {
        //        gridView.Columns.Add(new GridViewColumn
        //        {
        //            Header = fields[i],
        //            DisplayMemberBinding = new Binding(fields[i])
        //        });
        //    }

        //    this.ListView1.View = gridView;

        //    var data = AccessHelper.GetDataTable(sql, entity.SourceFileName);

        //    if (data.Rows.Count > 0)
        //    {
        //        _maxId = Convert.ToInt32(data.Rows[data.Rows.Count - 1][entity.SourceIndendityFieldName]);
        //    }
        //    this.ListView1.ItemsSource = data.DefaultView;
        //    this.tbTip.Text = string.Format("已导入标识:{0}", record.ImportedMaxIndendity);
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tag = (this.ComboBox1.SelectedItem as ComboBoxItem).Tag;
            _rule = AccessHelper.GetRule((TargetTableType)Convert.ToInt32(tag));

            var record = AccessHelper.GetRule(_rule.ID);
            var srcFields = _rule.SourceFields.Split(',');
            var tgtFields = _rule.TargetFields.Split(',');
            var selectFields = new StringBuilder();
            var importingFields = new List<string>();
            for (int i = 0; i < srcFields.Length; i++)
            {
                if (!string.IsNullOrEmpty(srcFields[i]))
                {
                    selectFields.AppendFormat("{0} as {1},", srcFields[i], tgtFields[i]);
                    importingFields.Add(tgtFields[i]);
                }
            }

            var sql = string.Format("select top {0} {1} from {2} where {3} > {4}", _rule.ImportingMaxCount, selectFields.ToString().TrimEnd(','), _rule.SourceTableName, _rule.SourceIndendityFieldName, record.ImportedMaxIndendity);

            var data = AccessHelper.GetDataTable(sql, _rule.SourceFileName);

            var models = new List<TargetModel.GongFenModel>();
            foreach (DataRow row in data.Rows)
            {
                var model = new TargetModel.GongFenModel();
                var modelType = typeof(TargetModel.GongFenModel);
                var modelProps = modelType.GetProperties();
                foreach (var field in importingFields)
                {
                    var prop = modelProps.Single(i => i.Name == field);
                    prop.SetValue(model, row[field]);
                }
                models.Add(model);
            }
            DisplayModel(models);
        }

        private void DisplayModel(List<TargetModel.GongFenModel> models)
        {
            var gridView = new GridView();
            var props = typeof(TargetModel.GongFenModel).GetProperties();
            foreach (var prop in props)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = prop.Name,
                    DisplayMemberBinding = new Binding(prop.Name)
                });
            }

            this.ListView1.View = gridView;

            this.ListView1.ItemsSource = models;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var sql = string.Format("update Mapping set ImportedMaxIndendity={0} where ID={1}", _maxId, _rule.ID);
            AccessHelper.ExecuteSql(sql, AppConsts.AppConnectionString);

            MessageBox.Show("导入成功");
            var model = this.ListView1.ItemsSource as List<TargetModel.GongFenModel>;

            this.ListView1.ItemsSource = null;

            var reqModel = new RequestModel<object>();
            reqModel.TableType = _rule.TargetTableType;
            reqModel.Model = model;

            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(reqModel, jSetting);
            MessageBox.Show(json);
        }
    }

}
