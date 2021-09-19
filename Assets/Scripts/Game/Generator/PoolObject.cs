using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] protected GameObject[] _groundPrefubs;
    [SerializeField] protected Transform _poolGroundContainer;
    [SerializeField] protected GroundPlatform _currentPlatform;

    protected List<GroundPlatform> _groundPlatforms = new List<GroundPlatform>();

    protected void InstantiateAllGroundPlatforms()
    {
        foreach (var ground in _groundPrefubs)
        {
            var newGroundPlatform = Instantiate(ground, _poolGroundContainer);
            if (newGroundPlatform.TryGetComponent(out GroundPlatform groundPlatform))
                _groundPlatforms.Add(groundPlatform);
            else
                Debug.Log("На объекте отсутствует скрипт GroundPlatform: " + newGroundPlatform.name);
            newGroundPlatform.SetActive(false);
        }
    }

    protected void DisableObjectAbroadScreen()
    {
        Vector3 disablePoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        foreach (var ground in _groundPlatforms)
        {
            if (ground.gameObject.activeSelf == true)
            {
                if (ground.MaxPositionX < disablePoint.x)
                    ground.gameObject.SetActive(false);
            }
        }
    }
}
