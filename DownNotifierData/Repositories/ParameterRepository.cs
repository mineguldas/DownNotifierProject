using AutoMapper;
using DownNotifierEntities.Context;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DownNotifierEntities.Enums.Enums;

namespace DownNotifierData.Repositories
{
    public interface IParameterRepository
    {
        /// <summary>
        /// It is used to call parameter with id
        /// </summary>
        /// <param name="ParameterId"></param>
        /// <returns></returns>
        Parameter GetParameter(int ParameterId);
        /// <summary>
        /// Used to list parameters
        /// </summary>
        /// <returns></returns>
        List<Parameter> ParameterList();
        /// <summary>
        /// New parameter registration occurs from the database in order to match it with the enum. This method is used for parameter update operation.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        EnumResult UpdateParameter(Parameter model, int userId);
    }
    public class ParameterRepository : IParameterRepository
    {
        private readonly DownNotifierContext _dbContext;
        private readonly ILogRepository _logRepository; 
        public ParameterRepository(DownNotifierContext dbContext, ILogRepository logRepository)
        {
            _dbContext = dbContext;
            _logRepository = logRepository;     
        }

        public Parameter GetParameter(int ParameterId)
        {
            try
            {
                var data = _dbContext.Parameter.FirstOrDefault(x => x.Id == ParameterId);
                return data;
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, ParameterId.ToString());
                return null;
            }
        }

        public List<Parameter> ParameterList()
        {
            try
            {
                return _dbContext.Parameter.ToList();
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException);
                return new List<Parameter>();
            }
        }


        public EnumResult UpdateParameter(Parameter model, int userId)
        {
            try
            {
                if (model.Id > 0)
                {
                    Parameter md = _dbContext.Parameter.FirstOrDefault(x => x.Id == model.Id);
                    if (md != null)
                    {
                        md.ParValue = model.ParValue;
                        md.UpdatedBy = userId;
                        md.UpdatedOn = DateTime.Now;
                        _dbContext.Parameter.Update(md);
                        _dbContext.SaveChanges();
                        return EnumResult.Basarili;
                    }
                    else
                    {
                        return EnumResult.Basarisiz;
                    }
                }
                else
                {
                    return EnumResult.Basarisiz;
                }
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, JObject.FromObject(model).ToString());
                return EnumResult.Basarisiz;    
            }
        }
    }
}
