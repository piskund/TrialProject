using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace UnitTests.Backup.Client.BL.AutoFixture
{
    public class AutoPopulatedMoqPropertiesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(
                new PropertiesPostprocessor(
                    new MockPostprocessor(
                        new MethodInvoker(
                            new MockConstructorQuery()))));
            fixture.ResidueCollectors.Add(
                new Postprocessor(
                    new MockRelay(),
                    new AutoPropertiesCommand(
                        new PropertiesOnlySpecification())));
        }

        private class PropertiesOnlySpecification : IRequestSpecification
        {
            public bool IsSatisfiedBy(object request)
            {
                return request is PropertyInfo;
            }
        }
    }
}