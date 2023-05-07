using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenga.Script.GameMode
{
    public class GameModeBaseState
    {
        public string name;
        protected GameModeStateMachine stateMachine;

        public GameModeBaseState(string name, GameModeStateMachine stateMachine)
        {
            this.name = name;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void UpdateLogic()
        {

        }

        public virtual void UpdatePhysics()
        {

        }

        public virtual void Exit()
        {

        }

    }
}