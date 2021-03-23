using System;
using System.Collections.Generic;
using Omnius.Lxna.Components;
using Omnius.Lxna.Components.Models;
using Omnius.Lxna.Ui.Desktop.Presenters.Models.Primitives;

namespace Omnius.Lxna.Ui.Desktop.Presenters.Models
{
    public sealed class DirectoryModel : BindableBase
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IFileSystem _fileSystem;
        private readonly Action<DirectoryModel> _expanded;

        public DirectoryModel(NestedPath path, IFileSystem fileSystem, Action<DirectoryModel> expanded)
        {
            this.Path = path;
            if (this.Path == NestedPath.Empty) return;

            _fileSystem = fileSystem;
            _expanded = expanded;

            this.Children = new[] { new DirectoryModel(NestedPath.Empty, _fileSystem, _expanded) };
        }

        private NestedPath _path = NestedPath.Empty;

        public NestedPath Path
        {
            get => _path;
            private set
            {
                var name = string.Empty;
                if (value != NestedPath.Empty) name = value.GetName();

                this.SetProperty(ref _path, value);
                this.Name = name;
            }
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            private set => this.SetProperty(ref _name, value);
        }

        private bool _isExpanded = false;

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                this.SetProperty(ref _isExpanded, value);
                _expanded.Invoke(this);
            }
        }

        private IList<DirectoryModel> _children = Array.Empty<DirectoryModel>();

        public IList<DirectoryModel> Children
        {
            get => _children;
            set => this.SetProperty(ref _children, value);
        }
    }
}
