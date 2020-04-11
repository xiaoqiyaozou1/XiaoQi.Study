using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XiaoQi.Study.EF;
using XiaoQi.Study.Model;

namespace XiaoQi.Study.EF
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }

        public DbSet<UserRole_R> UserRole_Rs { get; set; }
        public DbSet<RoleMenu_R> RoleMenu_Rs { get; set; }

        public DbSet<MenuInfo> MenuInfos { get; set; }

        public DbSet<ButtonInfo> ButtonInfos { get; set; }

        public DbSet<MenuButton_R> MenuButton_Rs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleInfo>()
                .HasKey(c => c.RoleId);
            modelBuilder.Entity<UserInfo>()
                .Ignore(c => c._RoleInfo)
                .HasKey(c => c.UserId);

            modelBuilder.Entity<UserRole_R>()
              .HasKey(c => c.UserRoleId);
            modelBuilder.Entity<RoleMenu_R>()
                .HasKey(o => o.RoleMenuId);
            modelBuilder.Entity<MenuInfo>()
                .HasKey(o => o.MenuInfoId);
            modelBuilder.Entity<ButtonInfo>()
                .HasKey(o => o.ButtonId);
            modelBuilder.Entity<MenuButton_R>()
                .HasKey(o => o.MenuButtonId);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            options.UseSqlite(@"Data Source=" + basePath + "userinfo.db");
        }
    }
}
