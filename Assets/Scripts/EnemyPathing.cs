using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig _waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex;

    void Start()
    {
        _waypoints = _waveConfig.GetWaypoints();
    }

    void Update()
    {
        Move();
    }

    public WaveConfig WaveConfig
    {
        set => _waveConfig = value;
    }

    private void Move()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;
            var movementThisFrame = _waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}