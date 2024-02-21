using Azure.Core;
using DownNotifierEntities;
using DownNotifierEntities.Context;
using DownNotifierEntities.DataTransferObjects;
using DownNotifierEntities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DownNotifierEntities.Enums.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DownNotifierData.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Used to list users
        /// </summary>
        /// <returns></returns>
        List<User> UserList();
        /// <summary>
        /// It is used to call user with id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUser(int userId);
        /// <summary>
        /// This method is used for user create operation.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EnumResult AddUser(UserSignDto model);
        /// <summary>
        /// This method is used for user sign in operation.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        User GetUserSignIn(UserSignDto model);
    }
    public class UserRepository : IUserRepository
    {
        private readonly DownNotifierContext _dbContext;
        private readonly ILogRepository _logRepository;
        public UserRepository(DownNotifierContext dbContext, ILogRepository logRepository)
        {
            _dbContext = dbContext;
            _logRepository = logRepository;
        }

        public List<User> UserList()
        {
            try
            {
                return _dbContext.User.ToList();
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException);
                return new List<User>();
            }
        }

        public User GetUser(int userId)
        {
            try
            {
                return _dbContext.User.FirstOrDefault(x => x.Id == userId);
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, userId.ToString());
                return null;
            }
        }

        public EnumResult AddUser(UserSignDto model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Email))
                {
                    var userKontrol = _dbContext.User.FirstOrDefault(x => x.Email == model.Email);
                    if (userKontrol == null)
                    {
                        User md = new User { Email = model.Email, Password = model.Password, NameSurname = model.NameSurname };
                        _dbContext.User.Add(md);
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
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, JObject.FromObject(model).ToString());
                return EnumResult.Basarisiz;
            }
        }

        public User GetUserSignIn(UserSignDto model)
        {
            try
            {
                var user = _dbContext.User.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string classname = new StackTrace().GetFrame(0).GetMethod().DeclaringType.Name;
                string fonk = new StackTrace().GetFrame(0).GetMethod().Name;
                Logs.LogTut(classname, fonk, ex.Message + "-" + ex.InnerException, JObject.FromObject(model).ToString());
                return null;
            }
        }
    }
}
