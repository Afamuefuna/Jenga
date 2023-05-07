using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEngine.UI;

namespace Jenga.Script.Tower.Block
{
    public class BlockManager : MonoBehaviour
    {
        public Collider floorCollider;
        public Color hoverColor = new Color(0.7f, 1f, 0.7f);
        public Color selectionColor = new Color(0.51f, 1f, 0.51f);

        [Range(0.5f, 45f)]
        public float
            angleTolerantion = 5f;

        private bool isSelected = false;
        private bool isMoved = false;
        private Mode mode = Mode.NONE;
        private float timer = -0.5f;
        private Vector3 originalPosition;
        private TowerManager tower;
        [SerializeField] private BlockDetails blockDetails;

        [SerializeField] Interface interface_;

        enum Mode
        {
            MOVING,
            FADING,
            REVERSING,
            NONE
        }

        #region Messages
        void Start()
        {
            if (floorCollider == null)
                floorCollider = GameObject.Find("Terrain").GetComponent<Collider>();
            tower = GameObject.FindObjectOfType<TowerManager>();
            blockDetails = GetComponent<BlockDetails>();
            interface_ = GameObject.FindObjectOfType<Interface>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastUpdate();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Terrain")
            {

            }
        }

        #endregion

        #region Update methods

        void RaycastUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (hit.collider == GetComponent<Collider>())
                    {
                        if (isSelected)
                            Deselect();
                        else
                            Select();
                    }
                }

                if (isSelected)
                    GetComponent<Renderer>().material.color = selectionColor;
                else if (hit.collider == GetComponent<Collider>())
                    GetComponent<Renderer>().material.color = hoverColor;
                else
                    GetComponent<Renderer>().material.color = Color.white;
            }
        }
        #endregion

        #region Select/deselect methods
        public void Select()
        {
            foreach (BlockManager obj in FindObjectsOfType<BlockManager>())
            {
                obj.Deselect();
            }

            if (interface_ != null)
            {
                interface_.infoParent.gameObject.SetActive(true);
            }
            isSelected = true;
            originalPosition = transform.localPosition;
            GetComponent<Renderer>().material.color = hoverColor;
            interface_.infoParent.gameObject.SetActive(true);
            interface_.GradeLevel.text = blockDetails.jengaRoot.grade;
            interface_.Cluster.text = blockDetails.jengaRoot.cluster;
            interface_.StandardID.text = blockDetails.jengaRoot.standardid;
            interface_.StandardDescription.text = blockDetails.jengaRoot.standarddescription;
            interface_.Domain.text = blockDetails.jengaRoot.domain;
        }

        public void Deselect()
        {
            isSelected = false;
            if (mode != Mode.FADING && isMoved)
            {
                isMoved = false;
                timer = -0.5f;
                mode = Mode.REVERSING;
            }
            GetComponent<Renderer>().material.color = Color.white;
            if (interface_ != null)
            {
                interface_.infoParent.gameObject.SetActive(false);
            }
        }
        #endregion
    }

}

