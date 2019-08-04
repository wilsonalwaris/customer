using Microsoft.Extensions.DependencyInjection;

namespace Api.Register
{
    public interface IDependencyRegister
    {
        void LoadServices(IServiceCollection serviceCollection);
    }
}