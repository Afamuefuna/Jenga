using System.Collections;
using System.Collections.Generic;
using Jenga.Script;
using Jenga.Script.Fetch.FetchState;
using Jenga.Script.Tower;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Jenga.Script.Fetch
{
    public class FetchAPI : MonoBehaviour
    {
        FetchStackData fetchStackData;
        public JengaModel jengaModel;
        public TowerManager sixthGradeTowerManager;
        public TowerManager seventhGradeTowerManager;
        public TowerManager eighthGradeTowerManager;
        [HideInInspector] public CenterMaintainer sixthGradeCenterMaintainer;
        [HideInInspector] public CenterMaintainer seventhGradeCenterMaintainer;
        [HideInInspector] public CenterMaintainer eigthGradeCenterMaintainer;

        private void Awake()
        {
            fetchStackData = new FetchStackData(this);
        }
        void Start()
        {
            fetchStackData.InvokeSearch();
        }

    }
}
