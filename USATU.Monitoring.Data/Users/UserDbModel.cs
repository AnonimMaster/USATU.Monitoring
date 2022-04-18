using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace USATU.Monitoring.Data.Users
{
    public class UserDbModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        internal class Map : IEntityTypeConfiguration<UserDbModel>
        {
            public void Configure(EntityTypeBuilder<UserDbModel> builder)
            {
                builder.ToTable("user");

                builder.Property(it => it.Id)
                    .HasColumnName("id");
                builder.Property(it => it.Login)
                    .HasColumnName("login");
                builder.Property(it => it.Password)
                    .HasColumnName("password");
            }
        }

    }
}