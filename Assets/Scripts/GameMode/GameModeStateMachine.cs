using UnityEngine;

namespace Jenga.Script.GameMode
{
    public class GameModeStateMachine : MonoBehaviour
    {
        public GameModeBaseState currentState;

        public virtual void Start()
        {
            currentState = GetInitialState();
            if (currentState != null)
                currentState.Enter();
        }

        void Update()
        {
            if (currentState != null)
                currentState.UpdateLogic();
        }

        void LateUpdate()
        {
            if (currentState != null)
                currentState.UpdatePhysics();
        }

        public virtual void ChangeState(GameModeBaseState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = newState;

            currentState.Enter();
        }

        protected virtual GameModeBaseState GetInitialState()
        {
            return null;
        }
    }
}
