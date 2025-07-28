using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private Transform[] _waypoints;
    
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
        
        if (Vector3.Distance(transform.position, _waypoint.position) == 0)
            _waypoint = _waypoints[++_currentWaypointIndex % _waypoints.Length];
    }
}
