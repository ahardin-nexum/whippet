﻿using System;
using Athi.Whippet.Data;
using NHibernate;
using Athi.Whippet.Data.NHibernate;
using Athi.Whippet.ServiceManagers;

namespace Athi.Whippet.Installer.Framework.Database.Entities
{
    /// <summary>
    /// Performs a database seed for the appropriate RDBMS. This class must be inherited.
    /// </summary>
    internal class SeedAction : InstallerActionBase, IInstallerAction
    {
        /// <summary>
        /// Indicates whether the action supports asynchronous execution. This property is read-only.
        /// </summary>
        public override bool SupportsAsync
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedAction"/> class with no arguments.
        /// </summary>
        public SeedAction()
            : this(null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedAction"/> class with the specified arguments.
        /// </summary>
        /// <param name="updateProgressPercentage"><see cref="Action{T}"/> that updates the progress percentage of the installer.</param>
        /// <param name="updateStatusAndProgressPercentage"><see cref="Action{T1, T2}"/> that updates the current progress percentage of the task execution.</param>
        public SeedAction(Action<double> updateProgressPercentage, Action<string, double> updateStatusAndProgressPercentage)
            : base("Creating Seed Data", updateProgressPercentage, updateStatusAndProgressPercentage)
        { }

        /// <summary>
        /// Executes the current action with the specified parameters.
        /// </summary>
        /// <param name="args">Parameters to pass to the action (if any).</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> containing the result of the operation.</returns>
        public override WhippetResultContainer<object> Execute(params object[] args)
        {
            SortedList<int, ISeedServiceManager> seeds = null;

            NHibernateConfigurationOptions options = default(NHibernateConfigurationOptions);
            
            WhippetResultContainer<object> result = null;

            List<Exception> errors = null;

            AggregateException aggregateErrors = null;
            
            bool primaryException = false;
            
            VerifyParameters(typeof(NHibernateConfigurationOptions), args);
            VerifyParameters(typeof(SortedList<int, ISeedServiceManager>), args);

            options = (NHibernateConfigurationOptions)(args.First(a => a is NHibernateConfigurationOptions));
            seeds = (SortedList<int, ISeedServiceManager>)(args.First(a => a is SortedList<int, ISeedServiceManager>));

            errors = new List<Exception>();

            try
            {
                foreach (KeyValuePair<int, ISeedServiceManager> seedEntry in seeds)
                {
                    result = new WhippetResultContainer<object>(seedEntry.Value.Seed(UpdateProgress), seedEntry);
                    result.ThrowIfFailed();
                }

                if (result == null)
                {
                    result = new WhippetResultContainer<object>(WhippetResult.Success, null);
                }
            }
            catch (Exception e)
            {
                errors.Add(e);
                primaryException = true;
                result = new WhippetResultContainer<object>(e);
            }
            finally
            {
                if (seeds != null)
                {
                    foreach (KeyValuePair<int, ISeedServiceManager> seedEntry in seeds)
                    {
                        try
                        {
                            seedEntry.Value.Dispose();
                        }
                        catch (Exception disposeException)
                        {
                            errors.Add(disposeException);
                        }
                    }
                }
            }

            if (!result.IsSuccess || errors.Count > 1)
            {
                if ((primaryException && errors.Count > 1) || !primaryException)
                {
                    aggregateErrors = new AggregateException(errors);
                    result = new WhippetResultContainer<object>(aggregateErrors);
                }
            }

            return result;
        }
    }
}
