using Caliburn.Micro;
using Gemini.Modules.MainWindow.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace Obsidian.Studio.ViewModels
{
    public class BaseViewModel : MainWindowViewModel
    {
        public Task Show() => IoC.Get<IWindowManager>().ShowWindowAsync(this);

        public void Hide() => WindowState = WindowState.Minimized;
    }
}
