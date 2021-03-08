using System.ComponentModel.Composition;

namespace Obsidian.Studio.ViewModels
{
    [Export(typeof(StartWindowViewModel))]
    public class StartWindowViewModel : BaseViewModel
    {
        public StartWindowViewModel() : base() { }
    }
}
