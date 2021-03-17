using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2_WebAPI.Models;

namespace Week2_WebAPI.Data.Context
{
    public class CarsAPIContext : DbContext
    {
        public CarsAPIContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}
