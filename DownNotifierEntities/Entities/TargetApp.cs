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
    /// used for target app table
    /// </summary>
    [Table("TblTargetApp")]
    public class TargetApp : BaseEntity
    {
        /// <summary>
        /// target app name information
        /// </summary>
        public string? TargetAppName { get; set; }
        /// <summary>
        /// target app Url information
        /// </summary>
        public string? TargetAppUrl { get; set; }
        /// <summary>
        /// user information that registered the target app
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// interval information for how many seconds the target app will remain open
        /// </summary>
        public int MonitoringInterval { get; set; }
        /// <summary>
        /// In order to control the open-closed status of the target app, it must be closed for a certain period of time. 
        /// interval information for how many seconds the target app will remain close
        /// </summary>
        public int CloseWaitingInterval { get; set; }
        /// <summary>
        /// statu of target app , it is open when target app has saved
        /// </summary>
        public TargetAppStatus Statu { get; set; }
        /// <summary>
        /// Indicates the last time when the target app went from closed to open or from open to closed
        /// </summary>
        public DateTime LastStatuChanged { get; set; } = DateTime.Now;
        /// <summary>
        /// When the target app goes into a closed state, it checks whether an e-mail has been sent or not. If an e-mail is sent once while it is closed, it will not be sent again
        /// </summary>
        public bool IsMailSent { get; set; }
    }


    
}
