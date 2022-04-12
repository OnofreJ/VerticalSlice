using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using VerticalSlice.WebApi;

namespace VerticalSlice.IntegratedTests.Factories
{
    internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }
}