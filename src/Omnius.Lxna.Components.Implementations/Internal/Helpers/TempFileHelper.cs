using System;
using System.IO;
using Omnius.Core.Helpers;
using Omnius.Core.Serialization;
using Omnius.Core.Serialization.Extensions;

namespace Omnius.Lxna.Components.Internal.Helpers
{
    internal static unsafe class TempFileHelper
    {
        private static readonly Random _random = new();
        private static readonly Lazy<Base16> _base16 = new(() => new Base16(), true);

        public static FileStream GenFileStream(string destinationDirectoryPath, string extension)
        {
            var buffer = new byte[32];
            int loopCount = 0;

            for (; ; )
            {
                _random.NextBytes(buffer);
                var randomText = _base16.Value.BytesToString(buffer);
                var tempFilePath = Path.Combine(destinationDirectoryPath, randomText + extension);

                try
                {
                    DirectoryHelper.CreateDirectory(destinationDirectoryPath);
                    var stream = new FileStream(tempFilePath, FileMode.CreateNew);
                    return stream;
                }
                catch (IOException)
                {
                    if (++loopCount >= 100) throw;
                }
            }
        }
    }
}
