﻿using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Attributes;
using DependencyInjection.Core;

namespace DependencyInjection.EditModeTests
{
    public class CustomInjectAttributeTests
    {
        private class CustomInjectAttribute : InjectAttribute
        {
        }

        public class CustomInjectOnField
        {
            [CustomInject] private int _number;
            public int GetNumber() => _number;
        }

        public class CustomInjectOnProperty
        {
            [CustomInject] private int Number { get; set; }
            public int GetNumber() => Number;
        }

        public class CustomInjectOnMethod
        {
            private int _number;

            [CustomInject]
            private void Inject(int number)
            {
                _number = number;
            }

            public int GetNumber() => _number;
        }

        [Test]
        public void CustomInheritorOfInjectAttribute_CanBeUsedToInjectFields()
        {
            using var container = new ContainerBuilder()
                .AddSingleton(42)
                .Build();

            var service = container.Construct<CustomInjectOnField>();
            service.GetNumber().Should().Be(42);
        }

        [Test]
        public void CustomInheritorOfInjectAttribute_CanBeUsedToInjectProperties()
        {
            using var container = new ContainerBuilder()
                .AddSingleton(42)
                .Build();

            var service = container.Construct<CustomInjectOnProperty>();
            service.GetNumber().Should().Be(42);
        }

        [Test]
        public void CustomInheritorOfInjectAttribute_CanBeUsedToInjectMethods()
        {
            using var container = new ContainerBuilder()
                .AddSingleton(42)
                .Build();

            var service = container.Construct<CustomInjectOnMethod>();
            service.GetNumber().Should().Be(42);
        }
    }
}