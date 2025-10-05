using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Member")
                .HasKey(x=>x.id); // Not Needed but for clarity [By Convention]

            builder.HasOne<Member>()
                .WithOne(m => m.HealthRecord)
                .HasForeignKey<HealthRecord>(hr => hr.id);

            builder.Ignore(x=>x.createdAt);
            builder.Ignore(x=>x.updatedAt);
        }
    }
}
