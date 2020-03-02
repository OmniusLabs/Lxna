using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Omnius.Core;
using Omnius.Core.Data;
using Omnius.Lxna.Service;

namespace Omnius.Lxna.Ui.Desktop.Views.Main
{
    public class MainView : Window
    {
        private MainViewModel _viewModel;

        public MainView(MainViewModel viewModel) : this()
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel;
        }

        public MainView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            
            var itemsRepeater = this.FindControl<ItemsRepeater>("ItemsRepeater");
            itemsRepeater.ElementPrepared += this.ItemsRepeater_ElementPrepared;
            itemsRepeater.ElementIndexChanged += this.ItemsRepeater_ElementIndexChanged;            
            itemsRepeater.ElementClearing += this.ItemsRepeater_ElementClearing;
        }

        private void ItemsRepeater_ElementPrepared(object? sender, ItemsRepeaterElementPreparedEventArgs e)
        {
            _viewModel.NotifyItemPrepared(e.Element.DataContext, e.Index);
        }

        private void ItemsRepeater_ElementIndexChanged(object? sender, ItemsRepeaterElementIndexChangedEventArgs e)
        {
            _viewModel.NotifyItemIndexChanged(e.Element.DataContext, e.OldIndex, e.NewIndex);
        }

        private void ItemsRepeater_ElementClearing(object? sender, ItemsRepeaterElementClearingEventArgs e)
        {
            _viewModel.NotifyItemClearing(e.Element.DataContext);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override async void OnClosed(EventArgs e)
        {
            if (this.DataContext is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync();
            }
        }
    }
}
