using Caliburn.Micro;
using Fluent;
using Gemini.Framework;

namespace Obsidian.Studio.ViewModels
{
    public static class Extensions
    {
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
