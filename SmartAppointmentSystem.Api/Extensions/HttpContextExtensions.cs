using System.Text;

namespace SmartAppointmentSystem.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetIPAddress(this ConnectionInfo connection)
    {
        const string LocalhostRepresentation = "::1";
        const string LocalhostIP = "127.0.0.1";

        var ip = connection.RemoteIpAddress?.ToString() ?? "Unknown";

        if (ip == LocalhostRepresentation)
        {
            return LocalhostIP;
        }

        return ip;
    }

    public static async Task<string> GetRequestBodyAsync(this HttpRequest request, CancellationToken cancellationToken = default)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync(cancellationToken);
        request.Body.Position = 0;
        return requestBody;
    }
}
