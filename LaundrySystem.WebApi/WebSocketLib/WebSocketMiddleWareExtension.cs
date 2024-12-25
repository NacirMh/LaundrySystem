namespace LaundrySystem.WebApi.WebSocketLib
{
    public static class WebSocketMiddleWareExtension
    {

        public static IApplicationBuilder UseWebSocketMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebSocketMiddleWare>();
        }
    }
}
