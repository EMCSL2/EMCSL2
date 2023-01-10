using System.Threading;
using System.Windows;

namespace EMCSL2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex? _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new System.Threading.Mutex(true,
                "EMCSL2");
            if (_mutex.WaitOne(0,
                    false))
            {
                base.OnStartup(e);
            }
            else
            {
                this.Shutdown();
            }
        }
    }
}