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
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Mapping.accdb";

            var sql = "select * from Mapping";
            var entities = new List<MappingEntity>();
            using (var cnn = new OleDbConnection(connectionString))
            {
                cnn.Open();
                using (var cmm = new OleDbCommand(sql, cnn))
                {
                    var reader = cmm.ExecuteReader();
                    while (reader.Read())
                    {
                        var entity = new MappingEntity();
                        entity.SourceTableName = reader["SourceTableName"].ToString();
                        entity.TargetTableName = reader["TargetTableName"].ToString();
                        entities.Add(entity);
                    }
                }
            }

            this.DataContext = entities;
        }
    }
}
