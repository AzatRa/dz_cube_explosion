using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _minForce = 10f;
    [SerializeField] private float _maxForce = 25f;

    public void Explode(Rigidbody rigidbody)
    {
        if (rigidbody == null)
            return;

        Vector3 direction = (gameObject.transform.position - transform.position).normalized;
        float forceMag = Random.Range(_minForce, _maxForce);
        rigidbody.AddForce(direction * forceMag + Vector3.up * 1f, ForceMode.Impulse);
    }
}