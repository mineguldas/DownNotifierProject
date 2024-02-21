using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.Enums
{
    public class Enums
    {
        /// <summary>
        /// used to show results on trading screens
        /// </summary>
        public enum EnumResult
        {
            Basarisiz = 0,
            Basarili = 1,
            IcerideAyniIslemKayitli = 2
        }

        /// <summary>
        /// Shows the open-closed status of the target app
        /// </summary>
        public enum TargetAppStatus
        {
            Close = 0,
            Open = 1
        }

        /// <summary>
        /// It is used to match the parameters used in the system with the database.
        /// </summary>
        public enum ParameterType
        {
            /// <summary>
            /// Automatic on-off control of the target app
            /// </summary>
            SorgulamaOtomatikYapilsinMi = 0,
            /// <summary>
            /// If the on-off control of the application is done automatically, information on how many seconds it will be checked
            /// </summary>
            SorgulamaKacSaniyedeBirYapilsin = 1
        }
    }
}
