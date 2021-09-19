using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundGenerator : PoolObject
{ 
    [SerializeField] private int _minDistancePlatform = 1;
    [SerializeField] private int _maxDistancePlatform = 3;

    private void Awake()
    {
        InstantiateAllGroundPlatforms();
    }

    private void Update()
    {
        Vector3 maxPointCamera = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
        if (maxPointCamera.x>=_currentPlatform.MaxPositionX && IsPlatformsAvailable())
        {
            SetNewPlatform(GetFollowingPlatform());
            DisableObjectAbroadScreen();
        }
    }

    private bool IsPlatformsAvailable()
    {
        int numberOfInactivePlatform = _groundPlatforms.Count(n => !n.gameObject.activeSelf);
        if (numberOfInactivePlatform <= 0)
        {
            Debug.LogError("There are not enough free platforms to create");
            return false;
        }
        return true;
    }

    private GroundPlatform GetFollowingPlatform()
    {
        while (true)
        {
            int randPlatformIndex = Random.Range(0, _groundPlatforms.Count);
            if (!_groundPlatforms[randPlatformIndex].gameObject.activeInHierarchy)
                return _groundPlatforms[randPlatformIndex];
        }
    }

    private void SetNewPlatform(GroundPlatform groundPlatform)
    {
        groundPlatform.gameObject.SetActive(true);
        int randomDistance = Random.Range(_minDistancePlatform, _maxDistancePlatform);
        Vector2 spawnPoint = new Vector2(_currentPlatform.MaxPositionX + groundPlatform.Lenght/2 + randomDistance, _currentPlatform.transform.position.y);
        groundPlatform.gameObject.transform.position = spawnPoint;
        _currentPlatform = groundPlatform;
        if(transform.position.x!=_currentPlatform.Center)
            Alignment();
    }

    private void Alignment()
    {
        float errorRate = _currentPlatform.transform.position.x - _currentPlatform.Center;
        _currentPlatform.transform.position = new Vector2(_currentPlatform.transform.position.x + errorRate, _currentPlatform.transform.position.y);
    }
}
