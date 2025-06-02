using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{
    public GameObject CubePrefab;
    public ParticleSystem explosionEffect;

    [SerializeField] private int _minNewCubes = 2;
    [SerializeField] private int _maxNewCubes = 6;

    [SerializeField] private float _minForceForExplosion = 10f;
    [SerializeField] private float _maxForceForExplosion = 25f;

    private Vector3 _originalScale;

    private static float _splitChance = 1f;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        float random = Random.value;
        Debug.Log($"rand={random}, splitChance={_splitChance}");

        if (random <= _splitChance)
        {
            SplitCube();
            _splitChance *= 0.5f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SplitCube()
    {
        Vector3 explosionCenter = transform.position;

        if (explosionEffect != null)
        {
            ParticleSystem ps = Instantiate(explosionEffect, explosionCenter, Quaternion.identity);
            ps.Play();

            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }

        int count = Random.Range(_minNewCubes, _maxNewCubes + 1);

        List<GameObject> newCubes = new List<GameObject>();

        Vector3 halfSize = _originalScale * 0.5f;

        Rigidbody originalRb = GetComponent<Rigidbody>();

        float originalMass;

        if (originalRb != null)
        {
            originalMass = originalRb.mass;
        }
        else
        {
            originalMass = 1f;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-halfSize.x, halfSize.x),
                Random.Range(-halfSize.y, halfSize.y),
                Random.Range(-halfSize.z, halfSize.z)
                );

            Vector3 position = transform.position + randomOffset;

            GameObject newCube = Instantiate(CubePrefab, position, Quaternion.identity);
            newCube.transform.localScale = _originalScale * 0.5f;

            Renderer renderer = newCube.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color = GetRandomColor();
            }

            Rigidbody rb = newCube.GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = newCube.AddComponent<Rigidbody>();
            }

            rb.mass = originalMass / count;
            newCubes.Add(newCube);
        }

        foreach (var cube in newCubes)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (cube.transform.position - transform.position).normalized;
                direction.y += 1f;
                direction.Normalize();

                float forceMagnitude = Random.Range(_minForceForExplosion, _maxForceForExplosion);
                rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
