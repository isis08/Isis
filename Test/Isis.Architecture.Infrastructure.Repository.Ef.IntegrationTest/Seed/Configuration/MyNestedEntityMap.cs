using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Configuration
{
    public class MyNestedEntityMap
    {
        public MyNestedEntityMap(EntityTypeBuilder<MyNestedEntity> entityBuilder)
        {
            entityBuilder.Ignore(x => x.State);

            entityBuilder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entityBuilder.Property(x => x.Name).IsRequired();
            entityBuilder.Property(x => x.Description);
            entityBuilder.Property(x => x.ModifiedLastDate).IsRequired(false);
            entityBuilder.Property(x => x.AddedDate).IsRequired(false);


            entityBuilder.HasMany(x => x.MyEntities)
                .WithOne(x => x.MyNestedEntity)
                .HasForeignKey(x => x.MyNestedEntityId);
        }
    }
}
