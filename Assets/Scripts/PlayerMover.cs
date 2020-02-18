using System.Collections;

using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Vector3 destination;

    public bool isMoving = false;

    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public float moveSpeed = 1.5f;
    public float iTweenDelay = 0f;

    private void Start()
    {
        Move(new Vector3(2f, 0f, 0f), 1f);
        Move(new Vector3(4f, 0f, 0f), 3f);
        Move(new Vector3(4f, 0f, 2f), 5f);
        Move(new Vector3(4f, 0f, 4f), 7f);
    }

    private IEnumerator MoveRoutine(Vector3 destinationPosition, float delayTime)
    {
        isMoving = true;
        destination = destinationPosition;

        yield return new WaitForSeconds(delayTime);

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destinationPosition.x,
            "y", destinationPosition.y,
            "z", destinationPosition.z,
            "delay", iTweenDelay,
            "easetype", easeType,
            "speed", moveSpeed
        ));

        while (Vector3.Distance(destinationPosition, transform.position) > 0.01f)
        {
            yield return null;
        }

        iTween.Stop(gameObject);

        transform.position = destinationPosition;

        isMoving = false;
    }

    public void Move(Vector3 destinationPosition, float delayTime = 0.25f) =>
        StartCoroutine(MoveRoutine(destinationPosition, delayTime));
}
