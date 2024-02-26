using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Provides an API for a raw binary <b>COPY</b> operation, a high-performance data import/export mechanism to a PostgreSQL table. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSQLRawCopyStream : Stream, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlRawCopyStream"/> object.
        /// </summary>
        private NpgsqlRawCopyStream InternalStream
        { get; set; }

        /// <summary>
        /// Indicates whether the current stream supports reading. This property is read-only.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return InternalStream.CanRead;
            }
        }

        /// <summary>
        /// Indicates whether the current stream supports seeking. This property is read-only.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return InternalStream.CanSeek;
            }
        }

        /// <summary>
        /// Indicates whether the current stream can time out. This property is read-only.
        /// </summary>
        public override bool CanTimeout
        {
            get
            {
                return InternalStream.CanTimeout;
            }
        }

        /// <summary>
        /// Indicates whether the current stream supports writing. This property is read-only.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return InternalStream.CanWrite;
            }
        }

        /// <summary>
        /// Gets the length (in bytes) of the stream. This property is read-only.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override long Length
        {
            get
            {
                return InternalStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override long Position
        {
            get
            {
                return InternalStream.Position;
            }
            set
            {
                InternalStream.Position = value;
            }
        }

        /// <summary>
        /// Gets or sets the length (in milliseconds) of time a stream will attempt to read before timing out.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public override int ReadTimeout
        {
            get
            {
                return InternalStream.ReadTimeout;
            }
            set
            {
                InternalStream.ReadTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the length (in milliseconds) of time a stream will attempt to write before timing out.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public override int WriteTimeout
        {
            get
            {
                return InternalStream.WriteTimeout;
            }
            set
            {
                InternalStream.WriteTimeout = value;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSQLRawCopyStream"/> class with no arguments.
        /// </summary>
        private WhippetPostgreSQLRawCopyStream()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSQLRawCopyStream"/> class with the specified <see cref="NpgsqlRawCopyStream"/> object.
        /// </summary>
        /// <param name="stream"><see cref="NpgsqlRawCopyStream"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSQLRawCopyStream(NpgsqlRawCopyStream stream)
            : this()
        {
            ArgumentNullException.ThrowIfNull(stream);
            InternalStream = stream;
        }

        /// <summary>
        /// Begins an asynchronous read operation.
        /// </summary>
        /// <param name="buffer">The buffer to read the data into.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which to begin writing data read from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="callback">An optional asynchronous callback that is called when the read is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
        /// <returns>An <see cref="IAsyncResult"/> that represents the asynchronous read, which could still be pending.</returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return InternalStream.BeginRead(buffer, offset, count, callback, state);
        }

        /// <summary>
        /// Begins an asynchronous write operation.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> from which to begin writing.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="callback">An optional asynchronous callback that is called when the write is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <returns>An <see cref="IAsyncResult"/> that represents the asynchronous write, which could still be pending.</returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return InternalStream.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed. 
        /// </summary>
        public override void Close()
        {
            InternalStream.Close();
        }

        /// <summary>
        /// Reads the bytes from the current stream and writes them to another stream. Both streams positions are advanced by the number of bytes copied.
        /// </summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        public new void CopyTo(Stream destination)
        {
            InternalStream.CopyTo(destination);
        }

        /// <summary>
        /// Reads the bytes from the current stream and writes them to another stream, using a specified buffer size. Both streams positions are advanced by the number of bytes copied.
        /// </summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        /// <param name="bufferSize">The size of the buffer. This value must be greater than zero. The default size is 81920.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        public override void CopyTo(Stream destination, int bufferSize)
        {
            InternalStream.CopyTo(destination, bufferSize);
        }

        /// <summary>
        /// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token. Both streams positions are advanced by the number of bytes copied.
        /// </summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous copy operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return InternalStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified cancellation token. Both streams positions are advanced by the number of bytes copied.
        /// </summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous copy operation.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public new Task CopyToAsync(Stream destination, CancellationToken cancellationToken)
        {
            return InternalStream.CopyToAsync(destination, cancellationToken);
        }

        /// <summary>
        /// Disposes of the object and releases its resources from memory.
        /// </summary>
        public void Dispose()
        {
            InternalStream.Dispose();
        }

        /// <summary>
        /// Asynchronously disposes of the object and releases its resources from memory.
        /// </summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public override ValueTask DisposeAsync()
        {
            return InternalStream.DisposeAsync();
        }

        /// <summary>
        /// Waits for the pending asynchronous read to complete.
        /// </summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
        /// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes requested. <see cref="ReadAsync"/> returns zero (0) only if zero bytes were requested or if no more bytes will be available because it's at the end of the stream; otherwise, read operations do not complete until at least one byte is available. If zero bytes are requested, read operations may complete immediately or may not complete until at least one byte is available (but without consuming any data).</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IOException"></exception>
        public override int EndRead(IAsyncResult asyncResult)
        {
            return InternalStream.EndRead(asyncResult);
        }

        /// <summary>
        /// Ends an asynchronous write operation.
        /// </summary>
        /// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IOException"></exception>
        public override void EndWrite(IAsyncResult asyncResult)
        {
            InternalStream.EndWrite(asyncResult);
        }

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="IOException"></exception>
        public override void Flush()
        {
            InternalStream.Flush();
        }

        /// <summary>
        /// Asynchronously clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <returns>A task that represents the asynchronous flush operation.</returns>
        public new Task FlushAsync()
        {
            return InternalStream.FlushAsync();
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current source.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the size of the buffer if that many bytes are not currently available, or zero (0) if the buffer's length is zero or the end of the stream has been reached.</returns>
        public override int Read(Span<byte> buffer)
        {
            return InternalStream.Read(buffer);
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if <paramref name="count"/> is zero (0) or the end of the stream has been reached.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return InternalStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write the data into.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="ValueTask{T}"/> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number or if the end of the stream has been reached.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return InternalStream.ReadAsync(buffer, cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write the data into.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{T}"/> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if <paramref name="count"/> is zero (0) or if the end of the stream has been reached.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public new Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return InternalStream.ReadAsync(buffer, offset, count);
        }
        
        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write the data into.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of the <see cref="Task{T}"/> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if <paramref name="count"/> is zero (0) or if the end of the stream has been reached.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return InternalStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>
        /// Reads at least a minimum number of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current stream.</param>
        /// <param name="minimumBytes">The minimum number of bytes to read into the buffer.</param>
        /// <param name="throwOnEndOfStream"><see langword="true"/> to throw an exception if the end of the stream is reached before reading <paramref name="minimumBytes"/> of bytes; <see langword="false"/> to return less than <paramref name="minimumBytes"/> when the end of the stream is reached. The default is <see langword="true"/>.</param>
        /// <returns>The total number of bytes read into the buffer. This is guaranteed to be greater than or equal to <paramref name="minimumBytes"/> when <paramref name="throwOnEndOfStream"/> is <see langword="true"/>. This will be less than <paramref name="minimumBytes"/> when the end of the stream is reached and <paramref name="throwOnEndOfStream"/> is <see langword="false"/>. This can be less than the number of bytes allocated in the buffer if that many bytes are not currently available.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="EndOfStreamException"></exception>
        public new int ReadAtLeast(Span<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true)
        {
            return InternalStream.ReadAtLeast(buffer, minimumBytes, throwOnEndOfStream);
        }
        
        /// <summary>
        /// Asynchronously reads at least a minimum number of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current stream.</param>
        /// <param name="minimumBytes">The minimum number of bytes to read into the buffer.</param>
        /// <param name="throwOnEndOfStream"><see langword="true"/> to throw an exception if the end of the stream is reached before reading <paramref name="minimumBytes"/> of bytes; <see langword="false"/> to return less than <paramref name="minimumBytes"/> when the end of the stream is reached. The default is <see langword="true"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous read operation. The value of <see cref="ValueTask{T}"/> contains the total number of bytes read into the buffer. This is guaranteed to be greater than or equal to <paramref name="minimumBytes"/> when <paramref name="throwOnEndOfStream"/> is <see langword="true"/>. This will be less than <paramref name="minimumBytes"/> when the end of the stream is reached and <paramref name="throwOnEndOfStream"/> is <see langword="false"/>. This can be less than the number of bytes allocated in the buffer if that many bytes are not currently available.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="EndOfStreamException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public new ValueTask<int> ReadAtLeastAsync(Memory<byte> buffer, int minimumBytes, bool throwOnEndOfStream = true, CancellationToken cancellationToken = default)
        {
            return InternalStream.ReadAtLeastAsync(buffer, minimumBytes, throwOnEndOfStream, cancellationToken);
        }

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream by one byte or returns -1 if at the end of the stream.
        /// </summary>
        /// <returns>The unsigned byte cast to an <see cref="Int32"/> or -1 if at the end of the stream.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override int ReadByte()
        {
            return InternalStream.ReadByte();
        }

        /// <summary>
        /// Reads bytes from the current stream and advances the position within the stream until the <paramref name="buffer"/> is filled.
        /// </summary>
        /// <param name="buffer">A region of memory. When this method returns, the contents of this region are replaced by the bytes read from the current stream.</param>
        /// <exception cref="EndOfStreamException"></exception>
        public new void ReadExactly(Span<byte> buffer)
        {
            InternalStream.ReadExactly(buffer);
        }

        /// <summary>
        /// Reads <paramref name="count"/> number of bytes from the current stream and advances the position within the stream.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="EndOfStreamException"></exception>
        public new void ReadExactly(byte[] buffer, int offset, int count)
        {
            InternalStream.ReadExactly(buffer, offset, count);
        }

        /// <summary>
        /// Asynchronously reads bytes from the current stream, advances the position within the stream until the <paramref name="buffer"/> is filled, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The buffer to write the data into.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <exception cref="EndOfStreamException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public new ValueTask ReadExactlyAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return InternalStream.ReadExactlyAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads <paramref name="count"/> number of bytes from the current stream and advances the position within the stream.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public new ValueTask ReadExactlyAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default)
        {
            return InternalStream.ReadExactlyAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override void SetLength(long value)
        {
            InternalStream.SetLength(value);
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">A region of memory.</param>
        public override void Write(ReadOnlySpan<byte> buffer)
        {
            InternalStream.Write(buffer);
        }

        /// <summary>
        /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write data from.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            return InternalStream.WriteAsync(buffer, cancellationToken);
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> from which to begin copying bytes to the stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public new Task WriteAsync(byte[] buffer, int offset, int count)
        {
            return InternalStream.WriteAsync(buffer, offset, count);
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> from which to begin copying bytes to the stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return InternalStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>
        /// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
        /// </summary>
        /// <param name="value">The byte to write to the stream.</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public override void WriteByte(byte value)
        {
            InternalStream.WriteByte(value);
        }

        /// <summary>
        /// Cancels and terminates an ongoing operation. Any data already written will be discarded.
        /// </summary>
        public void Cancel()
        {
            InternalStream.Cancel();
        }

        /// <summary>
        /// Asynchronously cancels and terminates an ongoing operation. Any data already written will be discarded.
        /// </summary>
        /// <returns></returns>
        public Task CancelAsync()
        {
            return InternalStream.CancelAsync();
        }

        public static implicit operator WhippetPostgreSQLRawCopyStream(NpgsqlRawCopyStream stream)
        {
            return (stream == null) ? null : new WhippetPostgreSQLRawCopyStream(stream);
        }

        public static implicit operator NpgsqlRawCopyStream(WhippetPostgreSQLRawCopyStream stream)
        {
            return (stream == null) ? null : stream.InternalStream;
        }
    }
}
