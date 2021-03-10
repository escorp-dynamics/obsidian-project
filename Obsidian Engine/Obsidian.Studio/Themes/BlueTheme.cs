using Caliburn.Micro;
using Gemini.Framework.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Obsidian.Studio.Themes
{
    [Export(typeof(ITheme))]
    public class BlueTheme : Gemini.Framework.Themes.BlueTheme
    {
        public override IEnumerable<Uri> ApplicationResources
        {
            get
            {
                foreach (Uri uri in base.ApplicationResources) yield return uri;

                yield return new Uri("pack://application:,,,/Fluent;component/Themes/Generic.xaml");
                yield return new Uri("pack://application:,,,/Fluent;component/Themes/Themes/Light.Blue.xaml");
            }
        }
    }
}
