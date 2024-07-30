using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] Cube _cube;
    [SerializeField] Transform _spawnPoint;

    private int _initialCubes = 6;
    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;
    private int _indexToDecreaseScale = 2;
    private int _IndexForDerciseChanceSpleet = 2;

    public event Action<List<Cube>, Vector3> Created;

    private void Awake()
    {
        for (int i = 0; i < _initialCubes; i++)
        {
            CreateCube(_cube, _spawnPoint.position);
        }
    }

    private Cube CreateCube(Cube cube, Vector3 startPosition)
    {
        Vector3 position = startPosition + Random.onUnitSphere * cube.transform.localScale.x;

        if (Physics.Linecast(startPosition, position, out RaycastHit hitInfo))
        {
            position = hitInfo.point;
        }

        Cube newCube = Instantiate(cube, position, Quaternion.identity);
        newCube.Exploded += CreateRedusedCubes;

        return newCube;
    }

    private void CreateRedusedCubes(Cube cube)
    {
        cube.Exploded -= CreateRedusedCubes;
        int countCubes = Random.Range(_minCountCubes, _maxCountCubes);
        List<Cube> cubes = new();

        Vector3 scale = cube.transform.localScale / _indexToDecreaseScale;
        float chanceToSplite = cube.ChanceToSplit / _IndexForDerciseChanceSpleet;

        for (int i = 0; i < countCubes; i++)
        {
            Cube newCube = CreateCube(cube, cube.transform.position);
            newCube.Initialization(scale, chanceToSplite, this);
            cubes.Add(newCube);
        }

        Created?.Invoke(cubes, cube.transform.position);
    }
}
