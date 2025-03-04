﻿using System;
using Athi.Whippet.Data;

namespace Athi.Whippet.Logging.Serilog
{
    /// <summary>
    /// Represents a log entry generated by Serilog.
    /// </summary>
    public interface ISerilogLogEntry : IWhippetEntity, IEqualityComparer<ISerilogLogEntry>
    {
        /// <summary>
        /// Gets or sets the unique ID of the <see cref="SerilogLogEntry"/>.
        /// </summary>
        new int ID
        { get; set; }

        /// <summary>
        /// Gets or sets the log message that was generated.
        /// </summary>
        string Message
        { get; set; }

        /// <summary>
        /// Gets or sets the message template used by Serilog to enrich the message.
        /// </summary>
        string MessageTemplate
        { get; set; }

        /// <summary>
        /// Gets or sets the severity level of the log entry.
        /// </summary>
        SerilogLevel? Level
        { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the log. All entries are respective to the date/time of the server the event was captured on.
        /// </summary>
        DateTime? TimeStamp
        { get; set; }

        /// <summary>
        /// Gets or sets the exception message that was captured.
        /// </summary>
        string Exception
        { get; set; }

        /// <summary>
        /// Gets or sets an XML document that contains the properties that are mapped to <see cref="MessageTemplate"/>.
        /// </summary>
        string Properties
        { get; set; }
    }
}

