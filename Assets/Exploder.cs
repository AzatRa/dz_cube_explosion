using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(GameObject gameObject, float minForce, float maxForce)
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
            return;

        Vector3 direction = (gameObject.transform.position - transform.position).normalized;
        float forceMag = Random.Range(minForce, maxForce);
        rigidbody.AddForce(direction * forceMag + Vector3.up * 1f, ForceMode.Impulse);
    }
}