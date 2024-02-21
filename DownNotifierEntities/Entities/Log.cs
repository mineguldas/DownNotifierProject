using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.Entities
{
    /// <summary>
    /// table where logs are recorded
    /// </summary>
    [Table("TblLog")]
    public class Log : BaseEntity
    {
        /// <summary>
        /// error message information
        /// </summary>
        public string? ErrorText { get; set; }
        /// <summary>
        /// Error method information
        /// </summary>
        public string? Fonksiyon { get; set; }
        /// <summary>
        /// Information about the model being processed
        /// </summary>
        public string? RequestModel { get; set; }
        /// <summary>
        /// Information about the repository and method being processed
        /// </summary>
        public string? Url { get; set; }
    }

}
