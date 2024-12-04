using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Attributes;
using DependencyInjection.Core;

namespace DependencyInjection.EditModeTests
{
    public class DependencyInjectionConstructorAttributeTests
    {
        private class ClassWithDependencyInjectionConstructorIdentifiedByMaxAmountOfArguments
        {
            public bool ConstructedWithEmptyConstructor { get; }
            public bool ConstructedWithNonEmptyConstructor { get; }
            
            public ClassWithDependencyInjectionConstructorIdentifiedByMaxAmountOfArguments()
            {
                ConstructedWithEmptyConstructor = true;
            }
            
            public ClassWithDependencyInjectionConstructorIdentifiedByMaxAmountOfArguments(object a, int b)
            {
                ConstructedWithNonEmptyConstructor = true;
            }
        }

        private class ClassWithDependencyInjectionConstructorIdentifiedByDependencyInjectionConstructorAttribute
        {
            public bool ConstructedWithEmptyConstructor { get; }
            public bool ConstructedWithNonEmptyConstructor { get; }
            
            [DependencyInjectionConstructor]
            public ClassWithDependencyInjectionConstructorIdentifiedByDependencyInjectionConstructorAttribute()
            {
                ConstructedWithEmptyConstructor = true;
            }
            
            public ClassWithDependencyInjectionConstructorIdentifiedByDependencyInjectionConstructorAttribute(object a, int b)
            {
                ConstructedWithNonEmptyConstructor = true;
            }
        }

        [Test]
        public void ClassWithMultipleConstructors_WithoutAnyDependencyInjectionConstructorAttribute_ShouldBeConstructedUsing_ConstructorWithMostArguments()
        {
            var container = new ContainerBuilder()
                .AddSingleton(new object())
                .AddSingleton(42)
                .Build();

            var result = container.Construct<ClassWithDependencyInjectionConstructorIdentifiedByMaxAmountOfArguments>();
            result.ConstructedWithEmptyConstructor.Should().BeFalse();
            result.ConstructedWithNonEmptyConstructor.Should().BeTrue();
        }
        
        [Test]
        public void ClassWithMultipleConstructors_WithOneDefiningDependencyInjectionConstructorAttribute_ShouldBeConstructedUsing_ConstructorWithDependencyInjectionConstructorAttribute()
        {
            var container = new ContainerBuilder()
                .AddSingleton(new object())
                .AddSingleton(42)
                .Build();

            var result = container.Construct<ClassWithDependencyInjectionConstructorIdentifiedByDependencyInjectionConstructorAttribute>();
            result.ConstructedWithEmptyConstructor.Should().BeTrue();
            result.ConstructedWithNonEmptyConstructor.Should().BeFalse();
        }
    }
}