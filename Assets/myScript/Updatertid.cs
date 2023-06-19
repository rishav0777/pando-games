using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Updatertid : MonoBehaviour
{
    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileUpdateRt";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private walletIdKey wallet;
    void Start()
    {
        Update();

    }

    public void Update()
    {
        string _walletid = wallet.GetToken();
        string _rtid = rtId.GetRtId();

        StartCoroutine(Updating(_walletid, _rtid));
    }

    IEnumerator Updating(string _walletid, string _rtid)
    {

        WWWForm wWForm = new WWWForm();
        wWForm.AddField("rtId", _rtid);
        wWForm.AddField("walletId", _walletid);

        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {

          
            request.method = "POST";
            request.SetRequestHeader("X-HTTP-Method-Override", "PATCH");
            byte[] formDataBytes = wWForm.data;
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            request.uploadHandler = new UploadHandlerRaw(formDataBytes);




     
            yield return request.SendWebRequest();
            var response = request.result;

            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("successfully updated rtid ");

            }
        }
    }
}
