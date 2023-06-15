using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;
    [SerializeField]
    private int _score = 000;

    private void Start()
    {
        _score = 000;
        m_Text.text = _score.ToString();
    }

    private void OnEnable()
    {
        Collectable.OnCoinCollect += Coin_OnCoinCollect;
    }

    private void Coin_OnCoinCollect()
    {
        _score += 100;
        m_Text.text = _score.ToString();
    }

    private void OnDisable()
    {
        Collectable.OnCoinCollect -= Coin_OnCoinCollect;
    }
}
