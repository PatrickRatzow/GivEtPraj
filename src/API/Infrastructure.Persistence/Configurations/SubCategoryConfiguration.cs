using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.OwnsOne(subcategory => subcategory.Name, name =>
            {
                name.Property(x => x.Danish)
                    .IsRequired()
                    .HasMaxLength(120);
                name.Property(x => x.English)
                    .IsRequired()
                    .HasMaxLength(120);
            });
        }
    }
}
