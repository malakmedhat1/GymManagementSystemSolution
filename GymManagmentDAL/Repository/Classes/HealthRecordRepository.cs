using GymManagmentDAL.Data.Contexts;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Classes
{
    internal class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymDBContext _dbContext;

        public HealthRecordRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(HealthRecord HealthRecord)
        {
            _dbContext.HealthRecords.Add(HealthRecord);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var healthRecord = _dbContext.HealthRecords.Find(id);
            if (healthRecord != null)
            {
                _dbContext.HealthRecords.Remove(healthRecord);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

        public IEnumerable<HealthRecord> GetAll()
        {
              return _dbContext.HealthRecords.ToList();
        }

        public HealthRecord? GetById(int id) => _dbContext.HealthRecords.Find(id);

        public int Update(HealthRecord HealthRecord)
        {
            _dbContext.HealthRecords.Update(HealthRecord);
            return _dbContext.SaveChanges();
        }
    }
}
