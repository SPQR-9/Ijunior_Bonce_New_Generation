using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GroundPlatform : MonoBehaviour
{
    private Renderer _renderer;
    private float _maxPositionX
    {
        get
        {
            return _renderer.bounds.max.x;
        }
    }
    private float _minPositionX
    {
        get
        {
            return _renderer.bounds.min.x;
        }
    }
    private float _center
    {
        get
        {
            return _renderer.bounds.center.x;
        }
    }
    private float _lenght;


    public float MaxPositionX => _maxPositionX;
    public float MinPositionX => _minPositionX;
    public float Center => _center;
    public float Lenght => _lenght;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _lenght = _maxPositionX - _minPositionX;
        
    }

    private void OnEnable()
    {
        SetActiveAllChildrenObjects();   
    }

    private void SetActiveAllChildrenObjects()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
