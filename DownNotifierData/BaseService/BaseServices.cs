using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DownNotifierEntities.Context;
using Microsoft.EntityFrameworkCore;


namespace DownNotifierData.BaseService
{
    public class BaseServices : IBaseServices
    {
        private readonly DownNotifierContext _mainContext;

        public BaseServices(DownNotifierContext mainContext)
        {
            _mainContext = mainContext;
        }

        public List<T> GetList<T>() where T : BaseEntity
        {
            return _mainContext.Set<T>().AsNoTracking().ToList();
        }

        public async Task<T> GetAsync<T>(int id) where T : BaseEntity
        {
            return await _mainContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<int> SaveAsync<T>(T model) where T : BaseEntity
        {
            if (model.Id == 0)
            {
                await _mainContext.Set<T>().AddAsync(model);
            }
            else
            {
                _mainContext.Set<T>().Update(model);
            }

            await _mainContext.SaveChangesAsync();

            return model.Id;
        }
        public async Task<int> AddAsync<T>(T model) where T : BaseEntity
        {
            await _mainContext.Set<T>().AddAsync(model);

            await _mainContext.SaveChangesAsync();

            return model.Id;
        }
        public async Task<int> UpdateAsync<T>(T model) where T : BaseEntity
        {
            _mainContext.Set<T>().Update(model);

            await _mainContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task<bool> DeleteAsync<T>(int id) where T : BaseEntity
        {
            T model = await GetAsync<T>(id);

            _mainContext.Set<T>().Remove(model);

            await _mainContext.SaveChangesAsync();

            return true;
        }

    }
}
