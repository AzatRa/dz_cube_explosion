using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(GameObject cube)
    {
        Renderer renderer = cube.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}