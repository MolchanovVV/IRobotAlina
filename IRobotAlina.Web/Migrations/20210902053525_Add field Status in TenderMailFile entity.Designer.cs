﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenderDocumentsScraper.Data;

namespace IRobotAlina.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210902053525_Add field Status in TenderMailFile entity")]
    partial class AddfieldStatusinTenderMailFileentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IRobotAlina.Data.Entities.ConfigurationItem", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Type");

                    b.ToTable("ConfigurationItems");

                    b.HasData(
                        new
                        {
                            Type = 0
                        },
                        new
                        {
                            Type = 1
                        },
                        new
                        {
                            Type = 2
                        },
                        new
                        {
                            Type = 3
                        },
                        new
                        {
                            Type = 4
                        },
                        new
                        {
                            Type = 5
                        });
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.Tender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenderMailId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("TenderMailId");

                    b.ToTable("Tenders");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderFileAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArchiveName")
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExtractedText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullPath")
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenderId");

                    b.ToTable("TenderFileAttachments");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HTMLBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InnerTextBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("ReceiptDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenderPlatformId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("UIDL")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("TenderPlatformId");

                    b.ToTable("TenderMails");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderMailFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ParsedData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenderMailId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenderMailId");

                    b.ToTable("TenderMailFiles");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderPlatform", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("TenderPlatforms");

                    b.HasData(
                        new
                        {
                            Id = "58fbc48e-b203-42b0-8d08-caf75a1a4ed1",
                            Name = "Zakupki"
                        });
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.Tender", b =>
                {
                    b.HasOne("IRobotAlina.Data.Entities.TenderMail", "TenderMail")
                        .WithMany("Tenders")
                        .HasForeignKey("TenderMailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("IRobotAlina.Data.Entities.Customer", "Customer", b1 =>
                        {
                            b1.Property<int>("TenderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("INN")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("KPP")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(512)");

                            b1.Property<string>("PlaceDelivery")
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<string>("Region")
                                .HasColumnType("nvarchar(512)");

                            b1.HasKey("TenderId");

                            b1.ToTable("Tenders");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.OwnsOne("IRobotAlina.Data.Entities.Purchase", "Purchase", b1 =>
                        {
                            b1.Property<int>("TenderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Comment")
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<DateTime?>("ConductingSelection")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Currency")
                                .HasColumnType("varchar(3)");

                            b1.Property<DateTime?>("DeadlineAcceptApp")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ETP")
                                .HasColumnType("nvarchar(128)");

                            b1.Property<string>("InitMinPrice")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("Mark")
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(512)");

                            b1.Property<DateTime?>("PublicationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("SecuringApp")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("SecuringContract")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("SelectionMethod")
                                .HasColumnType("varchar(128)");

                            b1.Property<string>("SelectionStage")
                                .HasColumnType("varchar(128)");

                            b1.Property<string>("TypeBidding")
                                .HasColumnType("varchar(128)");

                            b1.HasKey("TenderId");

                            b1.ToTable("Tenders");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.OwnsOne("IRobotAlina.Data.Entities.Result", "Result", b1 =>
                        {
                            b1.Property<int>("TenderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("ContractPrice")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("PercenteDecline")
                                .HasColumnType("varchar(20)");

                            b1.Property<DateTime?>("PublicationProtocol")
                                .HasColumnType("datetime2");

                            b1.Property<string>("SupplierINN")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("SupplierName")
                                .HasColumnType("nvarchar(512)");

                            b1.Property<string>("WinnerINN")
                                .HasColumnType("varchar(20)");

                            b1.Property<string>("WinnerName")
                                .HasColumnType("nvarchar(512)");

                            b1.Property<string>("WinnerOffer")
                                .HasColumnType("varchar(255)");

                            b1.HasKey("TenderId");

                            b1.ToTable("Tenders");

                            b1.WithOwner()
                                .HasForeignKey("TenderId");
                        });

                    b.Navigation("Customer");

                    b.Navigation("Purchase");

                    b.Navigation("Result");

                    b.Navigation("TenderMail");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderFileAttachment", b =>
                {
                    b.HasOne("IRobotAlina.Data.Entities.Tender", "Tender")
                        .WithMany("FileAttachments")
                        .HasForeignKey("TenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tender");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderMail", b =>
                {
                    b.HasOne("IRobotAlina.Data.Entities.TenderPlatform", "TenderPlatform")
                        .WithMany("TenderMails")
                        .HasForeignKey("TenderPlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenderPlatform");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderMailFile", b =>
                {
                    b.HasOne("IRobotAlina.Data.Entities.TenderMail", "TenderMail")
                        .WithMany("Files")
                        .HasForeignKey("TenderMailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenderMail");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.Tender", b =>
                {
                    b.Navigation("FileAttachments");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderMail", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Tenders");
                });

            modelBuilder.Entity("IRobotAlina.Data.Entities.TenderPlatform", b =>
                {
                    b.Navigation("TenderMails");
                });
#pragma warning restore 612, 618
        }
    }
}
