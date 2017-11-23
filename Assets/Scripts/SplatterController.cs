using UnityEngine;

public class SplatterController : MonoBehaviour {

    public Splatter splatter;

    public void Splat(float pos, Color color)
    {
        int numSplats = Random.Range(1, 3);
        for (int i = 0; i < numSplats; i++)
        {
            Vector3 splatPos = new Vector3(pos + Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0);
            Splatter newSplat = Instantiate(splatter, gameObject.transform);
            newSplat.transform.localPosition = splatPos;
            newSplat.color = new Color(color.r - Random.Range(0, -50f / 255f), color.g - Random.Range(0, -50f / 255f), color.b - Random.Range(0, -50f / 255f));
            newSplat.gameObject.SetActive(true);
        }
    }
}
