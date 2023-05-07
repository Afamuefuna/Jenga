using UnityEngine;
using Unity;
using System.Collections;
using UnityEngine.Networking;

namespace Jenga.Script.Fetch
{
    public class BaseFetch
    {
        public FetchAPI fetchAPI;

        public BaseFetch(FetchAPI fetchAPI)
        {
            this.fetchAPI = fetchAPI;
        }

        public virtual void InvokeSearch()
        {
            fetchAPI.StartCoroutine(Fetch());
        }

        public virtual void StartFetch()
        {

        }

        public virtual void ErrorFetch()
        {

        }

        public virtual void DoneFetch()
        {

        }

        public virtual IEnumerator Fetch()
        {
            StartFetch();
            //Get request API
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                // Error Occurred
                Debug.Log(request.error);
                ErrorFetch();
            }
            else
            {
                // Response can be accessed through: request.downloadHandler.text
                Debug.Log(request.downloadHandler.text);
                fetchResult = request.downloadHandler.text;

                DoneFetch();
            }
        }

        public string fetchResult;
        public string url;
    }
}