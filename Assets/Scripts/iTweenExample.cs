using UnityEngine;

public class iTweenExample : MonoBehaviour
{
    private void Start()
    {
        iTween.MoveFrom(gameObject, iTween.Hash(
            "y", 0f,
            "delay", 1f,
            "time", 0.5f,
            "easetype", iTween.EaseType.easeOutBounce
        ));
        
        // iTween.RotateTo(gameObject, iTween.Hash(
        //     "y", 90f,
        //     "delay", 1f,
        //     "time", 2f,
        //     "easetype", iTween.EaseType.easeInOutExpo
        // ));

        // iTween.RotateTo(gameObject, iTween.Hash(
        //     "x", -90f,
        //     "delay", 2f,
        //     "time", 1f,
        //     "easetype", iTween.EaseType.easeOutBounce
        // ));

        // iTween.ColorTo(gameObject, iTween.Hash(
        //     "r", 0f,
        //     "g", 0.5f,
        //     "b", 1f,
        //     "delay", 3f,
        //     "time", 1f,
        //     "easetype", iTween.EaseType.easeInOutExpo
        // ));

        // iTween.MoveTo(gameObject, iTween.Hash(
        //     "y", -2f,
        //     "delay", 4f,
        //     "time", 0.5f,
        //     "easetype", iTween.EaseType.easeOutBounce
        // ));

        // iTween.ShakePosition(Camera.main.gameObject, iTween.Hash(
        //     "y", 0.2f,
        //     "delay", 4.3f,
        //     "time", 0.3f
        // ));
    }
}
