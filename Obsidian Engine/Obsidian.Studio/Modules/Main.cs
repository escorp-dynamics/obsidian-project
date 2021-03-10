using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Framework.Themes;
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

        public override void Initialize()
        {
            base.Initialize();

            List<Task> procedures = new();

            Shell.MainMenu.Clear();
            Shell.ShowFloatingWindowsInTaskbar = true;
            //Shell.ToolBars.Visible = true;

            //MainWindow.WindowState = WindowState.Maximized;
            MainWindow.Title = "Obsidian Studio 2022";
            
            Shell.StatusBar.AddItem("Hello world!", new GridLength(1, GridUnitType.Star));
            Shell.StatusBar.AddItem("Ln 44", new GridLength(100));
            Shell.StatusBar.AddItem("Col 79", new GridLength(100));

            output.AppendLine("Started up");

            Shell.ActiveDocumentChanged += (sender, e) => RefreshInspector();
            RefreshInspector();

            Task.WaitAll(procedures.ToArray());
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
}
