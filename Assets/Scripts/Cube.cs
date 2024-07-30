using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> Exploded;

    private float _maximumChance = 100f;
    private float _minimumChance = 0f;

    public Rigidbody Rigidbody { get; private set; }
    public float ChanceToSplit { get; private set; } = 100f;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialization(Vector3 scale, float change)
    {
        transform.localScale = scale;
        ChanceToSplit = change;
    }

    private void OnMouseUpAsButton()
    {
        TryToSplit();
    }

    private void TryToSplit()
    {
        float chance = Random.Range(_minimumChance, _maximumChance);

        if(chance <= ChanceToSplit)
        {
            Exploded?.Invoke(this);
        }

        Destroy(gameObject);
    }
}
