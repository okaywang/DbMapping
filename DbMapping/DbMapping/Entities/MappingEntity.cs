using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMapping.Entities
{
    public class MappingEntity
    {
        public string SourceFileName { get; set; }

        public string SourceTableName { get; set; }

        public string SourceIndendityFieldName { get; set; }

        public string TargetDbName { get; set; }

        public string TargetTableName { get; set; }

        public string Fields { get; set; }
    }
}
