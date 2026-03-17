using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material material;
    private bool stopped = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (stopped) return;

        float offset = Time.time * scrollSpeed;
        material.mainTextureOffset = new Vector2(offset, 0);
    }

    public void StopScrolling()
    {
        stopped = true;
    }
}
