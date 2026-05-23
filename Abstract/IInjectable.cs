namespace MewTour.Abstract;

public interface IInjectable
{
    public void LoadDependencies(ILoader loader);
}