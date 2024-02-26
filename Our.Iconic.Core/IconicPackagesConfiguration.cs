using Our.Iconic.Core.Models;
using System.Collections.Generic;
using Umbraco.Cms.Core.PropertyEditors;

namespace Our.Iconic.Core
{
    public class IconicPackagesConfiguration
    {
        [ConfigurationField("packages", "Packages configuration", "/App_Plugins/Iconic/Views/iconic.prevalues.html", Description = "Add the font packages you want to use")]
        public IEnumerable<Package> Packages { get; set; } = new List<Package>();
    }
}
