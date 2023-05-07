using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenga.Script
{
    public class JengaModel : MonoBehaviour
    {
        public JengaRootData jengaRootData;
        public List<JengaRoot> sixthGradeJengaList = new List<JengaRoot>();
        public List<JengaRoot> seventhGradeJengaList = new List<JengaRoot>();
        public List<JengaRoot> eigthGradeJengaList = new List<JengaRoot>();

        public int numberOfSixthGradeStandards = 0;
        public int numberOfSeventhGradeStandards = 0;
        public int numberOfEighthGradeStandards = 0;
    }

    [System.Serializable]
    public class JengaRootData
    {
        public JengaRoot[] jengaRoots;
    }

    [System.Serializable]
    public class JengaRoot
    {
        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
    }

}

