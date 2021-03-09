using Caliburn.Micro;
using Gemini.Framework.Services;
using Obsidian.Studio.ViewModels;
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
            await DisplayRootViewFor<StartWindowViewModel>();

            Stopwatch timer = new();
            timer.Start();

            await DisplayRootViewFor<IMainWindow>();

            timer.Stop();
            long timeout = 3000;

            if (timer.ElapsedMilliseconds < timeout) await Task.Delay((int)(timeout - timer.ElapsedMilliseconds));

            await IoC.Get<StartWindowViewModel>().TryCloseAsync();
        }
    }
}
