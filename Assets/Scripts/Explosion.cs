using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explosionRadius = 40f;
    //[SerializeField] private float _explosionForce = 500f;
    [SerializeField] private Cube _cube;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _force = 1000;

    private void OnEnable()
    {
        _spawner.Created += Explode;
    }

    private void OnDisable()
    {
        _spawner.Created -= Explode;
    }

    private void Explode(List<Cube> cubes, Vector3 center)
    {
        Debug.Log("Взрыв");
        Instantiate(_explosionParticle, transform.position, transform.rotation);

        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(_force, center, _explosionRadius);
        }
    }


    /*private void Explode(Cube cube)
    {
        Debug.Log("Взрыв");
        Instantiate(_explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);

        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }*/
}
