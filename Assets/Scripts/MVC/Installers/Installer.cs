using UnityEngine;

namespace MVC
{
    public abstract class Installer : ScriptableObject
    {
        protected IContainer _container;

        public abstract void Install();

        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        protected void InstallAction<TController, TAction>()
           where TController : Controller, new()
           where TAction : Signal, new()
        {
            Controller controller = _container.Resolve<TController>() ?? new TController();
            Signal action = _container.Resolve<TAction>() ?? new TAction();

            controller.InstallAction(action);

            _container.Register<TAction>(action);
        }

        protected void InstallAction<TController, TAction, TObject>()
            where TController : Controller<TObject>, new()
            where TAction : Signal<TObject>, new()          
        {
            Controller<TObject> controller = _container.Resolve<TController>() ?? new TController();
            Signal<TObject> action = _container.Resolve<TAction>() ?? new TAction();

            controller.InstallAction(action);

            _container.Register<TAction>(action);
        }

        public void InvokeNotification<T>(Signal<T> signal, T data)
        {
            signal.Invoke(data);
        }

        public void InvokeNotification(Signal signal)
        {
            signal.Invoke();
        }
    }
}