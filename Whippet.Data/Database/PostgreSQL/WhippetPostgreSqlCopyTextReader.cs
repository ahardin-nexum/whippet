using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Reader for a text export in PostgreSQL. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSqlCopyTextReader : StreamReader, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlCopyTextReader"/> object.
        /// </summary>
        private NpgsqlCopyTextReader InternalReader
        { get; set; }
        
        /// <summary>
        /// Gets the underlying stream that interfaces with a backing store. This property is read-only.
        /// </summary>
        public override Stream BaseStream
        {
            get
            {
                return InternalReader.BaseStream;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Text.Encoding"/> in which the output is written. This property is read-only.
        /// </summary>
        public override Encoding CurrentEncoding
        {
            get
            {
                return InternalReader.CurrentEncoding;
            }
        }

        /// <summary>
        /// Indicates whether the current stream position is at the end of the stream. This property is read-only.
        /// </summary>
        public new bool EndOfStream
        {
            get
            {
                return InternalReader.EndOfStream;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlCopyTextReader"/> class with no arguments.
        /// </summary>
        private WhippetPostgreSqlCopyTextReader()
            : base(Stream.Null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlCopyTextReader"/> class with the specified <see cref="NpgsqlCopyTextReader"/> object.
        /// </summary>
        /// <param name="reader"><see cref="NpgsqlCopyTextReader"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSqlCopyTextReader(NpgsqlCopyTextReader reader)
            : this()
        {
            ArgumentNullException.ThrowIfNull(reader);
            InternalReader = reader;
        }

        /// <summary>
        /// Closes the <see cref="WhippetPostgreSqlCopyTextReader"/> object and the underlying stream and releases any system resources associated with the reader.
        /// </summary>
        public override void Close()
        {
            InternalReader.Close();
        }

        /// <summary>
        /// Clears the internal buffer.
        /// </summary>
        public new void DiscardBufferedData()
        {
            InternalReader.DiscardBufferedData();
        }

        /// <summary>
        /// Disposes of the current object and releases its resources from memory.
        /// </summary>
        public new void Dispose()
        {
            InternalReader.Dispose();
        }

        /// <summary>
        /// Asynchronously disposes of the current object and releases its resources from memory.
        /// </summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public new ValueTask DisposeAsync()
        {
            return InternalReader.DisposeAsync();
        }

        /// <summary>
        /// Returns the next available character but does not consume it.
        /// </summary>
        /// <returns>An integer representing the next character to be read or -1 if there are no characters left to be read or if the stream does not support seeking.</returns>
        /// <exception cref="IOException"></exception>
        public override int Peek()
        {
            return InternalReader.Peek();
        }

        /// <summary>
        /// Reads the next character from the input stream and advances the character position by one character.
        /// </summary>
        /// <returns>The next character from the input stream represented as an <see cref="Int32"/> object, or -1 if no more characters are available.</returns>
        /// <exception cref="IOException"></exception>
        public override int Read()
        {
            return InternalReader.Read();
        }

        /// <summary>
        /// Reads the characters from the current stream into a span.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified span of characters replaced by the characters read from the current source.</param>
        /// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="buffer"/> length, depending on whether the data is available within the stream.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        public override int Read(Span<char> buffer)
        {
            return InternalReader.Read(buffer);
        }

        /// <summary>
        /// Reads a specified maximum of characters from the current stream into a buffer beginning at the specified index.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index"/> and (<paramref name="index"/> + <paramref name="count"/> - 1) replaced by the characters read from the current source.</param>
        /// <param name="index">The index of <paramref name="buffer"/> at which to begin writing.</param>
        /// <param name="count">The maximum number of characters to read.</param>
        /// <returns>The number of characters that have been read, or zero (0) if at the end of the stream and no data was read. The number will be less than or equal to the count parameter, depending on whether the data is available within the stream.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="IOException"></exception>
        public override int Read(char[] buffer, int index, int count)
        {
            return InternalReader.Read(buffer, index, count);
        }

        /// <summary>
        /// Asynchronously reads the characters from the current stream into a memory block.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified memory block of characters replaced by the characters read from the current source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A value task that represents the asynchronous read operation. The value of the type parameter of the value task contains the number of characters that have been read, or zero (0) if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="buffer"/> length, depending on whether the data is available within the stream.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = new CancellationToken())
        {
            return InternalReader.ReadAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index"/> and (<paramref name="index"/> + <paramref name="count"/> - 1) replaced by the characters read from the current source.</param>
        /// <param name="index">The position in <paramref name="buffer"/> at which to begin writing.</param>
        /// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the current method returns.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{TResult}"/> parameter contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override Task<int> ReadAsync(char[] buffer, int index, int count)
        {
            return InternalReader.ReadAsync(buffer, index, count);
        }

        /// <summary>
        /// Reads a specified maximum number of characters from the current stream and writes the data to a buffer, beginning at the specified index.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index"/> and (<paramref name="index"/> + <paramref name="count"/> - 1) replaced by the characters read from the current source.</param>
        /// <param name="index">The position in <paramref name="buffer"/> at which to begin writing.</param>
        /// <param name="count">The maximum number of characters to read.</param>
        /// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count"/>, depending on whether all input characters have been read.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return InternalReader.ReadBlock(buffer, index, count);
        }

        /// <summary>
        /// Reads the characters from the current stream and writes the data to a buffer.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified span of characters replaced by the characters read from the current source.</param>
        /// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="buffer"/> length, depending on whether all input characters have been read.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        public override int ReadBlock(Span<char> buffer)
        {
            return InternalReader.ReadBlock(buffer);
        }

        /// <summary>
        /// Asynchronously reads the characters from the current stream and writes the data to a buffer.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified memory block of characters replaced by the characters read from the current source.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A value task that represents the asynchronous read operation. The value of the type parameter of the value task contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default)
        {
            return InternalReader.ReadBlockAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Reads a line of characters from the current stream and returns the data as a string.
        /// </summary>
        /// <returns>The next line from the input stream or <see langword="null"/> if the end of the input stream is reached.</returns>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="IOException"></exception>
        public override string ReadLine()
        {
            return InternalReader.ReadLine();
        }

        /// <summary>
        /// Reads a line of characters asynchronously from the current stream and returns the data as a string.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{TResult}"/> parameter contains the next line from the stream, or is <see langword="null"/> if all the characters have been read.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<string> ReadLineAsync()
        {
            return await InternalReader.ReadLineAsync();
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream.
        /// </summary>
        /// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="IOException"></exception>
        public override string ReadToEnd()
        {
            return InternalReader.ReadToEnd();
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{TResult}"/> parameter contains a string with the characters from the current position to the end of the stream.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<string> ReadToEndAsync()
        {
            return await InternalReader.ReadToEndAsync();
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{TResult}"/> parameter contains a string with the characters from the current position to the end of the stream.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task<string> ReadToEndAsync(CancellationToken cancellationToken)
        {
            return InternalReader.ReadToEndAsync(cancellationToken);
        }

        /// <summary>
        /// Cancels and terminates an ongoing export.
        /// </summary>
        public void Cancel()
        {
            InternalReader.Cancel();
        }

        /// <summary>
        /// Asynchronously cancels and terminates an ongoing export.
        /// </summary>
        /// <returns><see cref="Task"/> object.</returns>
        public Task CancelAsync()
        {
            return InternalReader.CancelAsync();
        }
        
        public static implicit operator WhippetPostgreSqlCopyTextReader(NpgsqlCopyTextReader reader)
        {
            return (reader == null) ? null : new WhippetPostgreSqlCopyTextReader(reader);
        }

        public static implicit operator NpgsqlCopyTextReader(WhippetPostgreSqlCopyTextReader reader)
        {
            return (reader == null) ? null : reader.InternalReader;
        }
    }
}
