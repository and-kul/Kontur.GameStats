using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Kontur.GameStats.Server.Models;
using SQLite.CodeFirst;

namespace Kontur.GameStats.Server.Data.Persistence
{
    public class GameStatsDbContext : DbContext
    {
        public static bool UseTestConnectionString = false;

        public GameStatsDbContext()
            : base(UseTestConnectionString ? "TestConnection" : "DefaultConnection")
        {
            Database.Log = System.Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Statistics)
                .WithRequiredPrincipal(ps => ps.Player)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Models.Server>()
                .HasRequired(s => s.Statistics)
                .WithRequiredPrincipal(ss => ss.Server)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Models.Server>()
                .HasMany(s => s.AvailableGameModes)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("GameModesAvailableOnServer");
                    m.MapLeftKey("ServerId");
                    m.MapRightKey("GameModeId");
                });

            modelBuilder.Entity<BestPlayer>()
                .HasRequired(best => best.Player)
                .WithOptional()
                .WillCascadeOnDelete();

            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();

            var sqliteDbInitializer = new SqliteCreateDatabaseIfNotExists<GameStatsDbContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteDbInitializer);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Models.Server> Servers { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Map> Maps { get; set; }

        public DbSet<PlayerStatistics> PlayersStatistics { get; set; }
        public DbSet<PlayerGameModeStats> PlayersGameModes { get; set; }
        public DbSet<PlayerServerStats> PlayersServers { get; set; }

        public DbSet<ServerStatistics> ServersStatistics { get; set; }
        public DbSet<ServerGameModeStats> ServersGameModes { get; set; }
        public DbSet<ServerMapStats> ServersMaps { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Score> Scores { get; set; }

        public DbSet<DateServerStats> DateServerStats { get; set; }
        public DbSet<DatePlayerStats> DatePlayerStats { get; set; }

        public DbSet<BestPlayer> BestPlayers { get; set; }
    }
}