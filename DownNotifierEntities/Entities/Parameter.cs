using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifierEntities.Entities
{
    /// <summary>
    /// It is a table where system-related parameters are kept.
    /// </summary>
    [Table("TblParameter")]
    public class Parameter : BaseEntity
    {
        /// <summary>
        /// parameter code information
        /// </summary>
        public string? ParCode { get; set; }
        /// <summary>
        /// parameter value information
        /// </summary>
        public string? ParValue { get; set; }
        /// <summary>
        /// It is the value information that matches the enum
        /// </summary>
        public ParameterType ParEnum { get; set; }
    }
}