using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NativeShareNamespace;
using System.IO;

public class ShareOnSocialMedia : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelShare;
    [SerializeField]
    private string _referralCode;

    //for test purpose only
    [SerializeField]
    private string _demoReferaalCode;


    private void Start()
    {
        _panelShare.SetActive(false);

        _demoReferaalCode = "This Is a demo referral code for Xando later to be integrated with client API";
    }

    public void ShareReferral()
    {
        _panelShare.SetActive(true);

        StartCoroutine(ShareReferralCode());
    }

    public void Share()
    {
        StartCoroutine(ShareReferralCode());
    }

	private IEnumerator ShareReferralCode()
	{
		yield return new WaitForEndOfFrame();

		

		new NativeShare()
			.SetSubject("Referral Code").
            SetText("Code: " + _demoReferaalCode)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
        
	}

}
