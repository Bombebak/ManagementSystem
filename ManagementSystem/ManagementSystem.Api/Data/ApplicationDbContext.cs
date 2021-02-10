using System;
using System.Collections.Generic;
using System.Text;
using ManagementSystem.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Api.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationTeamUser> TeamUsers { get; set; }
        public virtual DbSet<ApplicationTeam> Teams { get; set; }
        public virtual DbSet<ApplicationTimeRegistration> TimeRegistrations { get; set; }
        public virtual DbSet<ApplicationTaskUser> TaskUsers { get; set; }
        public virtual DbSet<ApplicationTask> Tasks { get; set; }
        public virtual DbSet<ApplicationProject> Projects { get; set; }
        public virtual DbSet<ApplicationSprint> Sprints { get; set; }
        public virtual DbSet<ApplicationSprintUser> SprintUsers { get; set; }
        public virtual DbSet<ApplicationMessage> Messages { get; set; }
        public virtual DbSet<ApplicationFile> Files { get; set; }
        public virtual DbSet<ApplicationMessageFile> MessageFiles { get; set; }
        public virtual DbSet<ApplicationTaskMessage> TaskMessages { get; set; }
        public virtual DbSet<ApplicationTaskFile> TaskFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.TeamUsers)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.SprintUsers)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.TaskUsers)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.TimeRegistrations)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.Messages)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.ClientCascade);

                b.HasMany(e => e.Files)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationProject>(b =>
            {
                b.ToTable("Project");
                b.HasKey(p => p.Id);
                b.HasMany(e => e.Tasks)
                   .WithOne(e => e.Project)
                   .HasForeignKey(ps => ps.ProjectId);
            });

            modelBuilder.Entity<ApplicationSprint>(b =>
            {
                b.ToTable("Sprint");

                b.HasKey(p => p.Id);

                b.HasMany(e => e.Tasks)
                   .WithOne(e => e.Sprint)
                   .HasForeignKey(ps => ps.SprintId);

                b.HasMany(e => e.SprintUsers)
                   .WithOne(e => e.Sprint)
                   .HasForeignKey(ps => ps.SprintId);
            });

            modelBuilder.Entity<ApplicationSprintUser>(b =>
            {
                b.ToTable("Rel_Sprint_User");

                b.HasKey(e => new { e.SprintId, e.UserId });

                modelBuilder.Entity<ApplicationSprintUser>()
                .HasOne(e => e.Sprint)
                .WithMany(m => m.SprintUsers)
                .HasForeignKey(x => x.SprintId);
            });                    

            modelBuilder.Entity<ApplicationTask>(b =>
            {
                b.ToTable("Task");

                b.HasKey(p => p.Id);

                b.HasMany(e => e.TaskUsers)
                   .WithOne(e => e.Task)
                   .HasForeignKey(ps => ps.TaskId);

                b.HasMany(e => e.TimeRegistrations)
                   .WithOne(e => e.Task)
                   .HasForeignKey(ps => ps.TaskId);

                b.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

                b.HasMany(e => e.TaskFiles)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<ApplicationTaskUser>(b =>
            {
                b.ToTable("Rel_Task_User");

                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ApplicationTeam>(b =>
            {
                b.ToTable("Team");

                b.HasKey(p => p.Id);

                b.HasMany(e => e.TeamUsers)
                  .WithOne(e => e.Team)
                  .HasForeignKey(ps => ps.TeamId)
                  .IsRequired();

                b.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
            });

            modelBuilder.Entity<ApplicationTeamUser>(b =>
            {
                b.ToTable("Rel_Team_User");

                b.HasKey(e => new { e.TeamId, e.UserId });
            });

            modelBuilder.Entity<ApplicationTimeRegistration>(b =>
            {
                b.ToTable("TimeRegistration");
                b.HasKey(p => p.Id);
            });

            modelBuilder.Entity<ApplicationMessage>(b =>
            {
                b.ToTable("Message");

                b.HasKey(p => p.Id);

                b.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

                b.HasMany(e => e.MessageFiles)
                .WithOne(e => e.Message)
                .HasForeignKey(e => e.MessageId)
                .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<ApplicationFile>(b =>
            {
                b.ToTable("File");

                b.HasKey(p => p.Id);

                b.HasMany(e => e.MessageFiles)
                .WithOne(e => e.File)
                .HasForeignKey(e => e.FileId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ApplicationMessageFile>(b =>
            {
                b.ToTable("Rel_Message_File");

                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ApplicationTaskMessage>(b =>
            {
                b.ToTable("Rel_Task_Message");

                b.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ApplicationTaskFile>(b =>
            {
                b.ToTable("Rel_Task_File");

                b.HasKey(e => e.Id);
            });
        }
    }
}
