using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoShow.Models;

namespace ProjetoShow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Evento> Eventos {get; set;}
        public DbSet<CasaShow> CasaShows {get; set;}
        public DbSet<Compra> Compras {get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
