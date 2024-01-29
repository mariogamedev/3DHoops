using UnityEngine;

namespace MVC
{
    public class DependenciesManager : MonoBehaviour
    {
        private IContainer _container;

        [SerializeField]
        private Installer _installer;

        private void Awake()
        {
            _container = new SimpleContainer();
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            if (_installer != null)
            {
                _installer.SetContainer(_container);
                _installer.Install();
            }

            View[] views = FindObjectsOfType<View>();
            foreach (View view in views)
            {
                view.SetContainer(_container);
                view.SetInstaller(_installer);
            }
        }
    }
}