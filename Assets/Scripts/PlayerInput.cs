using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float h;
    public float H { get => h; }

    private float v;
    public float V { get => v; }

    private bool inputEnabled = false;
    public bool InputEnabled
    {
        get => inputEnabled;
        set => inputEnabled = value;
    }

    public void GetKeyInput()
    {
        if (inputEnabled)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
    }
}
