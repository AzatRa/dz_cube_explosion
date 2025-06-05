using System;
using System.Collections;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    [SerializeField] private int _minNewCubes = 2;
    [SerializeField] private int _maxNewCubes = 6;

    private float _splitChance = 1f;
    private int _halver = 2;

    private event Action<Cube> OnHit;

    private void OnEnable()
    {
        OnHit += HandleHit;
    }

    private void OnDisable()
    {
        OnHit -= HandleHit;
    }

    public void Hit(Cube cube)
    {
        OnHit?.Invoke(cube);
    }

    private void HandleHit(Cube cube)
    {
        StartCoroutine(HitCoroutine(cube));
    }

    private IEnumerator HitCoroutine(Cube cube)
    {
        float random = UnityEngine.Random.value;
        Debug.Log($"random={random}, splitChance={_splitChance}");

        if (random <= _splitChance)
        {
            _splitChance /= _halver;

            float mass = cube.GetComponent<Rigidbody>().mass;

            if (_spawner != null && cube.ColorChanger != null)
                _spawner.Spawn(cube.transform.position, cube.transform.localScale, _minNewCubes, _maxNewCubes,
                                   cube.ColorChanger, mass);

            yield return new WaitForSeconds(0.2f);

            if (_exploder != null && cube.TryGetComponent(out Rigidbody rigidbody))
            {
                _exploder.Explode(rigidbody);
            }
        }

        Destroy(cube.gameObject);
    }
}
