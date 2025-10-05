﻿using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository.Interfaces
{
    internal interface IMemberRepository
    {
        // Get All
        IEnumerable<Member> GetAll();
        //Get By Id
        Member? GetById(int id);
        // Add
        int Add(Member member);
        // Update
        int Update(Member member);
        // Delete
        int Delete(int id);
    }
}
