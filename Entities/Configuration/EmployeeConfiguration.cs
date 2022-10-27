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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (
                new Employee
                {
                    Id = 6,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 7,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 8,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 9,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 10,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 11,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 12,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                },
                new Employee
                {
                    Id = 13,
                    Name = "Generic Name",
                    Age = 26,
                    Position = "Software Developer",
                    CompanyId = 7
                }
            );
        }
    }
}
