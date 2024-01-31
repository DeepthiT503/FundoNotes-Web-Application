using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RespositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryLayer.Context
{
    public class FundoContext:DbContext
    {
        public FundoContext(DbContextOptions options):base(options){ }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<NotesEntity> notes { get; set; }
        public DbSet<ImageEntity> image { get; set; }
        public DbSet<Colloboratory> colloborate { get; set; }
    }
}
