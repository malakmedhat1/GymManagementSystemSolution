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
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDBContext _dbContext;
        
        public MemberRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var member = _dbContext.Members.Find(id);
            if (member != null)
            {
                _dbContext.Members.Remove(member);
                return _dbContext.SaveChanges();
            }
            else
                return 0;
        }

        public IEnumerable<Member> GetAll()
        {
            return _dbContext.Members.ToList();
        }



        public Member? GetById(int id) => _dbContext.Members.Find(id);

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
