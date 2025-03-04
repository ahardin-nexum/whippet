﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.Json
{
    /// <summary>
    /// Provides a converter for <see cref="IMultichannelOrderManagerCountry"/> objects.
    /// </summary>
    public class MultichannelOrderManagerCountryCreationConverter : CustomCreationConverter<IMultichannelOrderManagerCountry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultichannelOrderManagerCountryCreationConverter"/> class with no arguments.
        /// </summary>
        public MultichannelOrderManagerCountryCreationConverter()
            : base()
        { }

        /// <summary>
        /// Creates an instance of <see cref="IMultichannelOrderManagerCountry"/> using the default implementation.
        /// </summary>
        /// <param name="objectType"><see cref="Type"/> of object encountered.</param>
        /// <returns><see cref="IMultichannelOrderManagerCountry"/> object.</returns>
        public override IMultichannelOrderManagerCountry Create(Type objectType)
        {
            return new MultichannelOrderManagerCountry();
        }

        /// <summary>
        /// Determines if the specified <see cref="Type"/> can be converted to an <see cref="IMultichannelOrderManagerCountry"/> instance.
        /// </summary>
        /// <param name="objectType"><see cref="Type"/> of object encountered.</param>
        /// <returns><see langword="true"/> if the object can be cast; otherwise, <see langword="false"/>.</returns>
        public override bool CanConvert(Type objectType)
        {
            MultichannelOrderManagerCountry testObj = null;
            bool canConvert = base.CanConvert(objectType);

            if (canConvert)
            {
                try
                {
                    testObj = Activator.CreateInstance(objectType) as MultichannelOrderManagerCountry;   // attempt to cast the instance which determines if it can be converted
                    canConvert = (testObj != null);
                }
                catch
                {
                    canConvert = false;
                }
            }

            return canConvert;
        }
    }
}
