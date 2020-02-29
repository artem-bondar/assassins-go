using UnityEngine;

public class Utility
{
    public static Vector3 Vector3Round(Vector3 inputVector) =>
        new Vector3(Mathf.Round(inputVector.x), Mathf.Round(inputVector.y), Mathf.Round(inputVector.x));

    public static Vector2 Vector2Round(Vector2 inputVector) =>
        new Vector2(Mathf.Round(inputVector.x), Mathf.Round(inputVector.y));
}
