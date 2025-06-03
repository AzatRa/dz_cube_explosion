using UnityEngine;

public class Splitter : MonoBehaviour
{
    public Exploder Exploder;
    public ExplodeEffector ExplodeEffector;
    public Spawner Spawner;
    public ColorChanger ColorChanger;

    [SerializeField] private int _minNewCubes = 2;
    [SerializeField] private int _maxNewCubes = 6;
    [SerializeField] private float _minForce = 10f;
    [SerializeField] private float _maxForce = 25f;

    public void Split(CubeController cube)
    {
        if (ExplodeEffector != null)
            ExplodeEffector.Explode(cube.transform.position);

        if (Exploder != null)
            Exploder.Explode(cube.gameObject, _minForce, _maxForce);

        float originalMass = cube.GetComponent<Rigidbody>().mass;

        if (Spawner != null && cube.ColorChanger != null)
            Spawner.SpawnCubes(cube.transform.position, cube.transform.localScale,
                               _minNewCubes, _maxNewCubes,
                               cube.ColorChanger,
                               this, originalMass);

        Destroy(cube.gameObject);
    }
}