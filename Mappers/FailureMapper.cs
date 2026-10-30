using System;
using System.Linq;
using system_zgloszen_ur.Entities;
using system_zgloszen_ur.Models;
using System_zgloszen_UR.DTOs.Failures;



namespace System_zgloszen_UR.Mappers
{
    public static class FailureMapper
    {
        public static FailureDto FailureToDto(Failure failure)
        {
            if (failure == null) return null;
            return new FailureDto
            {
                Id = failure.Id,
                Title = failure.Title,
                FailureDescription = failure.FailureDescription,
                MachineName = failure.MachineName,
                Location = failure.Location,
                Status = failure.Status,
                CreatedAt = failure.CreatedAt,
                ReportedById = 5,
                ReportedBy = "Damian Lewandowski"

            };
        }


        //Dodajemy nową awarie mapujemy z DTO na Encje Failure
        public static Failure MapFailureToEntity(CreateFailureDto failure)
        {
            return new Failure
            {
                Title = failure.Title,
                FailureDescription = failure.FailureDescription,
                MachineName = failure.MachineName,
                Location = failure.Location,
                Status = FailureStatus.New,
                CreatedAt = DateTime.UtcNow
            };
        }
    };
    
    
}