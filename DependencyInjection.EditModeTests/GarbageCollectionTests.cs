﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Core;
using UnityEngine;

namespace DependencyInjection.EditModeTests
{
    internal class GarbageCollectionTests
    {
        private class Service
        {
        }

        public static void ForceGarbageCollection()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [Test, Retry(3)]
        public void Singleton_ShouldBeFinalized_WhenOwnerIsDisposed()
        {
            var references = new List<WeakReference>();

            void Act()
            {
                var container = new ContainerBuilder().AddSingleton(typeof(Service), typeof(Service)).Build();
                var service = container.Single<Service>();
                references.Add(new WeakReference(service));
                container.Dispose();
            }

            Act();
            ForceGarbageCollection();
            references.Any(r => r.IsAlive).Should().BeFalse();
        }

        [Test, Retry(3)]
        public void DisposedScopedContainer_ShouldHaveNoReferencesToItself_AndShouldBeCollectedAndFinalized()
        {
            var references = new List<WeakReference>();

            void Act()
            {
                var parent = new ContainerBuilder().Build();
                var scoped = parent.Scope();
                references.Add(new WeakReference(scoped));
                scoped.Dispose();
            }

            Act();
            ForceGarbageCollection();
            references.Any(r => r.IsAlive).Should().BeFalse();
        }

        [Test, Retry(3)]
        public void Construct_ContainerShouldNotControlConstructedObjectLifeCycle_ByNotKeepingReferenceToIt()
        {
            var references = new List<WeakReference>();
            var container = new ContainerBuilder().Build();

            void Act()
            {
                var service = container.Construct<Service>();
                references.Add(new WeakReference(service));
            }

            Act();
            ForceGarbageCollection();
            references.Any(r => r.IsAlive).Should().BeFalse();
        }
    }
}