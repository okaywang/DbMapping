using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetModel;

namespace DbMapping
{
    public class AppHelper
    {
        public static ObservableCollection<NameValuePair> GetRules()
        {
            var rules = new ObservableCollection<NameValuePair>();
            rules.Add(new NameValuePair { Value = (int)TargetModel.TargetTableType.工分表, Name = TargetTableType.工分表.ToString() });
            rules.Add(new NameValuePair { Value = (int)TargetModel.TargetTableType.量热表, Name = TargetTableType.量热表.ToString() });
            rules.Add(new NameValuePair { Value = (int)TargetModel.TargetTableType.硫分表, Name = TargetTableType.硫分表.ToString() });
            return rules;
        }
    }
}
