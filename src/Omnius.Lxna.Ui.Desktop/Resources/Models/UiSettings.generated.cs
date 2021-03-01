// <auto-generated/>
#nullable enable
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Omnius.Lxna.Ui.Desktop.Resources.Models
{
    public sealed partial class UiSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _thumbnail_Width;

        public int Thumbnail_Width
        {
            get
            {
                return _thumbnail_Width;
            }
            set
            {
                if (_thumbnail_Width != value)
                {
                    _thumbnail_Width = value;
                    this.RaisePropertyChanged(nameof(Thumbnail_Width));
                }
            }
        }
        private int _thumbnail_Height;

        public int Thumbnail_Height
        {
            get
            {
                return _thumbnail_Height;
            }
            set
            {
                if (_thumbnail_Height != value)
                {
                    _thumbnail_Height = value;
                    this.RaisePropertyChanged(nameof(Thumbnail_Height));
                }
            }
        }
        private double _FileView_TreeViewWidth;

        public double FileView_TreeViewWidth
        {
            get
            {
                return _FileView_TreeViewWidth;
            }
            set
            {
                if (_FileView_TreeViewWidth != value)
                {
                    _FileView_TreeViewWidth = value;
                    this.RaisePropertyChanged(nameof(FileView_TreeViewWidth));
                }
            }
        }
    }
}