using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOgSikkerhed.Areas.Identity;
using TestOgSikkerhed.Data;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;


namespace Tests
{
    public class RazorPageTest
    {
        //public RazorPageTest(IServiceScopeFactory scopeFactory)
        //{
        //    _scopeFactory = scopeFactory;
        //    _provider = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IServiceProvider>();

        //}


        [Fact]
        public async Task IndexComponent()
        {
            // Arrenge
            using var ctx = new TestContext();



            ctx.Services.AddSingleton<MyRoleHandler>();
            ctx.Services.AddSingleton<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


            var auth = ctx.AddTestAuthorization();
            auth.SetAuthorized("Some user");
            auth.SetRoles("Admin");


            // Act
            var index = ctx.RenderComponent<TestOgSikkerhed.Pages.Index>();


            // Assert
            Assert.True(index.Instance.IsAdmin);

        }

        //public async Task Test()
        //{
        //    using var ctx = new TestContext();
        //    ctx.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        //    ctx.Services.AddSingleton<IServiceProvider>(new ServiceProvider());
        //    ctx.Services.AddSingleton<MyRoleHandler>(new MyRoleHandler());

        //    // Render the component and pass in the dependencies
        //    var component = ctx.RenderComponent<TestOgSikkerhed.Pages.Index>(parameters =>
        //    {
        //        parameters.Add(typeof(AuthenticationStateProvider), _authProvider);
        //        parameters.Add(typeof(IServiceProvider), _provider);
        //        parameters.Add(typeof(MyRoleHandler), roleHandler);
        //    });
        //}


        [Fact]
        public async Task CreateFileComponent()
        {
            // Arrrenge
            using var ctx = new TestContext();

            // Act
            var jsRuntime = new Mock<IJSRuntime>();

            ctx.Services.AddSingleton<IJSRuntime>(jsRuntime.Object);

            var component = ctx.RenderComponent<TestOgSikkerhed.Pages.CreateFile>();

            // just returns true
            var result = component.Instance.UnitTestCreateFile("testfil");


            // Assert

            Assert.True(result);
           
        }

        
    }
}
