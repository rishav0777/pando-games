using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class walletIdKey : MonoBehaviour
{

    string walletid;
    String _token;
    public string GetWalletId()
    {
        return walletid;
    }
    public void SetWalletId(String walletId)
    {
        walletid = walletId;
    }



    public string GetToken()
    {
        return _token;
    }
    public void SetToken(String token)
    {
        _token = token;
    }


}
