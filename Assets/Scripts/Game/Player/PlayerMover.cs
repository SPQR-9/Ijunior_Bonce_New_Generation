using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] ContactFilter2D _filter = new ContactFilter2D();
    [SerializeField] private float _rayCastRange = 0f;

    private Rigidbody2D _rigidbody;
    private bool _jumpPermission = false;
    private Player _player;
    private bool _pause = false;
    private readonly RaycastHit2D[] _rayResult = new RaycastHit2D[1];

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        
    }

    private void OnEnable()
    {
        _player.Dead += Die;
    }

    private void OnDisable()
    {
        _player.Dead -= Die;
    }

    private void Update()
    {
        if (_pause && _jumpPermission)
            return;
        int collisionCount = _rigidbody.Cast(transform.right, _filter, _rayResult, _rayCastRange);
        if (collisionCount > 0)
            return;
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && _jumpPermission)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpPermission = false;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void DisableMover()
    {
        _pause = true;
    }

    public void EnableMover()
    {
        _pause = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
        {
            _jumpPermission = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _) && _jumpPermission == true)
        {
            _jumpPermission = false;
        }
    }

}
