using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Interfaces
{
    internal interface IHealthRecordRepository
    {
        // Get All
        IEnumerable<HealthRecord> GetAll();

        //Get By Id
        HealthRecord? GetById(int id);
        
        // Add
        int Add(HealthRecord HealthRecord);
        
        // Update
        int Update(HealthRecord HealthRecord);
        
        // Delete
        int Delete(int id);
    }
}
