using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Context
{
    public  class ApplicationContext:DbContext
    {
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserDetails>().HasKey(x => new { x.Id, x.Firstname });
            modelBuilder.Entity<MenuItem>().HasData(
                    new MenuItem { Id = 1, Name = "Veg" },
                    new MenuItem { Id = 2, Name = "Non-veg" },
                    new MenuItem { Id = 3, Name = "Vegan" },
                    new MenuItem { Id = 4, Name = "Beverage" }
                );
            modelBuilder.Entity<Item>().HasData(

                new Item { Id = 1, Name = "Veg Item 1", ShelfLifeInDays = 1, Count = 2, Description = "Paneer", CategoryId = 1 },
                new Item { Id = 2, Name = "Non Veg Item 1", ShelfLifeInDays = 1, Count = 2, Description = "Non Veg", CategoryId = 2 },
                new Item { Id = 3, Name = "Vegan 1", ShelfLifeInDays = 1, Count = 2, Description = "Vegan", CategoryId = 3 },
                new Item { Id = 4, Name = "Beverage 1", ShelfLifeInDays = 1, Count = 2, Description = "beverage", CategoryId = 4 }
                );
        }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemDetails> ItemsDetails { get; set; }

       
    }
}
