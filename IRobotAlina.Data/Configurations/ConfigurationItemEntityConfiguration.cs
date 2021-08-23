using IRobotAlina.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace IRobotAlina.Data.Configurations
{
    public class ConfigurationItemEntityConfiguration : IEntityTypeConfiguration<ConfigurationItem>
    {
        public void Configure(EntityTypeBuilder<ConfigurationItem> builder)
        {
            builder.HasData(((EConfigurationItemType[])Enum.GetValues(typeof(EConfigurationItemType)))
                .Select(x => new ConfigurationItem()
                {
                    Type = x
                }));
        }
    }
}
