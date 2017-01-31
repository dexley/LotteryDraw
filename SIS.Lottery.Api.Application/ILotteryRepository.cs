using SIS.Lottery.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Lottery.Api.Application
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Find(string t);

        Task Create(T t);

        Task Update(T t);
    }
}
