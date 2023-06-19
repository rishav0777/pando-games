using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class RegisterUsers : MonoBehaviour
{

   // [SerializeField] private GameObject loginPage;
    //[SerializeField] private TMP_InputField username;
    //[SerializeField] private TMP_InputField password;
    [SerializeField] private GenerateRtId rtId;
   
    private string Token;
    

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/RT_userRegistartion";
    private string Rurl = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileCreateRt";

    
    public class MyData
    {
        public string message;
        public string token;
    }

    public class verificationResponse
    {
        public string message;
        public string walletId;
        public int status;
    }

    string _walletId;
    [SerializeField] private walletIdKey wallet;

    

  







    private void Start()
    {
        Register();
    }


    public void Register()
    {
        string _username = rtId.GetRtId() + "@pandoproject.org";
        string _password = rtId.GetRtId();

        StartCoroutine(Registrations(_username,_password));
    }

    IEnumerator Registrations(string _username,string _password)
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("username", _username);
        wWForm.AddField("password", _password);
        wWForm.AddField("role", "four");
        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {

            yield return request.SendWebRequest();
            var response = request.result;
            
            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully registered ");
                LoginF();
            }
        }
    }










    public void LoginF()
    {
        string _username = rtId.GetRtId() + "@pandoproject.org";
        string _password = rtId.GetRtId();

        StartCoroutine(Logging(_username, _password));
    }

    IEnumerator Logging(string _username, string _password)
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("username", _username);
        wWForm.AddField("password", _password);
        wWForm.AddField("role", "four");
        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {

            yield return request.SendWebRequest();
            var response = request.result;

            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully Login");
                string jason = request.downloadHandler.text;
                MyData data = JsonUtility.FromJson<MyData>(jason);
                Token = data.token;
                wallet.SetToken(Token);
                CreateVerify(Token);
            }
        }
    }











    public void CreateVerify(string token)
    {
        print("verifying");
        string _rtId = rtId.GetRtId();
        StartCoroutine(Verifications(_rtId,token));
    }

    IEnumerator Verifications(string _rtId,string token)
    {
        WWWForm wWForm = new WWWForm();


        wWForm.AddField("rtId", _rtId);
        wWForm.AddField("walletId",null);
        wWForm.AddField("latitude","8.888888");
        wWForm.AddField("longitude","88.99");
        wWForm.AddField("rtType","rtMobile");
        wWForm.AddField("created", "true");
        wWForm.AddField("walletId", null);
        wWForm.AddField("allFreeMemory", (int)854.6640625);
        wWForm.AddField("availableRam", (int)7926.78125);
        wWForm.AddField("core",4);
        wWForm.AddField("cpu","11th Gen Intel(R) Core(TM)i3-1115G4 @ 3.00GHz");
        wWForm.AddField("osPlateform","Android 12,MIUI 14");
        wWForm.AddField("osType","Android");
        wWForm.AddField("osVersion","Android 12");
        wWForm.AddField("speed","56");
        wWForm.AddField("stake","0");
        wWForm.AddField("country","india");


        using (UnityWebRequest request = UnityWebRequest.Post(Rurl, wWForm))
        {
            request.SetRequestHeader("Authorization", "Bearer " + token);
            yield return request.SendWebRequest();
            print(request.result);
            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully created and verified");
                string response = request.downloadHandler.text;
                verificationResponse data = JsonUtility.FromJson<verificationResponse>(response);
                _walletId = data.walletId;
                wallet.SetWalletId(_walletId);
            }
        }
    }


    




}
