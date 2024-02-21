using DownNotifierEntities.Context;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DownNotifierEntities.Enums.Enums;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using DownNotifierEntities.MailOperations;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;

namespace DownNotifierData.Repositories
{
    public interface ITargetAppRepository
    {
        /// <summary>
        /// It is used to call target app with id
        /// </summary>
        /// <param name="targetAppId"></param>
        /// <returns></returns>
        TargetApp GetTargetApp(int targetAppId);
        /// <summary>
        /// It is used to check the status of the target app
        /// </summary>
        /// <param name="targetAppUrl"></param>
        /// <returns></returns>
        TargetApp CheckTargetApp(string targetAppUrl);
        /// <summary>
        /// Used to list target apps
        /// </summary>
        /// <returns></returns>
        List<TargetApp> TargetAppList();
        /// <summary>
        /// It is used to delete target app with id
        /// </summary>
        /// <param name="targetAppId"></param>
        /// <returns></returns>
        bool DeleteTargetApp(int targetAppId);
        /// <summary>
        /// It is used to create or update target app with id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        EnumResult AddTargetApp(TargetAppDto model, int userId);
        /// <summary>
        /// When fetching the app list, it fetches all the apps and their status
        /// </summary>
        /// <param name="targetAppId"></param>
        /// <returns></returns>
        List<TargetAppDto> CheckTargetAppStatu(int targetAppId);
    }
    public class TargetAppRepository : ITargetAppRepository
    {
        private readonly DownNotifierContext _dbContext;
        private IMapper _mapper;
        private readonly ILogRepository _logRepository;
        public TargetAppRepository(DownNotifierContext dbContext, IMapper mapper, ILogRepository logRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logRepository = logRepository;
        }

        public TargetApp GetTargetApp(int targetAppId)
        {
            try
            {
                var data = _dbContext.TargetApp.FirstOrDefault(x => x.Id == targetAppId);
                return data;
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                 Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, targetAppId.ToString());
                return null;
            }
        }

