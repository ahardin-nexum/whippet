using System;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Provides an API for a binary <b>COPY TO</b> operation, a high-performance data export mechanism from a PostgreSQL table. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSqlBinaryExporter : IDisposable, IAsyncDisposable
    {
        private TimeSpan _timeout;
        
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlBinaryExporter"/> object.
        /// </summary>
        private NpgsqlBinaryExporter InternalObject
        { get; set; }

        /// <summary>
        /// Indicates whether the current column is <see langword="null"/>. This property is read-only.
        /// </summary>
        public bool IsNull
        {
            get
            {
                return InternalObject.IsNull;
            }
        }
        
        /// <summary>
        /// Gets or sets the timeout of the operation.
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
                InternalObject.Timeout = value;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlBinaryExporter"/> class with no arguments.
        /// </summary>
        private WhippetPostgreSqlBinaryExporter()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlBinaryExporter"/> class with the specified <see cref="NpgsqlBinaryExporter"/> object.
        /// </summary>
        /// <param name="exporter"><see cref="NpgsqlBinaryExporter"/> object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSqlBinaryExporter(NpgsqlBinaryExporter exporter)
            : this()
        {
            ArgumentNullException.ThrowIfNull(exporter);
            InternalObject = exporter;
        }

        /// <summary>
        /// Starts reading a single row.
        /// </summary>
        /// <returns>The number of columns in the row or -1 if there are no further rows.</returns>
        public int StartRow()
        {
            return InternalObject.StartRow();
        }

        /// <summary>
        /// Asynchronously starts reading a single row.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns><see cref="ValueTask{T}"/> struct.</returns>
        public ValueTask<int> StartRowAsync(CancellationToken cancellationToken = default)
        {
            return InternalObject.StartRowAsync(cancellationToken);
        }

        /// <summary>
        /// Reads the current column and returns its value before moving ahead to the next column. If the column is <see langword="null"/>, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the column to be read. This must correspond to the actual type or data corruption will occur.</typeparam>
        /// <returns>The value of the column.</returns>
        public T Read<T>()
        {
            return InternalObject.Read<T>();
        }

        /// <summary>
        /// Asynchronously reads the current column and returns its value before moving ahead to the next column. If the column is <see langword="null"/>, an exception is thrown.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <typeparam name="T">The type of the column to be read. This must correspond to the actual type or data corruption will occur.</typeparam>
        /// <returns><see cref="ValueTask{T}"/> struct.</returns>
        public ValueTask<T> ReadAsync<T>(CancellationToken cancellationToken = default)
        {
            return InternalObject.ReadAsync<T>(cancellationToken);
        }

        /// <summary>
        /// Reads the current column and returns its value before moving ahead to the next column. If the column is <see langword="null"/>, an exception is thrown.
        /// </summary>
        /// <param name="type">In some cases <typeparamref name="T"/> isn't enough to infer the data type coming in from the database.
        /// This parameter can be used to unambiguously specify the type. An example is the JSONB type, for which <typeparamref name="T"/> will be a
        /// simple string but for which <paramref name="type"/> must be specified as <see cref="NpgsqlDbType.Jsonb"/>.</param>
        /// <typeparam name="T">The .NET type of the column to be read.</typeparam>
        /// <returns>The value of the column.</returns>
        public T Read<T>(NpgsqlDbType type)
        {
            return InternalObject.Read<T>(type);
        }
        
        /// <summary>
        /// Asynchronously reads the current column and returns its value before moving ahead to the next column. If the column is <see langword="null"/>, an exception is thrown.
        /// </summary>
        /// <param name="type">In some cases <typeparamref name="T"/> isn't enough to infer the data type coming in from the database.
        /// This parameter can be used to unambiguously specify the type. An example is the JSONB type, for which <typeparamref name="T"/> will be a
        /// simple string but for which <paramref name="type"/> must be specified as <see cref="NpgsqlDbType.Jsonb"/>.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <typeparam name="T">The .NET type of the column to be read.</typeparam>
        /// <returns><see cref="ValueTask{T}"/> struct.</returns>
        public ValueTask<T> ReadAsync<T>(NpgsqlDbType type, CancellationToken cancellationToken = default)
        {
            return InternalObject.ReadAsync<T>(cancellationToken);
        }

        /// <summary>
        /// Skips the current column without interpreting its value.
        /// </summary>
        public void Skip()
        {
            InternalObject.Skip();
        }

        /// <summary>
        /// Asynchronously skips the current column without interpreting its value.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation.</param>
        /// <returns><see cref="Task"/> object.</returns>
        public Task SkipAsync(CancellationToken cancellationToken = default)
        {
            return InternalObject.SkipAsync(cancellationToken);
        }

        /// <summary>
        /// Cancels an ongoing export.
        /// </summary>
        public void Cancel()
        {
            InternalObject.Cancel();
        }

        /// <summary>
        /// Asynchronously cancels an ongoing export.
        /// </summary>
        /// <returns><see cref="Task"/> object.</returns>
        public Task CancelAsync()
        {
            return InternalObject.CancelAsync();
        }

        /// <summary>
        /// Disposes of the current object and releases its resources from memory.
        /// </summary>
        public void Dispose()
        {
            InternalObject.Dispose();
        }

        /// <summary>
        /// Asynchronously disposes of the current object and releases its resources from memory.
        /// </summary>
        /// <returns><see cref="ValueTask"/> struct.</returns>
        public ValueTask DisposeAsync()
        {
            return InternalObject.DisposeAsync();
        }

        public static implicit operator NpgsqlBinaryExporter(WhippetPostgreSqlBinaryExporter exporter)
        {
            return (exporter == null) ? null : exporter.InternalObject;
        }

        public static implicit operator WhippetPostgreSqlBinaryExporter(NpgsqlBinaryExporter exporter)
        {
            return (exporter == null) ? null : new WhippetPostgreSqlBinaryExporter(exporter);
        }
    }
}
