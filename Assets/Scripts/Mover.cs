using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _waypointReachThreshold = 0.1f;
    
    private int _currentWaypointIndex = 0;
    private Transform _waypoint;

    private void Awake()
    {
        _waypoint = _waypoints[_currentWaypointIndex];
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoint.position, _speed * Time.deltaTime);
        
        var sqrMaxDistance = _waypointReachThreshold * _waypointReachThreshold;
        
        if ((_waypoint.position - transform.position).sqrMagnitude < sqrMaxDistance)
            _waypoint = _waypoints[++_currentWaypointIndex % _waypoints.Length];
    }
}
