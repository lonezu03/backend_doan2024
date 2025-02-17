﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebStore.Entity;


namespace WebStore.Context
{
    public class ApplicationDbContext : DbContext
    {
        private object d;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; } // Ánh xạ tới bảng Users
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<Description> Description { get; set; }
        public DbSet<Variant> Variant { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Address> Address { get; set; }
       // public DbSet<Shipping> Shippings { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Order_Item> Order_Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Material)         // Một Product có một Material
                .WithMany(m => m.Products)       // Một Material có nhiều Product
                .HasForeignKey(p => p.Material_Id); // Khóa ngoại là MaterialId trong Product

            // Quan hệ 1:n giữa Gender và Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Gender)           // Một Product có một Gender
                .WithMany(g => g.Products)       // Một Gender có nhiều Product
                .HasForeignKey(p => p.Gender_Id); // Khóa ngoại là GenderId trong Product
                //.OnDelete(DeleteBehavior.Restrict); // Tránh xóa lẫn nhau khi xóa một thực thể

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)       // Một Product có nhiều Variant
                .WithOne(v => v.Product)         // Một Variant chỉ thuộc về một Product
                .HasForeignKey(v => v.Product_Id); // Khóa ngoại ở bảng Variant trỏ tới Product
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(a => a.Address)
                .HasForeignKey(a => a.User_Id)
                 .OnDelete(DeleteBehavior.Restrict);
            

            modelBuilder.Entity<Product>()
           .HasMany(p => p.Variants)
           .WithOne(v => v.Product)
           .HasForeignKey(v => v.Product_Id);

            // Variant - Color (nhiều-1)
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Color)
                .WithMany(c => c.Variants)
                .HasForeignKey(v => v.Color_Id);

            // Variant - category (nhiều-1)
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Variants)
                .HasForeignKey(v => v.Category_Id);

            // Variant - Size (nhiều-1)
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Size)
                .WithMany(s => s.Variants)
                .HasForeignKey(v => v.Size_Id);
            // Variant - description (nhiều-1)
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Description)
                .WithMany(d => d.Variant)
                .HasForeignKey(v => v.Description_Id);

            // Variant - Image (1-nhiều)
           
            // User - orders (1-nhiều)
            modelBuilder.Entity<Users>()
                .HasMany(v => v.orders)
                .WithOne(i => i.Users)
                .HasForeignKey(i => i.User_Id);


            // Đặt lại ON DELETE NO ACTION cho Orders -> Shippings
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Shipping)
            //    .WithMany()
            //    .HasForeignKey(o => o.Shipping_Id)
            //    .OnDelete(DeleteBehavior.Restrict); // No cascade delete
           //orders voi orderitem 1:n
            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderItem)        
                .WithOne(oi => oi.Order)           
                .HasForeignKey(oi => oi.Order_Id);
            // order_item vơi inventory 1:1
            modelBuilder.Entity<Order_Item>()
                .HasOne(o => o.Inventory)
                .WithOne(i => i.Order_Item)
                .HasForeignKey<Order_Item>(o => o.Inventory_Id);
            // variant voi inventory 1:n
            modelBuilder.Entity<Variant>()
                    .HasMany(v => v.Inventories)
                    .WithOne(i => i.Variant)
                    .HasForeignKey(i => i.Variant_Id);
            // variant voi category n:1
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Category)           
                .WithMany(c => c.Variants)         
                .HasForeignKey(v => v.Category_Id) 
                .OnDelete(DeleteBehavior.Restrict);
            // variant voi description n:1
            modelBuilder.Entity<Variant>()
                .HasOne(v => v.Description)
                .WithMany(c => c.Variant)
                .HasForeignKey(v => v.Description_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
