namespace DependencyInjection.Core
{
    public interface IInstaller
    {
        void InstallBindings(ContainerBuilder containerBuilder);
    }
}