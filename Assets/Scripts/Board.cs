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

    private void Awake() => GetNodeList();

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
}
