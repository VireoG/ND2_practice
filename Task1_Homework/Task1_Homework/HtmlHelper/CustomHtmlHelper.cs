using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Task1_Homework.Business;

namespace Task1_Homework.HtmlHelper
{
    public class CustomHtmlHelper
    {
        public HtmlString CreateDescription(string text)
        {
            string value = String.Format("{0}", text);
            return new HtmlString(value);
        }
    }
}
