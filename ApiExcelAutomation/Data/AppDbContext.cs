using ApiExcelAutomation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiExcelAutomation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Salary)
                    .HasPrecision(12, 2);

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Employee>().HasData(
    new Employee
    {
        Id = 1,
        Name = "Rahul Sharma",
        Email = "rahul.sharma@example.com",
        Department = "IT",
        Salary = 65000,
        JoiningDate = new DateTime(2024, 1, 15)
    },
    new Employee
    {
        Id = 2,
        Name = "Priya Das",
        Email = "priya.das@example.com",
        Department = "HR",
        Salary = 58000,
        JoiningDate = new DateTime(2023, 7, 10)
    },
    new Employee
    {
        Id = 3,
        Name = "Amit Kumar",
        Email = "amit.kumar@example.com",
        Department = "Finance",
        Salary = 72000,
        JoiningDate = new DateTime(2022, 11, 5)
    },
    new Employee
    {
        Id = 4,
        Name = "Sneha Patel",
        Email = "sneha.patel@example.com",
        Department = "IT",
        Salary = 81000,
        JoiningDate = new DateTime(2021, 6, 20)
    },
    new Employee
    {
        Id = 5,
        Name = "Arjun Singh",
        Email = "arjun.singh@example.com",
        Department = "Sales",
        Salary = 55000,
        JoiningDate = new DateTime(2024, 3, 12)
    },
    new Employee
    {
        Id = 6,
        Name = "Neha Roy",
        Email = "neha.roy@example.com",
        Department = "Finance",
        Salary = 69000,
        JoiningDate = new DateTime(2023, 9, 18)
    },
    new Employee
    {
        Id = 7,
        Name = "Vikash Das",
        Email = "vikash.das@example.com",
        Department = "IT",
        Salary = 92000,
        JoiningDate = new DateTime(2020, 2, 14)
    },
    new Employee
    {
        Id = 8,
        Name = "Anjali Mishra",
        Email = "anjali.mishra@example.com",
        Department = "HR",
        Salary = 62000,
        JoiningDate = new DateTime(2022, 8, 25)
    }
);
        }
    }
}
