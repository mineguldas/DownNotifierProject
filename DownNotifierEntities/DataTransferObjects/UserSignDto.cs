using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.DataTransferObjects
{
    /// <summary>
    /// Information about the fields to be sent when user login
    /// </summary>
    public class UserSignDto
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
