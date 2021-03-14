using Gemini.Framework.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Obsidian.Studio.Themes
{
    /// <summary>
    /// Представляет оригинальную тему оформления.
    /// </summary>
    [Export(typeof(ITheme))]
    public class ObsidianTheme : Gemini.Framework.Themes.DarkTheme
    {
        /// <summary>
        /// Название темы оформления.
        /// </summary>
        public override string Name => Properties.Resources.ThemeObsidianName;

        /// <summary>
        /// Возвращает связанные словари ресурсов.
        /// </summary>
        public override IEnumerable<Uri> ApplicationResources
        {
            get
            {
                foreach (Uri uri in base.ApplicationResources) yield return uri;

                yield return new Uri("pack://application:,,,/Fluent;component/Themes/Generic.xaml");
                yield return new Uri("pack://application:,,,/Fluent;component/Themes/Themes/Dark.Steel.xaml");

                yield return new Uri("pack://application:,,,/Resources/Themes/Obsidian/Generic.xaml");
            }
        }
    }
}
