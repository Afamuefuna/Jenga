using System.Collections.Generic;
using UnityEngine;
using Jenga.Script.Tower;
using Jenga.Script.Tower.Block;

namespace Jenga.Script.GameMode.States
{
    public class TestMyStackState : GameModeBaseState
    {
        private GameMode gameMode;
        public TestMyStackState(GameMode stateMachine) : base("TestMyStack", stateMachine)
        {
            gameMode = stateMachine;
        }

        public void TestMyStackSetup(List<GameObject> gameObjects)
        {
            foreach (GameObject o in gameObjects)
            {
                if (o.GetComponent<BlockDetails>() != null)
                {
                    BlockDetails blockDetails = o.GetComponent<BlockDetails>();
                    Debug.Log(blockDetails.jengaRoot.mastery);
                    if (blockDetails.jengaRoot.mastery == 0)
                    {
                        o.SetActive(false);
                    }
                }
                else
                {

                }
            }
        }

        public override void Enter()
        {
            base.Enter();

            if (gameMode.currentTowerFocus == TowerType.SIXTHGRADE)
            {
                Debug.Log("Sixth");
                TestMyStackSetup(gameMode.sixthGradeTowerManager.blocks);
            }
            if (gameMode.currentTowerFocus == TowerType.SEVENTHGRADE)
            {
                Debug.Log("Seventh");
                TestMyStackSetup(gameMode.seventhGradeTowerManager.blocks);
            }
            if (gameMode.currentTowerFocus == TowerType.EIGHTHGRADE)
            {
                Debug.Log("eigth");
                TestMyStackSetup(gameMode.eighthGradeTowerManager.blocks);
            }
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