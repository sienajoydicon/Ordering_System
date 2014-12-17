using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ordering_System.Models
{
    public class OrderSystemModel
    { }

    [Table("Items")]
    public class ItemModel
    {
        [Key]
        [Column(Order = 0)]
        public int ItemID { get; set; }

        [Display(Name = "Item Name")] 
        public string ItemName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Unit Price")] 
        public decimal? UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public string Unit { get; set; }
            
        [Display(Name = "Active")] 
        public bool IsActive { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryID { get; set; }
        public virtual CategoryModel Category { get; set; }

    }

    [Table("Category")]
    public class CategoryModel
    {
        [Key]
        [Column(Order = 0)]
        public int CategoryID { get; set; }

        [Display(Name = "Category Name")] 
        public string CategoryName { get; set; }
    }

    public class DefaultConnection : DbContext
    {
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

    }
}