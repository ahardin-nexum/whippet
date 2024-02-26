using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Writer for a text import in PostgreSQL. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSQLCopyTextWriter : StreamWriter
    {
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlCopyTextWriter"/> object.
        /// </summary>
        private NpgsqlCopyTextWriter InternalWriter
        { get; set; }

        /// <summary>
        /// Specifies whether the <see cref="WhippetPostgreSQLCopyTextWriter"/> will flush its buffer to the underlying stream after every call to <see cref="Write"/>.
        /// </summary>
        public override bool AutoFlush
        {
            get
            {
                return InternalWriter.AutoFlush;
            }
            set
            {
                InternalWriter.AutoFlush = value;
            }
        }

        /// <summary>
        /// Gets the underlying stream that interfaces with a backing store. This property is read-only.
        /// </summary>
        public override Stream BaseStream
        {
            get
            {
                return InternalWriter.BaseStream;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Text.Encoding"/> in which the output is written. This property is read-only.
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return InternalWriter.Encoding;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSQLCopyTextWriter"/> class with no arguments. 
        /// </summary>
        private WhippetPostgreSQLCopyTextWriter()
            : base(Stream.Null) 
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSQLCopyTextWriter"/> class with the specified <see cref="NpgsqlCopyTextWriter"/> object. 
        /// </summary>
        /// <param name="writer"><see cref="NpgsqlCopyTextWriter"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSQLCopyTextWriter(NpgsqlCopyTextWriter writer)
            : this()
        {
            ArgumentNullException.ThrowIfNull(writer);
            InternalWriter = writer;
        }

        /// <summary>
        /// Closes the current <see cref="WhippetPostgreSQLCopyTextWriter"/> object and the underlying stream.
        /// </summary>
        /// <exception cref="EncoderFallbackException"></exception>
        public override void Close()
        {
            InternalWriter.Close();
        }

        /// <summary>
        /// Disposes of the current object and releases its resources from memory.
        /// </summary>
        public new void Dispose()
        {
            InternalWriter.Dispose();
        }

        /// <summary>
        /// Asynchronously disposes of the current object and releases its resources from memory.
        /// </summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public override ValueTask DisposeAsync()
        {
            return InternalWriter.DisposeAsync();
        }

        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        public override void Flush()
        {
            InternalWriter.Flush();
        }

        /// <summary>
        /// Clears all buffers for this stream asynchronously and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous flush operation.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        public override Task FlushAsync()
        {
            return InternalWriter.FlushAsync();
        }

        /// <summary>
        /// Clears all buffers for this stream asynchronously and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous flush operation.</returns>
        /// 
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return InternalWriter.FlushAsync(cancellationToken);
        }

        /// <summary>
        /// Writes a formatted string to the stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <param name="arg2">The third object to format and write.</param>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            InternalWriter.Write(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Writes a formatted string to the stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        public override void Write(string format, object arg0, object arg1)
        {
            InternalWriter.Write(format, arg0, arg1);
        }

        /// <summary>
        /// Writes a subarray of characters to the stream.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void Write(char[] buffer, int index, int count)
        {
            InternalWriter.Write(buffer, index, count);
        }
    }
}
