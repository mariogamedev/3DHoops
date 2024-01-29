namespace MVC
{
    public interface IContainer
    {
        T Resolve<T>();
        void Register<TImplementation>(object implementation);
    }
}