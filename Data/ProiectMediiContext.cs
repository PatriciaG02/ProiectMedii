using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Models;


namespace ProiectMedii.Data
{
    public class ProiectMediiContext : DbContext
    {
        public ProiectMediiContext (DbContextOptions<ProiectMediiContext> options)
            : base(options)
        {
        }

        public DbSet<ProiectMedii.Models.Trainer> Trainer { get; set; } = default!;
        public DbSet<ProiectMedii.Models.TrainingSession> TrainingSession{ get; set; } = default!;
        public DbSet<ProiectMedii.Models.Location> Location{ get; set; } = default!;
        public DbSet<ProiectMedii.Models.User> User { get; set; } = default!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurare relații dacă este necesar
            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Trainer)
                .WithMany(t => t.TrainingSessions)
                .HasForeignKey(ts => ts.TrainerId);

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.Location)
                .WithMany(l => l.TrainingSessions)
                .HasForeignKey(ts => ts.LocationId);

            modelBuilder.Entity<TrainingSession>()
                .HasOne(ts => ts.User)
                .WithMany(u => u.TrainingSessions)
                .HasForeignKey(ts => ts.UserId);
        }
    }
}
