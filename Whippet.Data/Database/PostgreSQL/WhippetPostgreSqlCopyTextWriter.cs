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
    public sealed class WhippetPostgreSqlCopyTextWriter : StreamWriter
    {
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlCopyTextWriter"/> object.
        /// </summary>
        private NpgsqlCopyTextWriter InternalWriter
        { get; set; }

        /// <summary>
        /// Specifies whether the <see cref="WhippetPostgreSqlCopyTextWriter"/> will flush its buffer to the underlying stream after every call to <see cref="Write"/>.
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
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlCopyTextWriter"/> class with no arguments. 
        /// </summary>
        private WhippetPostgreSqlCopyTextWriter()
            : base(Stream.Null) 
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlCopyTextWriter"/> class with the specified <see cref="NpgsqlCopyTextWriter"/> object. 
        /// </summary>
        /// <param name="writer"><see cref="NpgsqlCopyTextWriter"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSqlCopyTextWriter(NpgsqlCopyTextWriter writer)
            : this()
        {
            ArgumentNullException.ThrowIfNull(writer);
            InternalWriter = writer;
        }

        /// <summary>
        /// Closes the current <see cref="WhippetPostgreSqlCopyTextWriter"/> object and the underlying stream.
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
        /// <param name="buffer">A character array that contains the data to write.</param>
        /// <param name="index">The character position in the buffer at which to start reading data.</param>
        /// <param name="count">The maximum number of characters to write.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override void Write(char[] buffer, int index, int count)
        {
            InternalWriter.Write(buffer, index, count);
        }

        /// <summary>
        /// Writes a formatted string to the stream, using the same semantics as the <see cref="String.Format(String, Object[])"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and write.</param>
        public override void Write(string format, params object[] args)
        {
            InternalWriter.Write(format, args);
        }

        /// <summary>
        /// Writes a character array to the stream.
        /// </summary>
        /// <param name="buffer">A character array containing the data to write. If <paramref name="buffer"/> is <see langword="null"/>, nothing is written.</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override void Write(char[] buffer)
        {
            InternalWriter.Write(buffer);
        }

        /// <summary>
        /// Writes a string to the stream.
        /// </summary>
        /// <param name="value">The string to write to the stream. If <paramref name="value"/> is <see langword="null"/>, nothing is written.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        public override void Write(string value)
        {
            InternalWriter.Write(value);
        }

        /// <summary>
        /// Writes a character span to the stream.
        /// </summary>
        /// <param name="buffer">The character span to write.</param>
        public override void Write(ReadOnlySpan<char> buffer)
        {
            InternalWriter.Write(buffer);
        }

        /// <summary>
        /// Writes a character to the stream.
        /// </summary>
        /// <param name="value">The character to write to the stream.</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override void Write(char value)
        {
            InternalWriter.Write(value);
        }

        /// <summary>
        /// Writes a formatted string to the stream, using the same semantics as the <see cref="String.Format(String, Object)"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">An object to format and write.</param>
        public override void Write(string format, object arg0)
        {
            InternalWriter.Write(format, arg0);
        }

        /// <summary>
        /// Asynchronously writes a character to the stream.
        /// </summary>
        /// <param name="value">The character to write to the stream.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteAsync(char value)
        {
            return InternalWriter.WriteAsync(value);
        }

        /// <summary>
        /// Asynchronously writes a string to the stream.
        /// </summary>
        /// <param name="value">The string to write to the stream. If <paramref name="value"/> is <see langword="null"/>, nothing is written.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteAsync(string value)
        {
            return InternalWriter.WriteAsync(value);
        }

        /// <summary>
        /// Asynchronously writes a character memory region to the stream.
        /// </summary>
        /// <param name="buffer">The character memory region to write to the stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            return InternalWriter.WriteAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Asynchronously writes a subarray of characters to the stream.
        /// </summary>
        /// <param name="buffer">A character array that contains the data to write.</param>
        /// <param name="index">The character position in the buffer at which to begin reading data.</param>
        /// <param name="count">The maximum number of characters to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteAsync(char[] buffer, int index, int count)
        {
            return InternalWriter.WriteAsync(buffer, index, count);
        }

        /// <summary>
        /// Writes a formatted string and a new line to the stream, using the same semantics as the <see cref="String.Format(String, Object)"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <param name="arg2">The third object to format and write.</param>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            InternalWriter.WriteLine(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Writes a formatted string and a new line  to the stream, using the same semantics as the <see cref="String.Format(String, Object, Object)"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            InternalWriter.WriteLine(format, arg0, arg1);
        }

        /// <summary>
        /// Writes a formatted string and a new line to the stream, using the same semantics as the <see cref="String.Format(String, Object[])"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and write.</param>
        public override void WriteLine(string format, params object[] args)
        {
            InternalWriter.WriteLine(format, args);
        }

        /// <summary>
        /// Writes a string to the stream followed by a line terminator.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value"/> is <see langword="null"/>, only the line terminator.</param>
        public override void WriteLine(string value)
        {
            InternalWriter.WriteLine(value);
        }

        /// <summary>
        /// Writes the text representation of a character span to the stream followed by a line terminator.
        /// </summary>
        /// <param name="buffer">The character span to write to the stream.</param>
        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            InternalWriter.WriteLine(buffer);
        }

        /// <summary>
        /// Writes a formatted string and a new line to the stream using the same semantics as the <see cref="String.Format(String, Object)"/> method..
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format and write.</param>
        public override void WriteLine(string format, object arg0)
        {
            InternalWriter.WriteLine(format, arg0);
        }

        /// <summary>
        /// Asynchronously writes the text representation of a character memory region to the stream followed by a line terminator.
        /// </summary>
        /// <param name="buffer">The character memory region to write to the stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            return InternalWriter.WriteLineAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Asynchronously writes a line terminator to the stream.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteLineAsync()
        {
            return InternalWriter.WriteLineAsync();
        }

        /// <summary>
        /// Asynchronously writes a character to the stream followed by a line terminator.
        /// </summary>
        /// <param name="value">The character to write to the stream.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteLineAsync(char value)
        {
            return InternalWriter.WriteLineAsync(value);
        }

        /// <summary>
        /// Asynchronously writes a string to the stream followed by a line terminator.
        /// </summary>
        /// <param name="value">The string to write to the stream. If <paramref name="value"/> is <see langword="null"/>, only a line terminator is written.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteLineAsync(string value)
        {
            return InternalWriter.WriteLineAsync(value);
        }

        /// <summary>
        /// Asynchronously writes a subarray of characters to the stream followed by a line terminator.
        /// </summary>
        /// <param name="buffer">A character array that contains the data to write.</param>
        /// <param name="index">The character position in the buffer at which to begin reading data.</param>
        /// <param name="count">The maximum number of characters to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task WriteLineAsync(char[] buffer, int index, int count)
        {
            return InternalWriter.WriteLineAsync(buffer, index, count);
        }

        /// <summary>
        /// Cancels and terminates any ongoing import. Any data already written will be discarded.
        /// </summary>
        public void Cancel()
        {
            InternalWriter.Cancel();
        }

        /// <summary>
        /// Cancels and terminates any ongoing import. Any data already written will be discarded.
        /// </summary>
        /// <returns><see cref="Task"/> object.</returns>
        public Task CancelAsync()
        {
            return InternalWriter.CancelAsync();
        }

        public static implicit operator WhippetPostgreSqlCopyTextWriter(NpgsqlCopyTextWriter writer)
        {
            return (writer == null) ? null : new WhippetPostgreSqlCopyTextWriter(writer);
        }

        public static implicit operator NpgsqlCopyTextWriter(WhippetPostgreSqlCopyTextWriter writer)
        {
            return (writer == null) ? null : writer.InternalWriter;
        }
    }
}
