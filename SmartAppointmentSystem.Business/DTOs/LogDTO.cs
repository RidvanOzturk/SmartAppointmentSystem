namespace SmartAppointmentSystem.Business.DTOs;

public record LogDTO(
       Guid Id,
       string Request,
       string Headers,
       string Endpoint,
       string HttpMethod,
       string Response,
       string Ip,
       DateTime CreatedAt
   );
