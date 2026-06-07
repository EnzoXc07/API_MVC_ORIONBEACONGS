using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Dados
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CorpoCeleste> CorposCelestes { get; set; }
        public DbSet<AreaAnalisada> AreasAnalisadas { get; set; }
        public DbSet<Missao> Missoes { get; set; }
        public DbSet<Analise> Analises { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<LeituraSensor> LeiturasSensor { get; set; }
        public DbSet<LogAnalise> LogsAnalise { get; set; }
        public DbSet<NosqlAreaJson> NosqlAreasJson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaAnalisada>()
                .HasOne(a => a.CorpoCeleste)
                .WithMany(c => c.Areas)
                .HasForeignKey(a => a.IdCorpo)
                .HasConstraintName("FK_AREA_CORPO")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Missao>()
                .HasOne(m => m.Area)
                .WithMany(a => a.Missoes)
                .HasForeignKey(m => m.IdArea)
                .HasConstraintName("FK_MISSAO_AREA")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Analise>()
                .HasOne(a => a.Area)
                .WithMany(ar => ar.Analises)
                .HasForeignKey(a => a.IdArea)
                .HasConstraintName("FK_ANALISE_AREA")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeituraSensor>()
                .HasOne(l => l.Analise)
                .WithMany(a => a.Leituras)
                .HasForeignKey(l => l.IdAnalise)
                .HasConstraintName("FK_LEITURA_ANALISE")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeituraSensor>()
                .HasOne(l => l.Sensor)
                .WithMany(s => s.Leituras)
                .HasForeignKey(l => l.IdSensor)
                .HasConstraintName("FK_LEITURA_SENSOR")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AreaAnalisada>()
                .Property(a => a.ScoreRanking)
                .HasDefaultValue(0m);

            modelBuilder.Entity<NosqlAreaJson>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
