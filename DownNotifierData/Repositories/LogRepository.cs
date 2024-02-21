using DownNotifierEntities.Context;
using DownNotifierEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifierData.Repositories
{
    /// <summary>
    /// LogRepository
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// Used to list logs
        /// </summary>
        /// <returns></returns>
        List<Log> LogList();
    }
    public class LogRepository : ILogRepository
    {
        private readonly DownNotifierContext _dbContext;
        public LogRepository(DownNotifierContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Log> LogList()
        {
            try
            {
                return _dbContext.Log.ToList();
            }
            catch (Exception ex)
            {
                return new List<Log>();
            }
        }

    }
}
