using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class TransactionService : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private GameObject loginPage;
    [SerializeField] private TMP_InputField senderAddress;
    [SerializeField] private TMP_InputField privatekey;
    [SerializeField] private GenerateRtId rtId;


    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileTransaction";
    
    private void Start()
    {
        Register();
    }




    public void Register()
    {
        string _senderaddress = senderAddress.text;
        string _privatekey = privatekey.text;
        string rtid = rtId.GetRtId();

        StartCoroutine(Registrations(_senderaddress,_privatekey,rtid));
    }

    IEnumerator Registrations(string _username, string _password,string rtid)
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("senderAddress", _username);
        wWForm.AddField("privateKey", _password);
        wWForm.AddField("rtId", rtid);
        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {

            yield return request.SendWebRequest();
            var response = request.result;

            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully transationservice ");
                
            }
        }
    }

}
