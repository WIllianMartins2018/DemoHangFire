using DemoHangFire.Data;
using DemoHangFire.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoHangFire.Jobs
{
    public class Schedule
    {
        private IServiceProvider serviceProvider;

        public Schedule(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void ScheduleJob()
        {
            Console.WriteLine("Iniciando Schedule...");

            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DemoDbContext>();

            List<UserModel> users =  context.Users.Where(u => u.IsChecked == false).ToList();

            if (users.Count != 0)
            {
                foreach (UserModel user in users)
                {
                    user.NameUser = user.NameUser.ToUpper();
                    user.IsChecked = true;

                    context.Update(user);
                }

                _ = context.SaveChangesAsync();

            }

            Console.WriteLine("Encerrando Schedule...");


        }
    }
}
