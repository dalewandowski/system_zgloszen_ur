using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using system_zgloszen_ur.Entities;
using system_zgloszen_ur.Models;
using System_zgloszen_UR.DTOs.Failures;



namespace System_zgloszen_UR.Mappers
{
    public static class FailureMapper
    {

        // metoda mapująca obiekt Failure na obiekt FailureDto, wysyłana do klienta w odpowiedzi na żądanie HTTP. Klient otrzymyje tylko niezbędne informacje o awarii, bez ujawniania poufnych danych, takich jak identyfikator użytkownika, który zgłosił awarię.
        public static FailureDto FailureToDto(Failure failure)
        {
            return new FailureDto
            {
                Id = failure.Id,
                Title = failure.Title,
                FailureDescription = failure.FailureDescription,
                MachineName = failure.MachineName,
                Location = failure.Location,
                Status = failure.Status,
                CreatedAt = failure.CreatedAt,
                ReportedById = failure.ReportedById,
                ReportedBy = failure.ReportedBy.Name

            };
        }


        //Dodajemy nową awarie mapujemy z DTO na Encje Failure. Klient wysyła dane w formacie JSON, które są deserializowane do obiektu CreateFailureDto. Następnie metoda mapuje te dane na obiekt Failure, który jest zapisywany w bazie danych.
        public static Failure MapFailureToEntity(CreateFailureDto failure)
        {

            return new Failure
            {
                Title = failure.Title,
                FailureDescription = failure.FailureDescription,
                MachineName = failure.MachineName,
                Location = failure.Location,
            };
        }

        public static void UpdateFailureFromDto(Failure failure, UpdateFailureDto updateFailureDto)
        {
            failure.Title = updateFailureDto.Title;
            failure.FailureDescription = updateFailureDto.FailureDescription;
            failure.MachineName = updateFailureDto.MachineName;
            failure.Location = updateFailureDto.Location;
        }
    };


};