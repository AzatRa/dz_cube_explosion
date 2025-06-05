using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    
    private Rigidbody _rigidbody;
    
    public ColorChanger ColorChanger => _colorChanger;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ChangeColor()
    {
        if (_colorChanger != null)
        {
            _colorChanger.ChangeColor(gameObject);
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

    public void SetColorChanger(ColorChanger colorChanger)
    {
        this._colorChanger = colorChanger;
    }
}