using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Omnius.Core;
using Omnius.Lxna.Ui.Desktop.Resources;
using Omnius.Lxna.Ui.Desktop.Views.Primitives;
using Omnius.Lxna.Ui.Desktop.Views.Windows.Main.FileView;

namespace Omnius.Lxna.Ui.Desktop.Views.Windows.Main
{
    public class MainWindow : StatefulWindowBase
    {
        private AppState? _state;

        public MainWindow()
            : base()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override async ValueTask OnInitialize()
        {
            var stateDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../state/ui-desktop");
            var temporaryDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../temp/ui-desktop");
            var bytesPool = BytesPool.Shared;

            ClearDirectory(temporaryDirectoryPath);

            _state = await AppState.Factory.CreateAsync(stateDirectoryPath, temporaryDirectoryPath, bytesPool);

            this.Model = new MainWindowModel(_state);
            this.FileViewControl.Model = new FileViewControlModel(_state);
        }

        private static void ClearDirectory(string path)
        {
            var di = new DirectoryInfo(path);

            foreach (var file in di.EnumerateFiles())
            {
                file.Delete();
            }

            foreach (var dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }

        protected override async ValueTask OnDispose()
        {
            if (this.FileViewControl.Model is FileViewControlModel fileViewControlModel)
            {
                await fileViewControlModel.DisposeAsync();
            }

            if (this.Model is MainWindowModel mainWindowModel)
            {
                await mainWindowModel.DisposeAsync();
            }

            if (_state is not null)
            {
                await _state.DisposeAsync();
            }
        }

        public MainWindowModel? Model
        {
            get => this.DataContext as MainWindowModel;
            set => this.DataContext = value;
        }

        public FileViewControl FileViewControl => this.FindControl<FileViewControl>(nameof(this.FileViewControl));
    }
}
