using System;
using System.IO;
using System.Threading;

namespace Omnius.Lxna.Components.Internal
{
    public class CancellableStream : Stream
    {
        private Stream _stream;
        private readonly CancellationToken _cancellationToken;
        private bool _leaveInnerStreamOpen;
        private bool _isDisposed;

        public CancellableStream(Stream stream, CancellationToken cancellationToken, bool leaveInnerStreamOpen)
        {
            _stream = stream;
            _cancellationToken = cancellationToken;
            _leaveInnerStreamOpen = leaveInnerStreamOpen;
        }

        public override bool CanRead
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                return _stream.CanRead;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                return _stream.CanWrite;
            }
        }

        public override bool CanSeek
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                return _stream.CanSeek;
            }
        }

        public override long Position
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                return _stream.Position;
            }

            set
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                _stream.Position = value;
            }
        }

        public override long Length
        {
            get
            {
                if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

                return _stream.Length;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            _cancellationToken.ThrowIfCancellationRequested();
            return _stream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            _cancellationToken.ThrowIfCancellationRequested();
            _stream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            if (_isDisposed) throw new ObjectDisposedException(this.GetType().FullName);

            _stream.Flush();
        }

        public override void Close()
        {
            if (_isDisposed) return;

            base.Close();
        }

        protected override void Dispose(bool isDisposing)
        {
            try
            {
                if (_isDisposed) return;
                _isDisposed = true;

                if (isDisposing)
                {
                    if (!_leaveInnerStreamOpen)
                    {
                        _stream?.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(isDisposing);
            }
        }
    }
}
