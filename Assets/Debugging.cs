using UnityEngine;

public class Debugging : MonoBehaviour
{
    void Update()
    {
        float x = GlobalVariables.barrierX;
        float y = GlobalVariables.barrierY;
        float z = GlobalVariables.barrierZ;

        Vector3 c0 = new Vector3(0, 0, 0);  // origin corner
        Vector3 c1 = new Vector3(x, 0, 0);
        Vector3 c2 = new Vector3(x, 0, z);
        Vector3 c3 = new Vector3(0, 0, z);
        Vector3 c4 = new Vector3(0, y, 0);
        Vector3 c5 = new Vector3(x, y, 0);
        Vector3 c6 = new Vector3(x, y, z);  // opposite corner
        Vector3 c7 = new Vector3(0, y, z);

        Color col = Color.white;

        // Bottom rectangle
        Debug.DrawLine(c0, c1, col);
        Debug.DrawLine(c1, c2, col);
        Debug.DrawLine(c2, c3, col);
        Debug.DrawLine(c3, c0, col);

        // Top rectangle
        Debug.DrawLine(c4, c5, col);
        Debug.DrawLine(c5, c6, col);
        Debug.DrawLine(c6, c7, col);
        Debug.DrawLine(c7, c4, col);

        // Vertical edges
        Debug.DrawLine(c0, c4, col);
        Debug.DrawLine(c1, c5, col);
        Debug.DrawLine(c2, c6, col);
        Debug.DrawLine(c3, c7, col);

    }
}
