using System.Collections.Generic;
using MessageApp.WEB.DAL.DTO;

namespace MessageApp.WEB.DAL
{
    public static class DbService
    {
        public static void AddTestData(ApiContext context)
        {
            List<UserDTO> users = new List<UserDTO>()
            {
                new UserDTO() { Name = "Harry", Surname = "Potter" },
                new UserDTO() { Name = "Luke", Surname = "Skywalker" },
                new UserDTO() { Name = "Lord", Surname = "Voldemort" },
                new UserDTO() { Name = "Rimus", Surname = "Lupin" },
                new UserDTO() { Name = "James", Surname = "Potter" },
                new UserDTO() { Name = "Sirius", Surname = "Black" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
