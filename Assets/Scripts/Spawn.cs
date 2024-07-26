using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private int _initialCountCubes;
    [SerializeField] private Cube _cube;
    [SerializeField] private Transform _spawnPoint;

    private int _maxCubesSpawn = 6;
    private int _minCubesSpawn = 2;
    private int _indexToDecreaseScale = 2;
    private int _IndexForDerciseChanceSpleet = 2;

    private void Awake()
    {
        for (int i = 0; i < _initialCountCubes; i++)
        {
            CreateNewCube(_cube, _spawnPoint.position);
        }
    }

    private Cube CreateNewCube(Cube cube, Vector3 initialPosition)
    {
        _cube.Explosed += CreateRedusedCubes;
        Vector3 position = initialPosition + Random.onUnitSphere * cube.transform.localScale.x;

        if (Physics.Linecast(initialPosition, position, out RaycastHit hitInfo))
        {
            position = hitInfo.point;
        }

        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.Explosed += CreateRedusedCubes;

        return newCube;
    }

    private void CreateRedusedCubes(Cube cube)
    {
        cube.Explosed -= CreateRedusedCubes;
        int countCubes = Random.Range(_minCubesSpawn, _maxCubesSpawn);
        List<Cube> cubes = new List<Cube>();

        Vector3 scale = cube.transform.localScale / _indexToDecreaseScale;
        float chanceSplit = cube.ChanceSplit / _IndexForDerciseChanceSpleet;

        for (int i = 0; i < countCubes; i++)
        {
            Cube newCube = CreateNewCube(cube, cube.transform.position);
            newCube.Initialization(scale, chanceSplit);

            cubes.Add(newCube);
        }
    }
}
