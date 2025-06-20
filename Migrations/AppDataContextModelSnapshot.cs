﻿// <auto-generated />
using System;
using MVCaptcha.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCaptcha.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MVCaptcha.Models.Entities.Captcha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("CaptchaName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("captcha_name");

                    b.Property<string>("CaptchaValue")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("captcha_value");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("image_url");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("level");

                    b.HasKey("Id");

                    b.ToTable("Captchas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CaptchaName = "easy1.png",
                            CaptchaValue = "4321",
                            ImageUrl = "/image/captcha/easy1.png",
                            Level = "E"
                        },
                        new
                        {
                            Id = 2,
                            CaptchaName = "easy2.png",
                            CaptchaValue = "45687",
                            ImageUrl = "/image/captcha/easy2.png",
                            Level = "E"
                        },
                        new
                        {
                            Id = 3,
                            CaptchaName = "easy3.png",
                            CaptchaValue = "965774123",
                            ImageUrl = "/image/captcha/easy3.png",
                            Level = "E"
                        },
                        new
                        {
                            Id = 4,
                            CaptchaName = "normal1.png",
                            CaptchaValue = "sPdY",
                            ImageUrl = "/image/captcha/normal1.png",
                            Level = "N"
                        },
                        new
                        {
                            Id = 5,
                            CaptchaName = "normal2.png",
                            CaptchaValue = "cRse",
                            ImageUrl = "/image/captcha/normal2.png",
                            Level = "N"
                        },
                        new
                        {
                            Id = 6,
                            CaptchaName = "normal3.png",
                            CaptchaValue = "opMuMI",
                            ImageUrl = "/image/captcha/normal3.png",
                            Level = "N"
                        },
                        new
                        {
                            Id = 7,
                            CaptchaName = "hard1.png",
                            CaptchaValue = "1ess2",
                            ImageUrl = "/image/captcha/hard1.png",
                            Level = "H"
                        },
                        new
                        {
                            Id = 8,
                            CaptchaName = "hard2.png",
                            CaptchaValue = "2wP34",
                            ImageUrl = "/image/captcha/hard2.png",
                            Level = "H"
                        },
                        new
                        {
                            Id = 9,
                            CaptchaName = "hard3.png",
                            CaptchaValue = "Lz00Oda",
                            ImageUrl = "/image/captcha/hard3.png",
                            Level = "H"
                        });
                });

            modelBuilder.Entity("MVCaptcha.Models.Entities.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("session_id");

                    b.Property<DateTime?>("DateTimeEnded")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("datetime_ended");

                    b.Property<DateTime>("DateTimeStarted")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("datetime_started");

                    b.Property<string>("Difficulty")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("difficulty");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("score");

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
