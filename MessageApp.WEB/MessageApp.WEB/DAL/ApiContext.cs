using MessageApp.WEB.DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace MessageApp.WEB.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<UserDTO> Users { get; set; }

        public DbSet<MessageDTO> Messages { get; set; }

        public DbSet<RecipientsToMessages> RecipientsToMessages { get; set; }

    }
}
