using UnityEngine;

public class PipeGapAdjuster : MonoBehaviour
{
    [Tooltip("Extra units to push top and bottom pipes apart")]
    public float extraGap = 2f;

    void Start()
    {
        // Push child pipes apart to widen the gap
        foreach (Transform child in transform)
        {
            if (child.localPosition.y > 0)
            {
                // Top pipe — move it up
                child.localPosition += new Vector3(0, extraGap, 0);
            }
            else if (child.localPosition.y < 0)
            {
                // Bottom pipe — move it down
                child.localPosition -= new Vector3(0, extraGap, 0);
            }
        }
    }
}
