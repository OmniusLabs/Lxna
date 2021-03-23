using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Omnius.Core;

namespace Omnius.Lxna.Components
{
    public interface IArchiveFileExtractorFactory
    {
        ValueTask<IArchiveFileExtractor> CreateAsync(ArchiveFileExtractorOptions options, CancellationToken cancellationToken = default);
    }

    public record ArchiveFileExtractorOptions
    {
        public ArchiveFileExtractorOptions(string archiveFilePath, IBytesPool bytesPool)
        {
            this.ArchiveFilePath = archiveFilePath;
            this.BytesPool = bytesPool;
        }

        public string ArchiveFilePath { get; }

        public IBytesPool BytesPool { get; }
    }

    public interface IArchiveFileExtractor : IDisposable
    {
        ValueTask<bool> ExistsFileAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<bool> ExistsDirectoryAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<DateTime> GetFileLastWriteTimeAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<IEnumerable<string>> FindDirectoriesAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<IEnumerable<string>> FindArchiveFilesAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<IEnumerable<string>> FindFilesAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<Stream> GetFileStreamAsync(string path, CancellationToken cancellationToken = default);

        ValueTask<long> GetFileSizeAsync(string path, CancellationToken cancellationToken = default);

        ValueTask ExtractFileAsync(string path, Stream stream, CancellationToken cancellationToken = default);
    }
}
