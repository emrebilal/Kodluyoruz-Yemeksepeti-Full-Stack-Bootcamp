using EFCoreGenericRepositoryPattern.Data.Configurations;
using EFCoreGenericRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                 .ApplyConfiguration(new CourseConfiguration());

            builder
                .ApplyConfiguration(new InstructorConfiguration());
        }
    }
}
