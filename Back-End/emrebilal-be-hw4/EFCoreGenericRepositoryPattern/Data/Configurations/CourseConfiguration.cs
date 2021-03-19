using EFCoreGenericRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Id);               
            builder
                .Property(c => c.CourseName)
                .IsRequired();
            builder
                .HasOne(c => c.Instructor)
                .WithMany(c => c.Courses)
                .HasForeignKey(i => i.InstructorId);
        }
    }
}
