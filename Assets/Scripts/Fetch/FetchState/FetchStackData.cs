
using Jenga.Script.Tower;
using UnityEngine;

namespace Jenga.Script.Fetch.FetchState{
public class FetchStackData : BaseFetch
{
    public FetchStackData(FetchAPI fetchAPI) : base(fetchAPI)
    {
        this.fetchAPI = fetchAPI;
    }

    public override void StartFetch()
    {
        base.StartFetch();
        url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
    }
    public override void DoneFetch()
    {
        base.DoneFetch();

        fetchAPI.jengaModel.jengaRootData = UnityEngine.JsonUtility.FromJson<JengaRootData>("{\"jengaRoots\":" + fetchResult + "}");
        JengaRoot[] jengaRoots = fetchAPI.jengaModel.jengaRootData.jengaRoots;
        Debug.Log("JengaModel: " + jengaRoots[0].standardid);

        //loop through jengaRootData and find 6th grade standards
        for (int i = 0; i < jengaRoots.Length; i++)
        {
            if (jengaRoots[i].grade == "6th Grade")
            {
                fetchAPI.jengaModel.numberOfSixthGradeStandards++;
                fetchAPI.jengaModel.sixthGradeJengaList.Add(jengaRoots[i]);
            }
            if (jengaRoots[i].grade == "7th Grade")
            {
                fetchAPI.jengaModel.numberOfSeventhGradeStandards++;
                fetchAPI.jengaModel.seventhGradeJengaList.Add(jengaRoots[i]);
            }
            if (jengaRoots[i].grade == "8th Grade")
            {
                fetchAPI.jengaModel.numberOfEighthGradeStandards++;
                fetchAPI.jengaModel.eigthGradeJengaList.Add(jengaRoots[i]);
            }
        }

        fetchAPI.sixthGradeTowerManager.rowsNumber = fetchAPI.jengaModel.numberOfSixthGradeStandards / 3;
        fetchAPI.sixthGradeTowerManager.isBlockLastRow = true;
        fetchAPI.sixthGradeCenterMaintainer = fetchAPI.sixthGradeTowerManager.gameObject.GetComponentInChildren<CenterMaintainer>();
        fetchAPI.sixthGradeTowerManager.BuildTower();
        fetchAPI.sixthGradeCenterMaintainer.StartCenter();

        fetchAPI.seventhGradeTowerManager.rowsNumber = fetchAPI.jengaModel.numberOfSeventhGradeStandards / 3;
        fetchAPI.seventhGradeTowerManager.isBlockLastRow = true;
        fetchAPI.seventhGradeCenterMaintainer = fetchAPI.seventhGradeTowerManager.gameObject.GetComponentInChildren<CenterMaintainer>();
        fetchAPI.seventhGradeTowerManager.BuildTower();
        fetchAPI.seventhGradeCenterMaintainer.StartCenter();

        fetchAPI.eighthGradeTowerManager.rowsNumber = fetchAPI.jengaModel.numberOfEighthGradeStandards / 3;
        fetchAPI.eighthGradeTowerManager.isBlockLastRow = true;
        fetchAPI.eigthGradeCenterMaintainer = fetchAPI.eighthGradeTowerManager.gameObject.GetComponentInChildren<CenterMaintainer>();
        fetchAPI.eighthGradeTowerManager.BuildTower();
        fetchAPI.eigthGradeCenterMaintainer.StartCenter();
    }
}
}

