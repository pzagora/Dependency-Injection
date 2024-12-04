using NUnit.Framework;
using DependencyInjection.Configuration;
using UnityEngine;

namespace DependencyInjection.EditModeTests
{
    public class ProjectSetupTests
    {
        [Test]
        public void ProjectCanHaveNoneOrSingleDependencyInjectionSettings()
        {
            var assets = Resources.LoadAll<DependencyInjectionSettings>(string.Empty);

            if (assets.Length is 0 or 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail($"You can have none or a single DependencyInjectionSettings, currently you have {assets.Length}");
            }
        }
    }
}