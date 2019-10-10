using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Data.Interfaces;

namespace Task.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
