using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierData.BaseService
{
    public interface IBaseServices
    {
        List<T> GetList<T>() where T : BaseEntity;
        Task<T> GetAsync<T>(int id) where T : BaseEntity;
        Task<int> SaveAsync<T>(T model) where T : BaseEntity;
        Task<int> AddAsync<T>(T model) where T : BaseEntity;
        Task<int> UpdateAsync<T>(T model) where T : BaseEntity;
        Task<bool> DeleteAsync<T>(int id) where T : BaseEntity;
    }
}
