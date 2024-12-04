using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using DependencyInjection.Extensions;

namespace DependencyInjection.EditModeTests
{
    internal class TypeExtensionsTests
    {
        [Test]
        public void IsEnumerable_ReturnsTrue_ForGenericIEnumerableDefinition()
        {
            typeof(IEnumerable<int>).IsEnumerable(out _).Should().BeTrue();
        }

        [Test]
        public void IsEnumerable_ShouldReturnFalse_ForAnythingElse()
        {
            typeof(Dictionary<int, string>).IsEnumerable(out _).Should().BeFalse();
        }
    }
}