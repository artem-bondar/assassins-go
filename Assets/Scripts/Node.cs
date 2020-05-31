using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector2 coordinate;
    public Vector2 Coordinate => Utility.Vector2Round(coordinate);

    private List<Node> neighbourNodes = new List<Node>();
    public List<Node> NeighbourNodes => neighbourNodes;

    private Board board;

    private bool isInitialized = false;

    public GameObject geometry;
    public GameObject linkPrefab;

    public float scaleTime = 0.3f;
    public float delay = 0.1f;

    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    public bool autoRun = false;

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

        if (autoRun)
        {
            InitNode();
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
            LinkNode(node);
            node.InitNode();
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
        }
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
