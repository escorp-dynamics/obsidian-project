using Caliburn.Micro;
using Fluent;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Framework.Themes;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Inspector;
using Gemini.Modules.MainWindow.ViewModels;
using Gemini.Modules.Output;
using Gemini.Modules.Settings.ViewModels;
using MahApps.Metro.Controls;
using Obsidian.Studio.Properties;
using Obsidian.Studio.Views;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Obsidian.Studio.ViewModels
{
    /// <summary>
    /// Представляет View-Model для главного окна приложения.
    /// </summary>
    [Export(typeof(IMainWindow))]
    public class StudioWindowViewModel : MainWindowViewModel
    {
        /// <summary>
        /// Заглавный блок окна.
        /// </summary>
        private RibbonTitleBar? titleBar;

        /// <summary>
        /// Заглавный блок окна.
        /// </summary>
        public RibbonTitleBar? TitleBar
        {
            get => titleBar;

            private set
            {
                titleBar = value;
                NotifyOfPropertyChange(() => TitleBar);
            }
        }

        /// <summary>
        /// Представление окна.
        /// </summary>
        public StudioWindowView? View { get; protected set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StudioWindowViewModel"/>.
        /// </summary>
        public StudioWindowViewModel() : base()
        {
            IThemeManager themeManager = IoC.Get<IThemeManager>();

            for (int i = 0; i < themeManager.Themes.Count; i++)
            {
                Type type = themeManager.Themes[i].GetType();

                if (type == typeof(BlueTheme) || type == typeof(LightTheme) || type == typeof(DarkTheme))
                    themeManager.Themes.Remove(themeManager.Themes[i]);
            }

            themeManager.SetCurrentTheme(Settings.Default.ThemeName);
        }

        /// <summary>
        /// Происходит в момент загрузки представления.
        /// </summary>
        /// <param name="view">Экземпляр представления.</param>
        protected override async void OnViewReady(object view)
        {
            base.OnViewReady(view);
            View = view as StudioWindowView;

            while (TitleBar == null)
            {
                TitleBar = View.FindChild<RibbonTitleBar>("ribbonTitleBar");
                await Task.Delay(10);
            }

            TitleBar.InvalidateArrange();
            TitleBar.UpdateLayout();

            Title = "Test Project 1";

            View!.toggleInspectorTool.BindTool<IInspectorTool>();
            View!.toggleErrorListTool.BindTool<IErrorList>();
            View!.toggleOutputTool.BindTool<IOutput>();

            
        }

        /// <summary>
        /// Происходит в момент активации субокна.
        /// </summary>
        public virtual async void OnGlobalSettingsShow() => await IoC.Get<IWindowManager>().ShowDialogAsync(IoC.Get<SettingsViewModel>());

        /// <summary>
        /// Происходит в момент переключения отображения субокна.
        /// </summary>
        /// <typeparam name="T">Тип субокна.</typeparam>
        public void OnToolToggled<T>() where T : ITool
        {
            T tool = IoC.Get<T>();

            if (tool.IsVisible)
                tool.IsVisible = false;
            else
                Shell.ShowTool<T>();
        }

        /// <summary>
        /// Происходит в момент переключения режима отображения субокна инспектора.
        /// </summary>
        public void OnToolInspectorToggled() => OnToolToggled<IInspectorTool>();

        /// <summary>
        /// Происходит в момент переключения режима отображения субокна списка ошибок.
        /// </summary>
        public void OnToolErrorListToggled() => OnToolToggled<IErrorList>();

        /// <summary>
        /// Происходит в момент переключения режима отображения субокна журнала.
        /// </summary>
        public void OnToolOutputToggled() => OnToolToggled<IOutput>();
    }
}
