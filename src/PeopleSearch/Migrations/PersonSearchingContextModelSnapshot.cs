using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleSearch.Data;
using PeopleSearch.Data.Models;

namespace PeopleSearch.Migrations
{
    [DbContext(typeof(PersonSearchingContext))]
    partial class PersonSearchingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

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

                    b.Property<string>("Interests");

                    b.Property<string>("LastName");

                    b.Property<string>("PictureUrl");

                    b.Property<int>("Zip");

                    b.HasKey("PersonId");

                    b.ToTable("People");
                });
        }
    }
}
