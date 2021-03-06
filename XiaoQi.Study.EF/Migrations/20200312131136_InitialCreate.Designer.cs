﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XiaoQi.Study.EF;

namespace XiaoQi.Study.EF.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200312131136_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("XiaoQi.Study.EF.ButtonInfo", b =>
                {
                    b.Property<string>("ButtonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ButtonDes")
                        .HasColumnType("TEXT");

                    b.Property<string>("ButtonIcon")
                        .HasColumnType("TEXT");

                    b.Property<string>("ButtonName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ButtonId");

                    b.ToTable("ButtonInfos");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.MenuButton_R", b =>
                {
                    b.Property<string>("MenuButtonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ButtonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuInfoId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("MenuButtonId");

                    b.ToTable("MenuButton_Rs");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.MenuInfo", b =>
                {
                    b.Property<string>("MenuInfoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FatherId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuIcon")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenyDes")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelfId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("MenuInfoId");

                    b.ToTable("MenuInfos");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.RoleInfo", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Des")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId");

                    b.ToTable("RoleInfos");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.RoleMenu_R", b =>
                {
                    b.Property<string>("RoleMenuId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuInfoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleMenuId");

                    b.ToTable("RoleMenu_Rs");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.UserInfo", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Count")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Pwd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("XiaoQi.Study.EF.UserRole_R", b =>
                {
                    b.Property<string>("UserRoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRole_Rs");
                });
#pragma warning restore 612, 618
        }
    }
}
