// <auto-generated />
using Coffee.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coffee.Migrations
{
    [DbContext(typeof(DBCoffeeContext))]
    partial class DBCoffeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("Coffee.Models.CoffeeClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Tempurature")
                        .HasColumnType("REAL");

                    b.Property<int>("Time")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Coffees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Капучино",
                            Tempurature = 60.0,
                            Time = 30
                        },
                        new
                        {
                            Id = 2,
                            Name = "Американо",
                            Tempurature = 70.0,
                            Time = 25
                        },
                        new
                        {
                            Id = 3,
                            Name = "Раф",
                            Tempurature = 40.0,
                            Time = 22
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
