using System;

namespace MVC
{
    public abstract class Signal<T>
    {
        public event Action<T> Action = delegate { };

        public void Invoke(T data)
        {
            Action.Invoke(data);
        }
    }

    public abstract class Signal
    {
        public event Action Action = delegate { };

        public void Invoke()
        {
            Action.Invoke();
        }
    }
}