﻿using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.Property(x => x.createdAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GetDate()");

            builder.HasKey(x=>new {x.MemberId,x.PlanId});

            builder.Ignore(x => x.id);
        }
    }
}
