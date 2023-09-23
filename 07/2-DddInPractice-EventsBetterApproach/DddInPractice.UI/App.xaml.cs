using DddInPractice.Logic.Utils;
using System.Windows;

namespace DddInPractice.UI;

public partial class App : Application
{
    public App()
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=DddInPractice;Trusted_Connection=True;MultipleActiveResultSets=true";
        Initer.Init(connectionString);
    }
}
