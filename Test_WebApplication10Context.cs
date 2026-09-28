using Microsoft.EntityFrameworkCore;
using Test_WebApplication10.Models;

public class Test_WebApplication10Context : DbContext
{
    public Test_WebApplication10Context(DbContextOptions<Test_WebApplication10Context> options)
        : base(options)
    {
    }

    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<Product> Product { get; set; } = default!;
}
