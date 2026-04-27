using Microsoft.EntityFrameworkCore;

namespace PruebaRiwi.Data;

public class MysqlDbContext : DbContext
{
    public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options)
    {
        
    }
    
    
    
}