using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Color { get; set; }
        
        public string Icon { get; set; }
        
        public int? ParentCategoryId { get; set; }
        
        public CategoryViewModel ParentCategory { get; set; }
        
        public List<CategoryViewModel> SubCategories { get; set; } = new List<CategoryViewModel>();
        
        public CategoryType Type { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public List<TransactionViewModel> Transactions { get; set; }
    }
    
    public class CategoryCreateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Color { get; set; }
        
        public string Icon { get; set; }
        
        public int? ParentCategoryId { get; set; }
        
        public CategoryType Type { get; set; }
    }
    
    public class CategoryUpdateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Color { get; set; }
        
        public string Icon { get; set; }
        
        public int? ParentCategoryId { get; set; }
        
        public CategoryType Type { get; set; }
        
        public bool IsActive { get; set; }
    }
    
    public enum CategoryType
    {
        Income,
        Expense,
        Transfer
    }
}