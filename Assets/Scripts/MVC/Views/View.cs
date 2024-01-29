using UnityEngine;

namespace MVC
{
    public abstract class View : MonoBehaviour
    {
        private IContainer _container;
        private Installer _installer;

        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        public void SetInstaller(Installer installer)
        {
            _installer = installer;
        }

        protected void InvokeAction<TAction>()
            where TAction : Signal, new()
        {
            TAction action = _container.Resolve<TAction>() ?? new TAction();
            _installer.InvokeNotification(action);
        }

        protected void InvokeAction<TAction, TObject>(TObject data)
            where TAction : Signal<TObject>, new()
        {
            TAction action = _container.Resolve<TAction>() ?? new TAction();
            _installer.InvokeNotification(action, data);
        }
    }
}