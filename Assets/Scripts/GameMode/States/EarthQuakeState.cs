using UnityEngine;

namespace Jenga.Script.GameMode.States
{

    public class EarthQuakeState : GameModeBaseState
    {
        private GameMode gameMode;
        public EarthQuakeState(GameMode stateMachine) : base("EarthQuake", stateMachine)
        {
            gameMode = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}