using KurosawaDia.Services.Interfaces;

namespace KurosawaDia.Services;

public class ActivatorService
{
    private readonly IEnumerable<IAutoStartService> _services;

    public ActivatorService(IEnumerable<IAutoStartService> services)
    {
        _services = services;
    }

    public void ActiveAllServices()
    {
        foreach (var service in _services)
        {
            service.Activate();
        }
    }
}
