using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.Entities
{
    /// <summary>
    /// It is a table that stores information about users who can log in to the system.
    /// </summary>
    [Table("TblUser")]
    public class User : BaseEntity
    {
        /// <summary>
        /// user name surname information
        /// </summary>
        public string? NameSurname { get; set; }
        /// <summary>
        /// user email information
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// user password information
        /// </summary>
        public string? Password { get; set; }
    }
}
