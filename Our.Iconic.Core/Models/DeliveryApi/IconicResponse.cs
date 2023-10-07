using System;

namespace Our.Iconic.Core.Models.DeliveryApi
{
    public class IconicResponse
    {
        public string? Icon { get; set; } = String.Empty;
        public Guid? PackageId { get; set; }
        public string? PackageName { get; set; } = String.Empty;        
    }
}
