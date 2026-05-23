namespace MewTour.Abstract;

public interface ILoader
{
    public T Get<T>() where T : Manager;
}