using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcTIC_IT.Models
{
    public class LocalizationDisplayNameAttribute : DisplayNameAttribute
    {
        private DisplayAttribute display;

        public LocalizationDisplayNameAttribute(string resourceName, Type resourceType)
        {
            this.display = new DisplayAttribute()
            {
                ResourceType = resourceType,
                Name = resourceName
            };
        }

        public override string DisplayName
        {
            get
            {
                return display.GetName();
            }
        }


    }
}