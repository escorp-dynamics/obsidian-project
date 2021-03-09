using Gemini.Framework.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Obsidian.Studio
{
    internal class AppBootstrapper : Gemini.AppBootstrapper
    {
        public AppBootstrapper() : base() { }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            Stopwatch timer = new();
            timer.Start();

            await DisplayRootViewFor<IMainWindow>();

            timer.Stop();
            long timeout = 3000;

            if (timer.ElapsedMilliseconds < timeout) await Task.Delay((int)(timeout - timer.ElapsedMilliseconds));

            App.Current.StartWindow.Close();
        }
    }
}
