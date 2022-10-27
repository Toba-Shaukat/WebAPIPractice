using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 7,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 8,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 9,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 10,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 11,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 12,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 13,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 14,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 15,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 16,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 17,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 18,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                },
                new Company
                {
                    Id = 19,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                }, new Company
                {
                    Id = 20,
                    Name = "Generic IT Company",
                    Address = "Generic Address",
                    Country = "We Dont know"
                }

            );
        }
    }
}
