
using System.Diagnostics;

namespace Baller
{
    public abstract class BaseBallerState : IBallerState
    {
        public BallerStates State;
        protected string AnimationTag;
        protected Baller Baller;

        public BallerStates BallerState => State;

        public virtual void EnterState()
        {
            Debug.WriteLine("EnterState: " + AnimationTag);
            Play();
        }

        protected void Play()
        {
            Baller.Animator.SetBool(AnimationTag, true);
        }

        public virtual void ExitState()
        {
            Debug.WriteLine("ExitState: " + AnimationTag);
            Stop();
        }

        protected void Stop()
        {
            Baller.Animator.SetBool(AnimationTag, false);
        }

        public virtual void FixedUpdateState()
        {

        }

        public abstract void UpdateState();      
        
    }
}