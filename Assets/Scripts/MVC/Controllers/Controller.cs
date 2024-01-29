
namespace MVC
{
    public abstract class Controller
    {
        protected abstract void Execute();

        public void InstallAction(Signal action)
        {
            action.Action += Execute;
        }
    }

    public abstract class Controller<T>
    {
        protected abstract void Execute(T input);

        public void InstallAction(Signal<T> action)
        {
            action.Action += Execute;
        }
    }
}