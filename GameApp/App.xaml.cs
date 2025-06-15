using System.Windows;
using GameApp.Data;
using Microsoft.EntityFrameworkCore;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var context = new AppDbContext())
        {
            // This will create the database or apply migrations on app start
            context.Database.EnsureCreated();
            // Or if you don’t use migrations, use:
            // context.Database.EnsureCreated();
        }

        // Continue starting your app normally
    }
}
