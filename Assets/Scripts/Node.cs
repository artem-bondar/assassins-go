using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector2 coordinate;
    public Vector2 Coordinate => Utility.Vector2Round(coordinate);

    private List<Node> neighbourNodes = new List<Node>();
    public List<Node> NeighbourNodes => neighbourNodes;

    private List<Node> linkedNodes = new List<Node>();
    public List<Node> LinkedNodes => linkedNodes;

    private Board board;

    private bool isInitialized = false;

    public GameObject geometry;
    public GameObject linkPrefab;

    public float scaleTime = 0.3f;
    public float delay = 0.1f;

    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    public LayerMask obstacleLayer;

    private void Awake()
    {
        board = Object.FindObjectOfType<Board>();
        coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;
        }

        if (board != null)
        {
            neighbourNodes = FindNeighbours(board.AllNodes);
        }
    }

    private void InitNeighbours() => StartCoroutine(InitNeighboursRoutine());

    private IEnumerator InitNeighboursRoutine()
    {
        yield return new WaitForSeconds(delay);

        foreach (Node node in neighbourNodes)
        {
            if (!linkedNodes.Contains(node))
            {
                Obstacle obstacle = FindObstacle(node);

                if (obstacle == null)
                {
                    LinkNode(node);
                    node.InitNode();
                }
            }
        }
    }

    private void LinkNode(Node targetNode)
    {
        if (linkPrefab != null)
        {
            GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            Link link = linkInstance.GetComponent<Link>();

            if (link != null)
            {
                link.DrawLink(transform.position, targetNode.transform.position);
            }

            if (!linkedNodes.Contains(targetNode))
            {
                linkedNodes.Add(targetNode);
            }

            if (!targetNode.LinkedNodes.Contains(this))
            {
                targetNode.LinkedNodes.Add(this);
            }
        }
    }

    private Obstacle FindObstacle(Node targetNode)
    {
        Vector3 checkDirection = targetNode.transform.position - transform.position;

        if (Physics.Raycast(transform.position, checkDirection, out RaycastHit raycastHit, Board.spacing + 0.1f, obstacleLayer))
        {
            // Debug.Log($"NODE FindObstacle: Hit an obstacle from {this.name} to {targetNode.name}");
            return raycastHit.collider.GetComponent<Obstacle>();
        }

        return null;
    }

    public void ShowGeometry()
    {
        if (geometry != null)
        {
            iTween.ScaleTo(geometry, iTween.Hash(
                "time", scaleTime,
                "scale", Vector3.one,
                "easetype", easeType,
                "delay", delay
            ));
        }
    }

    public List<Node> FindNeighbours(List<Node> nodes)
    {
        List<Node> nodesList = new List<Node>();

        foreach (Vector2 direction in Board.directions)
        {
            Node foundNeighbour = nodes.Find(n => n.Coordinate == Coordinate + direction);

            if (foundNeighbour != null && !nodesList.Contains(foundNeighbour))
            {
                nodesList.Add(foundNeighbour);
            }
        }

        return nodesList;
    }

    public void InitNode()
    {
        if (!isInitialized)
        {
            ShowGeometry();
            InitNeighbours();
            isInitialized = true;
        }
    }
}
