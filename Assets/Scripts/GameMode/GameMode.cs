using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jenga.Script.Tower;
using Jenga.Script.GameMode.States;
using Jenga;

namespace Jenga.Script.GameMode
{
    public class GameMode : GameModeStateMachine
    {
        public TestMyStackState testMyStackState;
        public BuildMyStackState buildMyStackState;
        public ChallengeState challengeState;
        public EarthQuakeState earthQuakeState;
        public StrengthenMyStackState strengthenMyStackState;

        public TowerManager sixthGradeTowerManager;
        public TowerManager seventhGradeTowerManager;
        public TowerManager eighthGradeTowerManager;

        public TowerType currentTowerFocus;

        private void Awake()
        {
            testMyStackState = new TestMyStackState(this);
            buildMyStackState = new BuildMyStackState(this);
            challengeState = new ChallengeState(this);
            earthQuakeState = new EarthQuakeState(this);
            strengthenMyStackState = new StrengthenMyStackState(this);
        }

        public void SwitchTowerFocus(int currentTower)
        {
            currentTowerFocus = (TowerType)currentTower;
        }

        public void TestMyStack()
        {
            ChangeState(testMyStackState);
        }
    }

}
