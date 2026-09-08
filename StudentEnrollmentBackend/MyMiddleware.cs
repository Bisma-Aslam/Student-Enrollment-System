namespace StudentEnrollmentBackend
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        public MyMiddleware(RequestDelegate next) {
            _next = next;
            
        }
        public async Task InvokeAsync(HttpContext context) {
            Console.WriteLine("Before Middleware");
            await _next(context);
            Console.WriteLine("After Middleware");
        }
    }
}
