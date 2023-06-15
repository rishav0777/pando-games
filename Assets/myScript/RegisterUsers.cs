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
    

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/RT_userRegistartion";
    private string Rurl = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileCreateRt";

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
                CreateVerify();
            }
        }
    }











    public void CreateVerify()
    {
        print("verifying");
        string _rtId = rtId.GetRtId();
        StartCoroutine(Verifications(_rtId));
    }

    IEnumerator Verifications(string _rtId)
    {
        WWWForm wWForm = new WWWForm();


        wWForm.AddField("rtId", _rtId);
        wWForm.AddField("walletId",null);
        wWForm.AddField("latitude","8.888888");
        wWForm.AddField("longitude","88.99");
        wWForm.AddField("rtType","rtMobile");
        wWForm.AddField("created","true");
        wWForm.AddField("walletId", null);
        wWForm.AddField("allFreeMemory","854.6640625");
        wWForm.AddField("availableRam","7926.78125");
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

            yield return request.SendWebRequest();
            print(request.result);
            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully created and verified");

            }
        }
    }


    /*   public void PostRtID()
       {
           StartCoroutine(createandverifyrtid());
       }

       IEnumerator createandverifyrtid()
       {
           WWWForm wWForm = new WWWForm();
           wWForm.AddField("rtId", rtId.GetRtId());
           using (UnityWebRequest request = UnityWebRequest.Post(walletid, wWForm))
           {

               yield return request.SendWebRequest();
               if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
               else if (request.result == UnityWebRequest.Result.Success)
               {
                   print("Successfully create and verified rtid");
                   getRtID();

               }
           }
       }






       public void getRtID()
       {
           StartCoroutine(getReferal());
       }


       IEnumerator getReferal()
       {
           using (UnityWebRequest webRequest = UnityWebRequest.Get(walletid))
           {
               // Request and wait for the desired page.
               yield return webRequest.SendWebRequest();

               string[] pages = walletid.Split('/');
               int page = pages.Length - 1;

               switch (webRequest.result)
               {
                   case UnityWebRequest.Result.ConnectionError:
                   case UnityWebRequest.Result.DataProcessingError:
                       Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                       break;
                   case UnityWebRequest.Result.ProtocolError:
                       print(pages[page]);
                       //Debug.LogError(pages[page]) + ": HTTP Error: " + webRequest.error);
                       break;
                   case UnityWebRequest.Result.Success:
                       Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                       break;
               }
           }
       }*/




}
