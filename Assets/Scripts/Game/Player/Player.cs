using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public event UnityAction Dead;
    public event UnityAction CoinsCollected;

    [SerializeField] private UnityEvent _activateOnDisable;

    private int _coins;

    public void TakeCoin()
    {
        _coins++;
        CoinsCollected.Invoke();
    }

    public void Die()
    {
        _activateOnDisable?.Invoke();
        Dead?.Invoke();
    }

    public int GetCoinsCount()
    {
        return _coins;
    }
}
