using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CubePrefab;

    private float _halver = 2f;

    public void SpawnCubes(Vector3 position, Vector3 originalScale, int minCount, int maxCount, ColorChanger colorChanger, 
        Splitter splitter, float originalMass)
    {
        int count = Random.Range(minCount, maxCount + 1);
        Vector3 newScale = originalScale / _halver;

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(
                Random.Range(-newScale.x / _halver, newScale.x / _halver),
                Random.Range(-newScale.y / _halver, newScale.y / _halver),
                Random.Range(-newScale.z / _halver, newScale.z / _halver)
            );

            GameObject newCube = Instantiate(CubePrefab, position + offset, Quaternion.identity);
            newCube.transform.localScale = newScale;

            CubeController cubeController = newCube.GetComponent<CubeController>();
            if (cubeController != null)
            {
                cubeController.ColorChanger = colorChanger;
                cubeController.Splitter = splitter;

                float newMass = originalMass / _halver;
                cubeController.SetMass(newMass);

                if (cubeController.ColorChanger != null)
                {
                    cubeController.ChangeColor();
                }
            }
        }
    }
}
