#pragma checksum "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e5d7780dedc93efa0f27924af84f8741e9be7d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__navbar), @"mvc.1.0.view", @"/Views/Shared/_navbar.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e5d7780dedc93efa0f27924af84f8741e9be7d2", @"/Views/Shared/_navbar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38764135fa712311fd522ebe8c4db4230a6e5ccf", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__navbar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""navbar bg-info navbar-dark navbar-expand-sm"">
    <div class=""container"">
        <a href=""/"" class=""navbar-brand"">OnemliBookStore</a>
        <ul class=""navbar-nav mr-auto"">

            <li class=""nav-item"">
                <a href=""/Books"" class=""nav-link"">Books</a>
            </li>
");
#nullable restore
#line 9 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
             if (User.Identity.IsAuthenticated)
            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item\">\r\n                    <a href=\"/cart\" class=\"nav-link\">Cart</a>\r\n                </li>\r\n");
#nullable restore
#line 16 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
                 if (User.IsInRole("Admin"))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <li class=""nav-item"">
                        <a href=""/admin/books"" class=""nav-link"">Admin Books</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""/admin/categories"" class=""nav-link"">Admin Categories</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""/admin/role/list"" class=""nav-link"">Roles</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""/admin/user/list"" class=""nav-link"">Users</a>
                    </li>
");
#nullable restore
#line 30 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </ul>\r\n\r\n\r\n        <ul class=\"navbar-nav ml-auto\">\r\n\r\n");
#nullable restore
#line 38 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
             if (User.Identity.IsAuthenticated)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item\">\r\n                    <a class=\"nav-link\">");
#nullable restore
#line 41 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
                                   Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                </li>\r\n                <li class=\"nav-item\">\r\n                    <a href=\"/account/logout\" class=\"nav-link\">Logout</a>\r\n                </li>\r\n");
#nullable restore
#line 46 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <li class=""nav-item"">
                    <a href=""/account/login"" class=""nav-link"">Login</a>
                </li>
                <li class=""nav-item"">
                    <a href=""/account/register"" class=""nav-link"">Register</a>
                </li>
");
#nullable restore
#line 55 "C:\Users\Ugurcan\Desktop\Workspace\OnemliBookStore\Views\Shared\_navbar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
