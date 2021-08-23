using IRobotAlina.Data.Entities;
using IRobotAlina.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace IRobotAlina.Data.Configurations
{
    public class TenderPlatformEntityConfiguration : IEntityTypeConfiguration<TenderPlatform>
    {
        public void Configure(EntityTypeBuilder<TenderPlatform> builder)
        {
            builder.HasData(TenderPlatforms.All()
                .Select(x => new TenderPlatform()
                {
                    Id = x.Id,
                    Name = x.Name
                }));
        }
    }
}
