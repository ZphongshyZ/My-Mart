using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BFSFindingPath : MyBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 1f;

    private List<Vector2> directions = new List<Vector2>() { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };

    private bool isCanGo(Vector2 direction, Vector2 startPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, this.rayDistance);
        //Debug.DrawRay(startPoint, direction * rayDistance, Color.red);
        return hit.collider == null;
    }

    private bool isValidDistance(Vector2 nextPos)
    {
        return nextPos.x <= 50f && nextPos.x >= -50f && nextPos.y <= 15f && nextPos.y >= -15f;
    }

    private bool isValidDirection(Vector2 currentPos, Vector2 nextPos, Vector2 targetPos)
    {
        if (currentPos.x != -13f) return true;
        bool targetDir = targetPos.x - -13f >= 0f;
        return (nextPos.x - -13f >= 0f) == targetDir;
    }

    public Queue<Vector2> PathBFS(Vector2 startPos, Vector2 targetPos)
    {
        Queue<Vector2> queuePath = new Queue<Vector2>();
        List<(Vector2 current, Vector2 parent)> parentList = new List<(Vector2, Vector2)>();
        List<Vector2> visited = new List<Vector2>();
        queuePath.Enqueue(startPos);
        visited.Add(startPos);

        while (queuePath.Count > 0)
        {
            Vector2 currentPos = Vector2Int.CeilToInt(queuePath.Dequeue());
            Vector2Int targetPosInt = Vector2Int.CeilToInt(targetPos);

            if (Vector2Int.CeilToInt(currentPos) == targetPosInt)
            {
                return BuildPath(parentList, targetPos, startPos);
            }

            foreach (Vector2 direction in this.directions)
            {
                Vector2 nextPos = (currentPos + direction);
                if (!visited.Contains(nextPos) && this.isValidDistance(nextPos) && this.isValidDirection(currentPos, nextPos, targetPosInt) && this.isCanGo(direction, nextPos))
                {
                    queuePath.Enqueue(nextPos);
                    visited.Add(nextPos);
                    parentList.Add((nextPos, currentPos));
                }
            }
        }
        return null;
    }

    private Queue<Vector2> BuildPath(List<(Vector2 current, Vector2 parent)> parentList, Vector2 targetPos, Vector2 startPos)
    {
        Queue<Vector2> path = new Queue<Vector2>();
        Vector2 currentPos = Vector2Int.CeilToInt(targetPos);

        // Truy ngược lại từ target đến start
        while (Vector2Int.CeilToInt(currentPos) != Vector2Int.CeilToInt(startPos))
        {
            path.Enqueue(currentPos);
            currentPos = parentList.Find(p => p.current == currentPos).parent; // Tìm parent của currentPos trong danh sách
        }

        path.Enqueue(startPos); // Thêm điểm xuất phát vào đường đi
        return ReverseQueue(path); // Đảo ngược queue để đường đi từ start đến target
    }

    private Queue<Vector2> ReverseQueue(Queue<Vector2> queue)
    {
        Stack<Vector2> stack = new Stack<Vector2>(queue);
        return new Queue<Vector2>(stack); // Đảo ngược queue bằng stack
    }
}