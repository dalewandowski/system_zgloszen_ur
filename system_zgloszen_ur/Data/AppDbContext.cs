using System;

namespace system_zgloszen_ur.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

}
