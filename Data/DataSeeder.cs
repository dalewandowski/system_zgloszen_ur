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
            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }


    public async Task SetFailureAsync()
    {
        var user = await context.Users.FirstAsync();

        
        if (!await context.Failures.AnyAsync())
        {
            
            var mieszarka = new Failure
            {
                Title = "Mieszarka nie działa",
                FailureDescription = "To jest przykładowy opis awarii.",
                MachineName = "Mieszarka 1",
                Location = "Hala 1",
                Status = FailureStatus.New,
                ReportedById = user.Id
            };

            var prasa = new Failure
            {
                Title = "Prasa nie działa",
                FailureDescription = "To jest przykładowy opis awarii.",
                MachineName = "Prasa 1",
                Location = "Hala 2",
                Status = FailureStatus.InProgress,
                ReportedById = user.Id,
                Repairs = new List<Repair>
                {
                    new Repair
                    {
                        RepairDescription = "",
                        StartedAt = DateTime.UtcNow,
                        PerformedById = user.Id,
                        
                    }
                }
            };

            var tokarka = new Failure
            {
                Title = "Tokarka nie działa",
                FailureDescription = "To jest przykładowy opis awarii.",
                MachineName = "Tokarka 3",
                Location = "Hala 3",
                Status = FailureStatus.Resolved,
                ReportedById = user.Id,
                Repairs = new List<Repair>
                {
                    new Repair
                    {
                        RepairDescription = "To jest przykładowy opis naprawy.",
                        StartedAt = DateTime.UtcNow,
                        ResolvedAt = DateTime.UtcNow.AddHours(1),
                        PerformedById = user.Id,
                        
                    }
                }
            };

            var spawarka = new Failure
            {
                Title = "Spawarka nie działa",
                FailureDescription = "To jest przykładowy opis awarii.",
                MachineName = "Spawarka 1",
                Location = "Hala 4",
                Status = FailureStatus.New,
                ReportedById = user.Id
            };
            await context.Failures.AddRangeAsync(mieszarka, prasa, tokarka, spawarka);
            await context.SaveChangesAsync();
        }
    }

}
