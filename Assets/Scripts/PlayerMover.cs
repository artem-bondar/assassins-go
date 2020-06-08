using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public Vector3 destination;

    public bool isMoving = false;

    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public float moveSpeed = 1.5f;
    public float iTweenDelay = 0f;

    private Board board;

    private void Awake() => board = Object.FindObjectOfType<Board>().GetComponent<Board>();

    private void Start() => UpdateBoard();

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

        UpdateBoard();
    }

    private void UpdateBoard()
    {
        if (board != null)
        {
            board.UpdatePlayerNode();
        }
    }

    public void Move(Vector3 destinationPosition, float delayTime = 0.25f)
    {
        if (board != null)
        {
            Node targetNode = board.FindNodeAt(destinationPosition);

            if (targetNode != null && board.PlayerNode.LinkedNodes.Contains(targetNode))
            {
                StartCoroutine(MoveRoutine(destinationPosition, delayTime));
            }
        }
    }

    public void MoveLeft()
    {
        Vector3 newPosition = transform.position + new Vector3(-Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    public void MoveRight()
    {
        Vector3 newPosition = transform.position + new Vector3(Board.spacing, 0f, 0f);
        Move(newPosition, 0);
    }

    public void MoveForward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, Board.spacing);
        Move(newPosition, 0);
    }

    public void MoveBackward()
    {
        Vector3 newPosition = transform.position + new Vector3(0f, 0f, -Board.spacing);
        Move(newPosition, 0);
    }
}
