using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Splatter : MonoBehaviour
{
    public List<Sprite> sprites;
    public Color color;
    private Image sprite;
    private float time = 0.0f;
    private float lifetime = 1.0f;

    private void Start()
    {
        time = 0.0f;
        sprite = GetComponent<Image>();
        sprite.sprite = sprites[Random.Range(0, sprites.Count)];
        sprite.color = color;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
