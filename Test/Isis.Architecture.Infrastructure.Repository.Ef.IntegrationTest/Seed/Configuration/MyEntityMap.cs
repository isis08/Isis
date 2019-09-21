using Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Isis.Architecture.Infrastructure.Repository.Ef.IntegrationTest.Seed.Configuration
{
    public class MyEntityMap
    {
        public MyEntityMap(EntityTypeBuilder<MyEntity> entityBuilder)
        {
            entityBuilder.ToTable("MyEntity")
                .HasKey(x => x.Id);

            entityBuilder.Ignore(x => x.State);

            entityBuilder.Property(x => x.Id)
                .HasColumnName("id");
                //.ValueGeneratedOnAdd();

            entityBuilder.Property(x => x.ModifiedLastDate).IsRequired(false);
            entityBuilder.Property(x => x.AddedDate).IsRequired(false);
            entityBuilder.Property(x => x.Name).IsRequired();
            entityBuilder.Property(x => x.Description);

            entityBuilder.HasOne(x => x.MyNestedEntity)
                .WithMany(x => x.MyEntities)
                .HasForeignKey(x => x.MyNestedEntityId);
        }
    }
}
