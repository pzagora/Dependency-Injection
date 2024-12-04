using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Attributes;
using DependencyInjection.Core;

namespace DependencyInjection.EditModeTests
{
    internal class AttributeInjectorTests
    {
        private class Foo
        {
            [Inject] public readonly int InjectedFieldValue;
            [Inject] public int InjectedPropertyValue { get; private set; }
            public int InjectedMethodValue { get; private set; }
            
            
            [Inject]
            private void Inject(int value)
            {
                InjectedMethodValue = value;
            }
        }
        
        [Test]
        public void AddSingleton_ShouldRunAttributeInjectionOnFieldsPropertiesAndMethodsMarkedWithInjectAttribute()
        {
            var container = new ContainerBuilder()
                .AddSingleton(42, typeof(int))
                .AddSingleton(typeof(Foo), typeof(Foo))
                .Build();
            
            var foo = container.Single<Foo>();
            foo.InjectedFieldValue.Should().Be(42);
            foo.InjectedPropertyValue.Should().Be(42);
            foo.InjectedMethodValue.Should().Be(42);
        }
        
        [Test]
        public void AddTransient_ShouldRunAttributeInjectionOnFieldsPropertiesAndMethodsMarkedWithInjectAttribute()
        {
            var container = new ContainerBuilder()
                .AddSingleton(42, typeof(int))
                .AddTransient(typeof(Foo), typeof(Foo))
                .Build();
            
            var foo = container.Single<Foo>();
            foo.InjectedFieldValue.Should().Be(42);
            foo.InjectedPropertyValue.Should().Be(42);
            foo.InjectedMethodValue.Should().Be(42);
        }
    }
}