using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHK3.Domain.Entities
{
    internal class ReportLogs : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public  string? ReportType { get; set; }

        public int GeneratedBy { get; set; }

        [ForeignKey(nameof(GeneratedBy))]
        public User? GeneratedByAdmin { get; set; }


    }
}
