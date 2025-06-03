using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Splitter Splitter;
    public ColorChanger ColorChanger;

    private static float _splitChance = 1f;
    private int _halver = 2;

    private void OnMouseDown()
    {
        float random = Random.value;
        Debug.Log($"rand={random}, splitChance={_splitChance}");

        if (random <= _splitChance)
        {
            if (Splitter != null)
            {
                Splitter.Split(this);
            }
            _splitChance /= _halver;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeColor()
    {
        if (ColorChanger != null)
        {
            ColorChanger.ChangeColor(gameObject);
        }
    }

    public void SetMass(float mass)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.mass = mass;
        }
    }
}