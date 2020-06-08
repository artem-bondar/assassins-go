using System.Collections.Generic;

using UnityEngine;

public class Board : MonoBehaviour
{
    public static float spacing = 2f;

    public static readonly Vector2[] directions = 
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f, spacing),
        new Vector2(0f, -spacing)
    };

    private List<Node> allNodes = new List<Node>();
    public List<Node> AllNodes { get => allNodes; }

    private Node playerNode;
    public Node PlayerNode => playerNode;

    private PlayerMover player;

    private Node goalNode;
    public Node GoalNode => goalNode;

    public GameObject goalPrefab;

    public float drawGoalTime = 2f;
    public float drawGoalDelay = 2f;
    
    public iTween.EaseType drawGoalEaseType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();

        goalNode = FindGoalNode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);

        if (playerNode != null)
        {
            Gizmos.DrawSphere(playerNode.transform.position, 0.2f);
        }
    }

    private Node FindGoalNode() => allNodes.Find(n => n.isLevelGoal);

    public void GetNodeList()
    {
        Node[] nodesList = GameObject.FindObjectsOfType<Node>();
        allNodes = new List<Node>(nodesList);
    }

    public Node FindNodeAt(Vector3 position)
    {
        Vector2 boardCoordinate = Utility.Vector2Round(new Vector2(position.x, position.z));
        return allNodes.Find(n => n.Coordinate == boardCoordinate);
    }

    public Node FindPlayerNode()
    {
        if (player != null && !player.isMoving)
        {
            return FindNodeAt(player.transform.position);
        }

        return null;
    }

    public void UpdatePlayerNode() => playerNode = FindPlayerNode();

    public void DrawGoal()
    {
        if (goalPrefab != null && goalNode != null)
        {
            GameObject goalInstance = Instantiate(goalPrefab, goalNode.transform.position, Quaternion.identity);
            
            iTween.ScaleFrom(goalInstance, iTween.Hash(
                "scale", Vector3.zero,
                "time", drawGoalTime,
                "delay", drawGoalDelay,
                "easetype", drawGoalEaseType
            ));
        }
    }
}
