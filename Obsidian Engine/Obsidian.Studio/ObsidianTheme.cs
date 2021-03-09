using Gemini.Framework.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Obsidian.Studio
{
    [Export(typeof(ITheme))]
    public class ObsidianTheme : DarkTheme
    {
        public override string Name => Properties.Resources.ThemeObsidianName;

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
