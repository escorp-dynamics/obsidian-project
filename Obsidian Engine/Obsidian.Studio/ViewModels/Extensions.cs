using Caliburn.Micro;
using Fluent;
using Gemini.Framework;

namespace Obsidian.Studio.ViewModels
{
    /// <summary>
    /// Представляет набор методов расширений для View-Model.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Связывает активацию субокна и кнопки переключения.
        /// </summary>
        /// <typeparam name="T">Тип субокна.</typeparam>
        /// <param name="toggle">Кнопка переключения.</param>
        public static void BindTool<T>(this ToggleButton toggle) where T : ITool
        {
            T tool = IoC.Get<T>();

            tool.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(tool.IsVisible))
                    toggle.IsChecked = tool.IsVisible;
            };
        }
    }
}
