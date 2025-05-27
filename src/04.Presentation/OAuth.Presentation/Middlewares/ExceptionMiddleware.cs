using OAuth.Common.Exceptions;

namespace OAuth.Presentation.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = 500;
            string message = "خطای ناشناخته";
            string errorType = ex.GetType().Name;

            // اگر Exception از نوع CustomException بود

            // اگر Exception سفارشی نبود، فقط اسم کلاس را بده
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                errorType
            }));
        }

    }
}

//public class ExceptionHandlingMiddleware
//{
//    private readonly RequestDelegate _next;
//    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

//    public async Task Invoke(HttpContext context)
//    {
//        try
//        {
//            await _next(context);
//        }
//        catch (Exception ex)
//        {
//            context.Response.ContentType = "application/json";
//            int statusCode = 500;
//            string message = "خطای ناشناخته";
//            string errorType = ex.GetType().Name;

//            // اگر Exception از نوع CustomException بود

//                // اگر Exception سفارشی نبود، فقط اسم کلاس را بده
//                context.Response.StatusCode = statusCode;
//                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
//                {
//                    errorType
//                }));
//        }
//    }
//}