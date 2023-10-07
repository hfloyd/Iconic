using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Our.Iconic.Core.Models;
using Our.Iconic.Core.Models.DeliveryApi;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.DeliveryApi;
using Umbraco.Cms.Core.Services;

namespace Our.Iconic.Core.ValueConverters
{
    public class IconicValueConverter : PropertyValueConverterBase, IDeliveryApiPropertyValueConverter
    {
        private readonly ConfiguredPackagesCollection _configuredPackages;

        public IconicValueConverter(IDataTypeService dataTypeService, ConfiguredPackagesCollection configuredPackages)
        {
            _configuredPackages = configuredPackages;
        }
        public override bool IsConverter(IPublishedPropertyType propertyType)
             => propertyType.EditorAlias.Equals("our.iconic");

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
            => typeof(HtmlString);

   
        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        {
            if (source == null) return null;

            SelectedIcon icon;
            if (source is JObject jObject)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                icon = jObject.ToObject<SelectedIcon>();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            else
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                icon = JsonConvert.DeserializeObject<SelectedIcon>(source.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

            return icon;
        }

        public override object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview)
        {
            if (inter == null) return new HtmlString(string.Empty);
            string htmlString = string.Empty;

            var icon = (SelectedIcon)inter;

            var packages = _configuredPackages.GetConfiguredPackages(propertyType);

            if (icon != null && packages.ContainsKey(icon.PackageId))
            {
                var pckg = packages[icon.PackageId];
                htmlString = pckg?.Template?.Replace("{icon}", icon.Icon) ?? string.Empty;
            }
            return new HtmlString(htmlString);
        }
   

        public PropertyCacheLevel GetDeliveryApiPropertyCacheLevel(IPublishedPropertyType propertyType) => GetPropertyCacheLevel(propertyType);

        public Type GetDeliveryApiPropertyValueType(IPublishedPropertyType propertyType) => typeof(IconicResponse);

        public object? ConvertIntermediateToDeliveryApiObject(IPublishedElement owner,
            IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview,
            bool expanding)
        {
            if (inter == null) return null;
            var result = new IconicResponse();

            var icon = (SelectedIcon)inter;
            result.Icon = icon.Icon;

            var packages = _configuredPackages.GetConfiguredPackages(propertyType);

            if (icon != null && packages.ContainsKey(icon.PackageId))
            {
                var pckg = packages[icon.PackageId];

                if (pckg != null)
                {
                    result.PackageId = pckg.Id;                    
                    result.PackageName = pckg.Name;
                }
            }

            return result;
        }
    }
}
