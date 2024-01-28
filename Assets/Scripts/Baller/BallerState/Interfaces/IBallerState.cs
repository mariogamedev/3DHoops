
namespace Baller
{
    public interface IBallerState
    {
        public BallerStates BallerState { get; }
        void EnterState();
        void UpdateState();
        void FixedUpdateState();
        void ExitState();
    }
}