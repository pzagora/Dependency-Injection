using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace DependencyInjection.EditModeTests
{
    public class LinkerTests
    {
        [Test]
        public void LinkerFileExist()
        {
            var linker = Resources.Load<TextAsset>("link");
            linker.Should().NotBeNull();
        }
    }
}