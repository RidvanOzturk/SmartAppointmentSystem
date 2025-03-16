namespace SmartAppointmentSystem.Business.DTOs;

public record LogDTO(
       Guid Id,
       string Request,
       string Headers,
       string Endpoint,
       string HttpMethod,
       string StatusCode,
       string Response,
       string IP,
       DateTime CreatedAt
   );
