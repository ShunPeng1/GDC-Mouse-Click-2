using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer)) ]
public class Trajectory : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;

    private Scene _simulationScene;
    private PhysicsScene2D _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Start() {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = _simulationScene.GetPhysicsScene2D();
        
        foreach (Transform obj in _obstaclesParent) {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            SpriteRenderer spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update() {
        foreach (var item in _spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(Vector3 pos, Vector3 velocity) {
        var ghostObj = Instantiate(ResourceManager.Instance.ghostTrajectoryGameObject, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        
        ghostObj.GetComponent<TrajectoryGhostObject>().Init(velocity, true);

        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }
}
