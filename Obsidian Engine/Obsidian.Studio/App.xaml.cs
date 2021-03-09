using Obsidian.Studio.Views;
using System.Windows;

namespace Obsidian.Studio
{
    public partial class App : Application
    {
        public StartWindowView StartWindow { get; protected set; }

        public static new App Current { get; } = (App)Application.Current;

        public App()
        {
            StartWindow = new StartWindowView();
            StartWindow.Show();
        }
    }
}
