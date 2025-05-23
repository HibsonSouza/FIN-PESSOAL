using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string CultureCode { get; set; } = "pt-BR";
        public string Currency { get; set; } = "BRL";
        public bool UseDarkTheme { get; set; } = false;
        public string? ProfilePictureUrl { get; set; }
        public string? PhoneNumber { get; set; }
    }
}