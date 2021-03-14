using Obsidian.Studio.Views;
using System;
using System.Windows;

namespace Obsidian.Studio
{
    /// <summary>
    /// Представляет базовый класс приложения.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Загрузочный экран.
        /// </summary>
        public StartWindowView StartWindow { get; protected set; }

        /// <summary>
        /// Экземпляр <see cref="Application"/> для текущего <see cref="AppDomain"/>.
        /// </summary>
        public static new App Current { get; } = (App)Application.Current;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="App"/>.
        /// </summary>
        public App()
        {
            StartWindow = new StartWindowView();
            StartWindow.Show();
        }
    }
}
