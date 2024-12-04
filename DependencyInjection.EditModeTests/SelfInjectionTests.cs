using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Core;

namespace DependencyInjection.EditModeTests
{
    internal class SelfInjectionTests
    {
        [Test]
        public void Container_ShouldBeAbleToResolveItself()
        {
            var container = new ContainerBuilder().Build();
            container.Single<Container>().Should().Be(container);
        }
    }
}