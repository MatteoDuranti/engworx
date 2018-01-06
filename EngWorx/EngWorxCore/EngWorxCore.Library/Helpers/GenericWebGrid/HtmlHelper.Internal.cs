using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Web.Mvc;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages.Html {
    public partial class HtmlHelperGeneric
    {
        internal static IDictionary<string, object> ObjectToDictionary(object instance) {
            IDictionary<string, object> propertyDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (instance != null) {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(instance)) {
                    object obj2 = descriptor.GetValue(instance);
                    propertyDictionary.Add(descriptor.Name, obj2);
                }
            }
            return propertyDictionary;
        }
    }
}
