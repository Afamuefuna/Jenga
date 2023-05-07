using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Jenga.Script;
using Jenga.Script.Tower.Block;

namespace Jenga.Script.Tower
{
    public enum TowerType
    {
        SIXTHGRADE,
        SEVENTHGRADE,
        EIGHTHGRADE
    }

    public class TowerManager : MonoBehaviour
    {
        public int rowsNumber = 18;
        public int numberOfBlocksAdded;
        public GameObject blockModel, setupBlockModel;
        public TowerState State
        {
            get { return _desiredState; }
            set
            {
                if (_desiredState == value)
                    return;
            }
        }
        public event EventHandler RowAdded;

        [SerializeField] private List<GameObject> rows = new List<GameObject>();
        [SerializeField] private List<JengaRoot> jengaRoots = new List<JengaRoot>();
        private TowerState _desiredState, _currentState;
        private GameObject towerCenter;

        public List<GameObject> blocks = new List<GameObject>();

        [SerializeField] TowerType towerType;

        public enum TowerState
        {
            PLACING,
            REMOVING,
            WAITING,
            FALLING
        }

        #region Messages
        // Use this for initialization
        void Start()
        {
            towerCenter = new GameObject("Center");
            towerCenter.transform.parent = transform;
            towerCenter.AddComponent<CenterMaintainer>();

            if (towerType == TowerType.SIXTHGRADE)
            {
                jengaRoots = jengaModel.sixthGradeJengaList;
            }
            else if (towerType == TowerType.SEVENTHGRADE)
            {
                jengaRoots = jengaModel.seventhGradeJengaList;
            }
            else if (towerType == TowerType.EIGHTHGRADE)
            {
                jengaRoots = jengaModel.eigthGradeJengaList;
            }

        }

        public JengaModel jengaModel;
        public Material glassMaterial, stoneMaterial, woodMaterial;

        public void BuildTower()
        {
            if (rows.Count > 0)
            {
                foreach (GameObject obj in rows)
                {
                    Destroy(obj);
                }
            }
            var blockSize = ((BoxCollider)blockModel.GetComponent<Collider>()).size;

            while (rows.Count < rowsNumber)
            {
                var row = AddRow();

                for (int z = -1; z <= 1; z++)
                {
                    GameObject block = (GameObject)Instantiate(blockModel);
                    block.transform.parent = row.transform;
                    block.transform.localRotation = Quaternion.identity;
                    block.transform.localPosition = new Vector3(0, 0, z * blockSize.z);

                    BlockDetails blockDetails = block.GetComponent<BlockDetails>();
                    AddBlockDetails(blockDetails, numberOfBlocksAdded, jengaRoots);

                    if (jengaRoots[numberOfBlocksAdded].mastery == 0)
                    {
                        //assign a glass material
                        block.GetComponent<Renderer>().material = glassMaterial;
                    }
                    else if (jengaRoots[numberOfBlocksAdded].mastery == 1)
                    {
                        block.GetComponent<Renderer>().material = woodMaterial;
                    }
                    else
                    {
                        block.GetComponent<Renderer>().material = stoneMaterial;
                    }
                    numberOfBlocksAdded++;
                    blocks.Add(block);
                }
            }
            HandleTopBlockPlacerDestroyed(this, new EventArgs());
            State = TowerState.REMOVING;
        }

        public void AddBlockDetails(BlockDetails blockDetails, int numberOfBlocksAdded, List<JengaRoot> jengaRoots)
        {
            blockDetails.jengaRoot.id = jengaRoots[numberOfBlocksAdded].id;
            blockDetails.jengaRoot.subject = jengaRoots[numberOfBlocksAdded].subject;
            blockDetails.jengaRoot.grade = jengaRoots[numberOfBlocksAdded].grade;
            blockDetails.jengaRoot.mastery = jengaRoots[numberOfBlocksAdded].mastery;
            blockDetails.jengaRoot.domainid = jengaRoots[numberOfBlocksAdded].domainid;
            blockDetails.jengaRoot.domain = jengaRoots[numberOfBlocksAdded].domain;
            blockDetails.jengaRoot.cluster = jengaRoots[numberOfBlocksAdded].cluster;
            blockDetails.jengaRoot.standardid = jengaRoots[numberOfBlocksAdded].standardid;
            blockDetails.jengaRoot.standarddescription = jengaRoots[numberOfBlocksAdded].standarddescription;
        }

        #region Private methods
        GameObject AddRow()
        {
            GameObject towerRow = new GameObject();
            towerRow.transform.parent = transform;
            int rowNum = rows.Count + 1;
            if (rowNum % 2 != 0)
            {
                towerRow.name = "Row #" + rowNum + " (0)";
                towerRow.transform.localRotation = Quaternion.identity;

            }
            else
            {
                towerRow.name = "Row #" + rowNum + " (90)";
                towerRow.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            towerRow.transform.localPosition = new Vector3(0, rowNum * ((BoxCollider)blockModel.GetComponent<Collider>()).size.y, 0);
            rows.Add(towerRow);

            if (RowAdded != null)
                RowAdded(this, new EventArgs());

            return towerRow;
        }
        #endregion

        public GameObject[] GetAllRows()
        {
            return rows.ToArray();
        }
        #endregion

        #region Event handlers
        void HandleTopBlockPlacerDestroyed(object sender, EventArgs e)
        {
            var row = AddRow();
            var components = rows[rows.Count - 2].GetComponentsInChildren<BlockManager>();
        }

        void HandleBlockPlaced(object sender, BlockEventArgs e)
        {
            State = TowerState.REMOVING;
        }

        void HandleBlockDropped(object sender, BlockEventArgs e) // game over
        {

        }

        void HandleBlockFading(object sender, BlockEventArgs e)
        {

        }

        void HandleBlockFaded(object sender, BlockEventArgs e)
        {
            State = TowerState.PLACING;
        }
        #endregion

        public bool isBlockLastRow = false;

        void BlockLastRows()
        {
            if (!isBlockLastRow)
            {
                return;
            }

            List<BlockManager> managers = new List<BlockManager>(rows[rows.Count - 1].GetComponentsInChildren<BlockManager>());
            managers = managers.Union(rows[rows.Count - 2].GetComponentsInChildren<BlockManager>()).ToList();

            foreach (BlockManager bm in managers)
            {
                bm.enabled = false;
            }
        }

    }
}
