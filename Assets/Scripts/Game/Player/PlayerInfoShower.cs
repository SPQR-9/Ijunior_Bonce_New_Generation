using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInfoShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.CoinsCollected += ShowCoinsCount;
    }

    private void OnDisable()
    {
        _player.CoinsCollected -= ShowCoinsCount;
    }

    private void ShowCoinsCount()
    {
        _coinText.text = _player.GetCoinsCount().ToString();
    }
}
