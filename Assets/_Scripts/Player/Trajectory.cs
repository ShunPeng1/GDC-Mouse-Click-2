using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;


[RequireComponent(typeof(LineRenderer)) ]
public class Trajectory : MonoBehaviour {
    
    private LineRenderer _lineRenderer;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    
    private void Update() {
        
    }

    public void SimulateTrajectory(Vector3 pos, Vector3 velocity) {
        var ghostObj = Instantiate(ResourceManager.Instance.ghostTrajectoryGameObject, pos, Quaternion.identity);
        PlatformManager.Instance.MoveObjectToSimulateScene(ghostObj);
        
        ghostObj.GetComponent<TrajectoryGhostObject>().Init(velocity);

        _lineRenderer.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _lineRenderer.SetPosition(i, ghostObj.transform.position);
            PlatformManager.Instance.Simulate();
        }

        Destroy(ghostObj.gameObject);
    }
}