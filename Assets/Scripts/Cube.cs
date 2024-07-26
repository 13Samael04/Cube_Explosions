using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private float _explosionRadius = 40f;
    [SerializeField] private float _explosionForce = 500f;

    public event Action<Cube> Explosed;

    private float _maxChanceSplite = 100f;
    private float _minChanceSplite = 0;

    public float ChanceSplit { get; private set; } = 100f;

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Initialization(Vector3 scale, float chanceSplit)
    {
        transform.localScale = scale;
        ChanceSplit = chanceSplit;
    }

    private void OnMouseUpAsButton()
    {
        if (TrySplite() == false)
        {
            Explode();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Instantiate(_explosionParticle, transform.position, transform.rotation);
        Explosed?.Invoke(this);
        Destroy(gameObject);

        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private bool TrySplite()
    {
        float chance = Random.Range(_minChanceSplite, _maxChanceSplite);

        if (chance <= ChanceSplit)
        {
            return false;
        }
        else
        {
            return true;
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
    }
}
