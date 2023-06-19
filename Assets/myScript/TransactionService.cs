using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class TransactionService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject referal;
    [SerializeField] private GameObject register;
    [SerializeField] private TMP_InputField senderAddress;
    [SerializeField] private TMP_InputField privatekey;
    [SerializeField] private GenerateRtId rtId;

    [SerializeField] private walletIdKey wallet;


    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileTransaction";
    

    public void SwitchToReferal()
    {
        register.SetActive(false);
        referal.SetActive(true);
    }





    private void Start()
    {
        if (wallet.GetWalletId()!=null)
        {
            SwitchToReferal();
        }
        Register();
    }




    public void Register()
    {

     
        string _senderaddress = senderAddress.text;
        string _privatekey = privatekey.text;
        string rtid = rtId.GetRtId();

        wallet.SetWalletId(_senderaddress);

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
            request.SetRequestHeader("Authorization", "Bearer " + wallet.GetToken());
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
