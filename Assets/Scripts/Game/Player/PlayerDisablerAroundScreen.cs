using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDisablerAroundScreen : MonoBehaviour
{
    private Vector2 _disablePoint;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
        _disablePoint = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0));
    }

    private void Update()
    {
        if (transform.position.y < _disablePoint.y)
        {
            _player.Die();
        }
    }
}
