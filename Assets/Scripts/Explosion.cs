using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 40f;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _force = 1000;

    private void OnDisable()
    {
        _spawner.Created -= Explode;
    }

    private void Explode(List<Cube> cubes, Vector3 center)
    {
        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(_force, center, _explosionRadius);
        }
    }

    public void Initialize(Spawner spawner)
    {
        _spawner = spawner;
        _spawner.Created += Explode;
    }
}