        public List<TargetApp> TargetAppList()
        {
            try
            {
                return _dbContext.TargetApp.ToList();
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException);
                return new List<TargetApp>();
            }
        }

        public bool DeleteTargetApp(int targetAppId)
        {
            try
            {
                var data = _dbContext.TargetApp.FirstOrDefault(x => x.Id == targetAppId);
                _dbContext.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, targetAppId.ToString());
                return false;
            }
        }

        public EnumResult AddTargetApp(TargetAppDto model, int userId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<TargetAppDto, TargetApp>(); });
                _mapper = config.CreateMapper();

                if (model.Id > 0)
                {
                    TargetApp md = _dbContext.TargetApp.FirstOrDefault(x => x.Id == model.Id);
                    if (md != null)
                    {
                        md.TargetAppName = model.TargetAppName;
                        md.TargetAppUrl = model.TargetAppUrl;
                        md.CloseWaitingInterval = model.CloseWaitingInterval;
                        md.MonitoringInterval = model.MonitoringInterval;
                        md.Statu = TargetAppStatus.Open;
                        md.UserId = userId;
                        md.UpdatedBy = userId;
                        md.UpdatedOn = DateTime.Now;
                        _dbContext.TargetApp.Update(md);
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
                    if (!string.IsNullOrEmpty(model.TargetAppUrl) && !string.IsNullOrEmpty(model.TargetAppName))
                    {
                        var kontrol = _dbContext.TargetApp.FirstOrDefault(x => x.TargetAppUrl == model.TargetAppUrl && x.TargetAppName == model.TargetAppName);
                        if (kontrol == null)
                        {
                            TargetApp md = _mapper.Map<TargetApp>(model);
                            md.Statu = TargetAppStatus.Open;
                            md.UserId = userId;
                            md.CreatedBy = userId;
                            md.CreatedOn = DateTime.Now;
                            _dbContext.TargetApp.Add(md);
                            _dbContext.SaveChanges();
                            return EnumResult.Basarili;
                        }
                        else
                        {
                            return EnumResult.IcerideAyniIslemKayitli;
                        }
                    }
                    else
                    {
                        return EnumResult.Basarisiz;
                    }
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


        public List<TargetAppDto> CheckTargetAppStatu(int targetAppId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<TargetApp, TargetAppDto>(); });
                _mapper = config.CreateMapper();

                DateTime date = DateTime.Now;
                List<TargetApp> targetList = targetAppId > 0 ? _dbContext.TargetApp.Where(x => x.Id == targetAppId).ToList() : _dbContext.TargetApp.ToList();
                List<TargetAppDto> dtoList = new List<TargetAppDto>();
                foreach (TargetApp item in targetList)
                {
                    TargetAppDto dto = new TargetAppDto();
                    if (item.Statu == TargetAppStatus.Close)
                    {
                        if (item.IsMailSent)
                        {
                            // şu an kapalı ve kapalı olmasından bu yana kapalı kalma süresini aşmışsa acık durumuna getirilmeli
                            if (item.LastStatuChanged.AddSeconds(item.CloseWaitingInterval) < date)
                            {
                                item.Statu = TargetAppStatus.Open;
                                item.LastStatuChanged = date;
                                item.IsMailSent = false;
                                _dbContext.Update(item);
                                _dbContext.SaveChanges();

                                dto = _mapper.Map<TargetAppDto>(item);
                                dto.Message = "Target App has opened.";
                            }
                            else
                            {
                                dto = _mapper.Map<TargetAppDto>(item);
                                dto.Message = "Target App has closed.";
                                dto.CloseTimeInterval = date.Subtract(item.LastStatuChanged).TotalSeconds;
                            }
                        }
                        else
                        {
                            // daha önceden kapalı durumuna cekildi ancak mail gönderilemedi ise mail gönderimi tekrar denenecek
                            User user = _dbContext.User.FirstOrDefault(x => x.Id == item.UserId);
                            if (user != null && !string.IsNullOrEmpty(user.Email))
                            {
                                bool result = MailSendOperations.SendMail(user.Email, item.TargetAppUrl);
                                item.IsMailSent = result;
                            }
                            _dbContext.Update(item);
                            _dbContext.SaveChanges();

                            dto = _mapper.Map<TargetAppDto>(item);
                            dto.Message = "Target App has closed. Email sending could not be done.";
                            dto.CloseTimeInterval = date.Subtract(item.LastStatuChanged).TotalSeconds;
                        }
                    }
                    else
                    {
                        // şu an aktif ve aktif olmasından bu yana açık kalma süresini aşmışsa kapalı durumuna getirilmeli
                        // kullanıcısına mail gönderilmeli
                        if (item.LastStatuChanged.AddSeconds(item.MonitoringInterval) < date)
                        {
                            bool result = false;
                            item.Statu = TargetAppStatus.Close;
                            item.LastStatuChanged = date;
                            User user = _dbContext.User.FirstOrDefault(x => x.Id == item.UserId);
                            if (user != null && !string.IsNullOrEmpty(user.Email))
                            {
                                result = MailSendOperations.SendMail(user.Email, item.TargetAppUrl);
                                item.IsMailSent = result;
                            }
                            _dbContext.Update(item);
                            _dbContext.SaveChanges();

                            dto = _mapper.Map<TargetAppDto>(item);
                            dto.Message = "Target App has closed. " + (result ? "Email has been sent." : "Email sending could not be done.");
                        }
                        else
                        {
                            dto = _mapper.Map<TargetAppDto>(item);
                            dto.Message = "Target App has opened.";
                            dto.OpenTimeInterval = date.Subtract(item.LastStatuChanged).TotalSeconds;
                        }
                    }
                    dtoList.Add(dto);
                }

                return dtoList;
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                 Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, targetAppId.ToString());
                return new List<TargetAppDto>();
            }
        }

        public TargetApp CheckTargetApp(string targetAppUrl)
        {
            try
            {
                return _dbContext.TargetApp.FirstOrDefault(x => x.TargetAppUrl == targetAppUrl);
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, targetAppUrl);
                return null;
            }
        }


    }
}
