using MewgenicsModSdk;

namespace MewTour.Abstract;

public abstract class Manager : IInjectable
{
    public abstract void Configure(MewTour main, ModConfig config);
    
    public virtual void LoadDependencies(ILoader loader, ModConfig config)
    {
    }
}