using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Modules.Inspector;
using Gemini.Modules.Output;
using Obsidian.Studio.ViewModels;
using Obsidian.Studio.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Obsidian.Studio.Modules
{
    [Export(typeof(IModule))]
    public class Main : ModuleBase
    {
        private readonly IOutput output;
        private readonly IInspectorTool inspectorTool;

        public override IEnumerable<Type> DefaultTools
        {
            get { yield return typeof(IInspectorTool); }
        }

        [ImportingConstructor]
        public Main(IOutput output, IInspectorTool inspectorTool)
        {
            this.output = output;
            this.inspectorTool = inspectorTool;
        }

        public override async void Initialize()
        {
            base.Initialize();

            //IoC.Get<StudioWindowViewModel>().Hide();
            //_ = IoC.Get<StartWindowViewModel>().Show();

            Stopwatch timer = new();
            timer.Start();

            List<Task> procedures = new();

            Shell.MainMenu.Clear();
            Shell.ShowFloatingWindowsInTaskbar = true;
            //Shell.ToolBars.Visible = true;

            //MainWindow.WindowState = WindowState.Maximized;
            MainWindow.Title = "Gemini Demo";
            
            Shell.StatusBar.AddItem("Hello world!", new GridLength(1, GridUnitType.Star));
            Shell.StatusBar.AddItem("Ln 44", new GridLength(100));
            Shell.StatusBar.AddItem("Col 79", new GridLength(100));

            output.AppendLine("Started up");

            Shell.ActiveDocumentChanged += (sender, e) => RefreshInspector();
            RefreshInspector();
            timer.Stop();

            Task.WaitAll(procedures.ToArray());

            long timeout = 3000;

            if (timer.ElapsedMilliseconds < timeout) await Task.Delay((int)(timeout - timer.ElapsedMilliseconds));

            //_ = IoC.Get<StudioWindowViewModel>().Show();
            //IoC.Get<StartWindowViewModel>().Hide();
        }

        private void RefreshInspector()
        {
            if (Shell.ActiveItem != null)
                inspectorTool.SelectedObject = new InspectableObjectBuilder()
                    .WithObjectProperties(Shell.ActiveItem, pd => pd.ComponentType == Shell.ActiveItem.GetType())
                    .ToInspectableObject();
            else
                inspectorTool.SelectedObject = null;
        }
    }

    public static class MainExtensions
    {
        public static T GetViewModel<T>(this ModuleBase _, string? key = null) where T : IViewAware => IoC.Get<T>(key);

        public static T GetView<T>(this IViewAware vm, object? ctx = null) where T : UIElement => (T)vm.GetView(ctx);

        public static TView GetView<TViewModel, TView>(this ModuleBase _, string? key = null, object? ctx = null) where TViewModel : IViewAware where TView : UIElement => _.GetViewModel<TViewModel>(key).GetView<TView>(ctx);
    }
}
