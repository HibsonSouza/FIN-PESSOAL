using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryType Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public List<CategoryViewModel> Subcategories { get; set; } = new List<CategoryViewModel>();
    }

    public enum CategoryType
    {
        Income,
        Expense,
        Both
    }

    public class CategoryCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryType Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    public class CategoryUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryType Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}