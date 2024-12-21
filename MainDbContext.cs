using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using workflowTest.models;

namespace workflowTest;

public class MainDbContext : DbContext
{
    public DbSet<Request> Items { get; set; }
    public DbSet<RequestHistory> ItemHistories { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    // Other DB sets and configurations...
}
