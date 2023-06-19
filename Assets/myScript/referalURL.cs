using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class referalURL : MonoBehaviour
{
    // Start is called before the first frame update

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private TMP_InputField referalUrl;
    [SerializeField] private walletIdKey wallet;

    void Start()
    {
        GetRequest();
        
    }



    IEnumerator GetRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            webRequest.SetRequestHeader("Authorization", "Bearer " + wallet.GetToken());
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Referal();
                    break;
            }
        }
    }















    public void Referal()
    {
        string _referalUrl = referalUrl.text;
        string _rtid = rtId.GetRtId();

        StartCoroutine(PostingReferalUrl(_referalUrl, _rtid));
    }

    IEnumerator PostingReferalUrl(string _referalUrl, string _rtid)
    {

        WWWForm wWForm = new WWWForm();
        wWForm.AddField("referralURL", _referalUrl);
        wWForm.AddField("rtId", _rtid);
       
        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {
            request.SetRequestHeader("Authorization", "Bearer " + wallet.GetToken());
            yield return request.SendWebRequest();
            var response = request.result;

            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("url  referaled ");
                
            }
        }
    }
}
