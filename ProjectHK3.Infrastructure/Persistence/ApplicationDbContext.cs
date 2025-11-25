using Microsoft.EntityFrameworkCore;
using ProjectHK3.Application.Abstractions;
using ProjectHK3.Domain.Common;
using ProjectHK3.Domain.Emtitys;
using ProjectHK3.Domain.Entities;
using System.Linq.Expressions;

namespace ProjectHK3.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private ICurrentUserService? _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService? currentUserService = null) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<AdminLogin> AdminLogin { get; set; } 
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<AuthSession> AuthSession { get; set; }
        public DbSet<CompanyDetail> CompanyDetail { get; set; }
        public DbSet<EmpRegister> EmpRegister { get; set; }
        public DbSet<HospitalInfo> HospitalInfo { get; set; }
        public DbSet<NotificationLog> NotificationLog { get; set; }
        public DbSet<PoliciesOnEmployee> PoliciesOnEmployee { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<PolicyApprovalDetail> PolicyApprovalDetail { get; set; }
        public DbSet<PolicyRequestDetail> PolicyRequestDetail { get; set; }
        public DbSet<PolicyTotalDescription> PolicyTotalDescription { get; set; }
        public DbSet<ReportLog> ReportLog { get; set; }
        public DbSet<TransactionLedger> TransactionLedger { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var entity = modelBuilder.Entity(entityType.ClrType);

                    entity.Property<DateTimeOffset>("CreatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");
                    entity.Property<DateTimeOffset>("UpdatedAt")
                        .HasDefaultValueSql("GETUTCDATE()");
                    entity.Property<string>("CreatedBy")
                        .HasDefaultValue("System");
                    entity.Property<string>("UpdatedBy")
                        .HasDefaultValue("System");
                    entity.Property<bool>("IsDeleted")
                        .HasDefaultValue(false);

                    entity.HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

        static LambdaExpression ConvertFilterExpression(Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var prop = Expression.Property(param, nameof(BaseEntity.IsDeleted));
            var compare = Expression.IsFalse(prop);
            return Expression.Lambda(compare, param);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = _currentUserService?.Email;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = _currentUserService?.Email;
                    entry.Entity.IsDeleted = false;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = _currentUserService?.Email;
                }
                else if(entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = _currentUserService?.Email;
                }
            }
            return base.SaveChanges();
        }
    }
}
