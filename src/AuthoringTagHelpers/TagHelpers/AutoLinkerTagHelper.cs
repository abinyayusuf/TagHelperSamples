﻿using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthoringTagHelpers.TagHelpers
{
    [HtmlTargetElement("p")]
    public class AutoLinkerHttpTagHelper : TagHelper
    {
        public override int Order
        {
            get { return int.MinValue; }
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() : (await output.GetChildContentAsync()).GetContent();

            // Find Urls in the content and replace them with their anchor tag equivalent
            output.Content.SetHtmlContent(Regex.Replace(childContent, @"\b(?:https?://)(\S+)\b", "<a target=\"_blank\" href=\"$0\">$0</a>")); // http link version
        }
    }

    [HtmlTargetElement("p")]
    public class AutoLinkerWwwTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() : (await output.GetChildContentAsync()).GetContent();
            
            // Find Urls in the content and replace them with their anchor tag equivalent.
            output.Content.SetHtmlContent(Regex.Replace(childContent, @"\b(www\.)(\S+)\b", "<a target=\"_blank\" href=\"http://$0\">$0</a>"));  // www link version
        }
    }
}