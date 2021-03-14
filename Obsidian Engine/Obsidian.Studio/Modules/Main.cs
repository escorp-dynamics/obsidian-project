using Gemini.Framework;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Inspector;
using Gemini.Modules.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;

namespace Obsidian.Studio.Modules
{
    /// <summary>
    /// Представляет главный модуль приложения.
    /// </summary>
    [Export(typeof(IModule))]
    public class Main : ModuleBase
    {
        /// <summary>
        /// Субокно журнала.
        /// </summary>
        public IOutput Output { get; protected set; }

        /// <summary>
        /// Субокно инспектора.
        /// </summary>
        public IInspectorTool Inspector { get; protected set; }

        /// <summary>
        /// Субокно списка ошибок.
        /// </summary>
        public IErrorList ErrorList { get; protected set; }

        /// <summary>
        /// Базовые типы субокон.
        /// </summary>
        public override IEnumerable<Type> DefaultTools
        {
            get
            {
                yield return typeof(IInspectorTool);
                yield return typeof(IOutput);
                yield return typeof(IErrorList);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Main"/>.
        /// </summary>
        /// <param name="output">Субокно журнала.</param>
        /// <param name="inspectorTool">Субокно инспектора.</param>
        [ImportingConstructor]
        public Main(IOutput output, IInspectorTool inspectorTool, IErrorList errorList)
        {
            Output = output;
            Inspector = inspectorTool;
            ErrorList = errorList;
        }

        /// <summary>
        /// Происходит в момент инициализации.
        /// </summary>
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

            Output.AppendLine("Started up");

            Shell.ActiveDocumentChanged += (sender, e) => OnInspectorUpdated();
            OnInspectorUpdated();

            Task.WaitAll(procedures.ToArray());
        }

        /// <summary>
        /// Происходит в момент обновления данных в инспекторе.
        /// </summary>
        protected virtual void OnInspectorUpdated()
        {
            if (Shell.ActiveItem != null)
                Inspector.SelectedObject = new InspectableObjectBuilder()
                    .WithObjectProperties(Shell.ActiveItem, pd => pd.ComponentType == Shell.ActiveItem.GetType())
                    .ToInspectableObject();
            else
                Inspector.SelectedObject = null;
        }
    }
}
