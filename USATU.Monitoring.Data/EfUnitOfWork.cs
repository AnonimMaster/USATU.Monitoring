using System.Threading.Tasks;
using USATU.Monitoring.Core;

namespace USATU.Monitoring.Data
{
    public class EfUnitOfWork: IUnitOfWork
    {
        private readonly DataContext _context;

        public EfUnitOfWork(DataContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}