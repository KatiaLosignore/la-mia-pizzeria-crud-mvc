﻿using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Database_Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Database
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MyPizzeriaNew2023;Integrated Security=True;TrustServerCertificate=True");
        }


    }
}
