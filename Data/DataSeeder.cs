using System;
using Microsoft.EntityFrameworkCore;
using system_zgloszen_ur.Entities;

namespace system_zgloszen_ur.Data;

public class DataSeeder (AppDbContext context)
{

    public async Task SetUserAsync()
    {
        if (!await context.Users.AnyAsync())
        {
            var adminUser = new User
            {
                Name = "Admin",
                Email = "admin@example.com",
                PasswordHash = "HashedPassword",
                Role= UserRole.Admin
            };

            var regularUser1 = new User
            {
                Name = "Dawid Kowalski",
                Email = "dawid.kowalski@example.com",
                PasswordHash = "HashedPassword123",
                Role = UserRole.User
            };

            var regularUser2 = new User
            {
                Name = "Adam Nowak",
                Email = "adam.nowak@example.com",
                PasswordHash = "HashedPassword456",
                Role = UserRole.User
            };
            
            var regularUser3 = new User
            {
                Name = "Jan Kowalski",
                Email = "jan.kowalski@example.com",
                PasswordHash = "HashedPassword789",
                Role = UserRole.User
            };

            await context.Users.AddRangeAsync(adminUser, regularUser1, regularUser2, regularUser3);
            await context.SaveChangesAsync();
        }
    }


    public async Task SetFailureAsync()
    {
        
        if (!await context.Failures.AnyAsync())
        {
            
            var mieszarka = new Failure
            {
                Title = "Mieszarka wyłaczyła się",
                FailureDescription = "Wyłączyła sie mieszarka podczas pracy. Nie można jej uruchomić ponownie. Z przekładni cieknie olej.",
                MachineName = "Mieszarka",
                Location = "Hala produkcji 1",
                Status = FailureStatus.New,
                ReportedById = context.Users.First(u => u.Id == 3).Id,
            };

            var prasa = new Failure
            {
                Title = "Prasa nie działa",
                FailureDescription = "Prasa nie działa. Nie można jej uruchomić. Słychać dziwne dźwięki podczas próby uruchomienia.",
                MachineName = "Prasa hydrauliczna",
                Location = "Hala 2",
                Status = FailureStatus.InProgress,
                ReportedById = context.Users.First(u => u.Id == 2).Id,
                Repairs = new List<Repair>
                {
                    new Repair
                    {
                        RepairDescription = "",
                        StartedAt = DateTime.UtcNow,
                        PerformedById = context.Users.First(u => u.Id == 4).Id,
                        
                    }
                }
            };

            var tokarka = new Failure
            {
                Title = "Tokarka wyłącza się podczas pracy",
                FailureDescription = "Tokarka wyłącza się podczas pracy. Przycisk awaryjnego zatrzmania nie działa.",
                MachineName = "Tokarka ",
                Location = "Hala produkcyjna 1",
                Status = FailureStatus.Resolved,
                ReportedById = context.Users.First(u => u.Id == 3).Id,
                Repairs = new List<Repair>
                {
                    new Repair
                    {
                        RepairDescription = "Wymiana przycisku awaryjnego zatrzymania i sprawdzenie instalacji elektrycznej.Przerwany przewód w instalacji elektrycznej.",
                        StartedAt = DateTime.UtcNow,
                        ResolvedAt = DateTime.UtcNow.AddHours(1),
                        PerformedById = context.Users.First(u => u.Id == 4).Id,
                        
                    }
                }
            };

            var spawarka = new Failure
            {
                Title = "Spawarka",
                FailureDescription = "Przy próbie uruchomienia spawarki, wydobywa się dym i czuć zapach spalenizny.",
                MachineName = "Spawarka",
                Location = "Magazyn wyrobów gotowych",
                Status = FailureStatus.New,
                ReportedById = context.Users.First(u => u.Id == 2).Id
            };
            await context.Failures.AddRangeAsync(mieszarka, prasa, tokarka, spawarka);
            await context.SaveChangesAsync();
        }
    }

}
