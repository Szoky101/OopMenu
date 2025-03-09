using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;

public class DrawingContext : DbContext
{
    public DbSet<Drawing> Drawings { get; set; }
    public DbSet<Dot> Dots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DrawingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}
public class Drawing
{
    public int DrawingId { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Dot> Dots { get; set; }
}

public class Dot
{
    public int DotId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Shade { get; set; }
    public string Color { get; set; }
    public int DrawingId { get; set; }
    public Drawing Drawing { get; set; }
}