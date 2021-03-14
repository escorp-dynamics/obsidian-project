using Gemini.Framework.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Obsidian.Studio
{
    /// <summary>
    /// Загрузчик приложения.
    /// </summary>
    internal class AppBootstrapper : Gemini.AppBootstrapper
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AppBootstrapper"/>.
        /// </summary>
        public AppBootstrapper() : base() { }

        /// <summary>
        /// Происходит в момент запуска приложения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
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
