using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleSearch.Data;
using PeopleSearch.Data.Models;

namespace PeopleSearch.Data.Migrations
{
    [DbContext(typeof(PersonSearchingContext))]
    [Migration("20170225201745_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("PeopleSearch.Data.Models.Interest", b =>
                {
                    b.Property<string>("InterestId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PersonId");

                    b.Property<string>("category");

                    b.HasKey("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("PeopleSearch.Data.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int>("AddressState");

                    b.Property<int>("Age");

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PictureUrl");

                    b.Property<int>("Zip");

                    b.HasKey("PersonId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("PeopleSearch.Data.Models.Interest", b =>
                {
                    b.HasOne("PeopleSearch.Data.Models.Person", "Person")
                        .WithMany("Interests")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
