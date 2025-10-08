using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Interfaces
{
    internal interface IPlanRepository
    {
        // Get All
        IEnumerable<Plan> GetAll();
        
        //Get By Id
        Plan? GetById(int id);
        
        // Add
        int Add(Plan plan);
        
        // Update
        int Update(Plan plan);
        
        // Delete
        int Delete(int id);
    }
}
