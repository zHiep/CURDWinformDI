using CURDWinformDI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CURDWinformDI
{
    internal static class Program
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo ServiceCollection
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            string connectionString = "Data Source=DESKTOP-NHH\\SQLEXPRESS;Initial Catalog=CURD;Integrated Security=True";
            services.AddSingleton<ICustomerService>(provider => new CustomerService(connectionString));
            services.AddSingleton<MainForm>();
        }
    }
}
