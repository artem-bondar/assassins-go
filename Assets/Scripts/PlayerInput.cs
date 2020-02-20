using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float h;
    public float v;

    public bool inputEnabled = false;

    public void GetKeyInput()
    {
        if (inputEnabled)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
    }
}
