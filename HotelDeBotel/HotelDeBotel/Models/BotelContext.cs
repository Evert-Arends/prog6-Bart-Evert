using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelDeBotel.Models
{
    public partial class BotelContext : DbContext
    {
        public BotelContext() : base("name = HdBDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BotelContext>());
        }

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}