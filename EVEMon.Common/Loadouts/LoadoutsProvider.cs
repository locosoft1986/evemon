﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EVEMon.Common.Data;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;

namespace EVEMon.Common.Loadouts
{
    public abstract class LoadoutsProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the topic URL.
        /// </summary>
        /// <value>
        /// The topic URL.
        /// </value>
        public abstract Uri TopicUrl { get; }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public static IEnumerable<LoadoutsProvider> Providers
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(type => typeof(LoadoutsProvider).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null)
                    .Select(type => Activator.CreateInstance(type) as LoadoutsProvider)
                    .Where(provider => !String.IsNullOrWhiteSpace(provider.Name))
                    .OrderBy(provider => provider.Name);
            }
        }

        /// <summary>
        /// Gets the loadouts feed asynchronous.
        /// </summary>
        /// <param name="ship">The ship.</param>
        public abstract void GetLoadoutsFeedAsync(Item ship);

        /// <summary>
        /// Gets the loadout by identifier asynchronous.
        /// </summary>
        /// <param name="id">The id.</param>
        public abstract void GetLoadoutByIDAsync(long id);

        /// <summary>
        /// Deserializes the loadout.
        /// </summary>
        /// <param name="loadout">The loadout.</param>
        /// <param name="feed">The feed.</param>
        public abstract void DeserializeLoadout(Loadout loadout, object feed);

        /// <summary>
        /// Deserializes the loadouts feed.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <param name="feed">The feed.</param>
        public abstract ILoadoutInfo DeserializeLoadoutsFeed(Item ship, object feed);
    }
}