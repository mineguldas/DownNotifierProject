using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierEntities.DataTransferObjects
{
    public  class ResultDto
    {
        /// <summary>
        /// sonucun basarılı mı basarısız mı oldugunu belirtir
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// sonucun mesaj bilgisini verir
        /// </summary>
        public string? Message { get; set; } 
    }
}
