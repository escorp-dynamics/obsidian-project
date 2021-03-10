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
    [Export(typeof(IMainWindow))]
    public class StudioWindowViewModel : MainWindowViewModel
    {
        private RibbonTitleBar? titleBar;

        public RibbonTitleBar? TitleBar
        {
            get => titleBar;

            private set
            {
                titleBar = value;
                NotifyOfPropertyChange(() => TitleBar);
            }
        }

        public StudioWindowView? View { get; protected set; }

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

        public virtual async void OnGlobalSettingsShow() => await IoC.Get<IWindowManager>().ShowDialogAsync(IoC.Get<SettingsViewModel>());

        public void OnToolToggled<T>() where T : ITool
        {
            T tool = IoC.Get<T>();

            if (tool.IsVisible)
                tool.IsVisible = false;
            else
                Shell.ShowTool<T>();
        }

        public void OnToolInspectorToggled() => OnToolToggled<IInspectorTool>();

        public void OnToolErrorListToggled() => OnToolToggled<IErrorList>();

        public void OnToolOutputToggled() => OnToolToggled<IOutput>();
    }
}
