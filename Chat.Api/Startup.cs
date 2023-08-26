using Chat.Api.ChatModule.Hubs;
using Chat.Api.Middlewares;
using Chat.Shared;

namespace Chat.Api
{
    public class Startup : AStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment)
        {

        }

        public override void EndpointRouteBuilder(IEndpointRouteBuilder endpointRouteBuilder)
        {
            base.EndpointRouteBuilder(endpointRouteBuilder);
            endpointRouteBuilder.MapHub<ChatHub>("/chatHub");
        }

        public override void UseMiddlewaresBeforeController(IApplicationBuilder app)
        {
            base.UseMiddlewaresBeforeController(app);
            app.UseMiddleware<LastSeenMiddleware>();
        }
    }
}
