#pragma checksum "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_ResponseMessage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c3e6df58d53cc5e2eecd8b1de500458bd0c5941"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ResponseMessage), @"mvc.1.0.view", @"/Views/Shared/_ResponseMessage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using OnemliBookStore.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using OnemliBookStore.Entity.Dtos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using OnemliBookStore.Entity.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using OnemliBookStore.Domain.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\_ViewImports.cshtml"
using OnemliBookStore.Ui.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c3e6df58d53cc5e2eecd8b1de500458bd0c5941", @"/Views/Shared/_ResponseMessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38764135fa712311fd522ebe8c4db4230a6e5ccf", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ResponseMessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AlertMessage>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div");
            BeginWriteAttribute("class", " class=\"", 85, "\"", 121, 3);
            WriteAttributeValue("", 93, "alert", 93, 5, true);
            WriteAttributeValue(" ", 98, "alert-", 99, 7, true);
#nullable restore
#line 6 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_ResponseMessage.cshtml"
WriteAttributeValue("", 105, Model.AlertType, 105, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <h4 class=\"alert-title\">");
#nullable restore
#line 7 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_ResponseMessage.cshtml"
                               Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n            <p>");
#nullable restore
#line 8 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_ResponseMessage.cshtml"
          Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AlertMessage> Html { get; private set; }
    }
}
#pragma warning restore 1591
