using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using USATU.Monitoring.Core.Domains.Tasks.Enums;
using USATU.Monitoring.Data.Users;

namespace USATU.Monitoring.Data.Tasks
{
    public class TaskMonitoringDbModel
    {
        public string Id { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string Result { get; set; }

        internal class Map : IEntityTypeConfiguration<TaskMonitoringDbModel>
        {
            public void Configure(EntityTypeBuilder<TaskMonitoringDbModel> builder)
            {
                builder.ToTable("task");

                builder.Property(it => it.Id)
                    .HasColumnName("id");
                builder.Property(it => it.Status)
                    .HasColumnName("status");
                builder.Property(it => it.Type)
                    .HasColumnName("type");
                builder.Property(it => it.Description)
                    .HasColumnName("description");
                builder.Property(it => it.Data)
                    .HasColumnName("data");
                builder.Property(it => it.Result)
                    .HasColumnName("result");
            }
        }

    }
}