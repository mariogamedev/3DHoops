
namespace Baller
{
    public class IdleState : BaseBallerState
    {
        private readonly Baller _baller;

        public IdleState(Baller baller)
        {
            State = BallerStates.Idle;
            _baller = baller;
        }

        public override void EnterState()
        {

        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {

        }
    }
}