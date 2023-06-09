﻿using UnityEngine;
using System.Collections;

namespace Jenga.Script.Tower
{
    public class CenterMaintainer : MonoBehaviour
    {
        TowerManager tower;

        // Use this for initialization
        public void StartCenter()
        {
            tower = transform.parent.GetComponent<TowerManager>();
            tower.RowAdded += (sender, e) => UpdateCenter();
            UpdateCenter();
        }

        // Update is called once per frame
        void UpdateCenter()
        {
            var rows = tower.GetAllRows();
            float y = (rows[rows.Length - 1].transform.localPosition.y + rows[0].transform.localPosition.y) / 2;
            transform.localPosition = new Vector3(0, y, 0);
        }
    }
}
