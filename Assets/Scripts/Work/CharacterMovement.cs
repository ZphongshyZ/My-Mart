using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MyBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Animator animator;

    protected override void LoadComponents()
    {
        this.animator = this.transform.parent.GetComponentInChildren<Animator>();
    }

    // Coroutine di chuyển nhân vật theo đường đi trong Queue
    public IEnumerator MoveAlongPath(Queue<Vector2> path)
    {
        if (path == null) yield break;
        if (path.Count <= 2)
        {
            Destroy(this.transform.parent);
            yield break;
        }
        while (path.Count > 0)
        {
            Vector2 nextPos = path.Dequeue();
            Vector2 direction = nextPos - (Vector2)this.transform.parent.position; // Hướng đi từ vị trí hiện tại đến điểm tiếp theo

            // Truyền hướng vào Animator
            this.animator.SetFloat("X", direction.x);
            this.animator.SetFloat("Y", direction.y);
            this.animator.SetBool("isMoving", true);

            // Di chuyển nhân vật đến vị trí tiếp theo
            while ((Vector2)this.transform.parent.position != nextPos)
            {
                this.transform.parent.position = Vector2.MoveTowards(this.transform.parent.position, nextPos, Time.deltaTime * speed);
                yield return null;
            }
        }
        this.animator.SetFloat("X", 0f);
        this.animator.SetFloat("Y", 0f);
        this.animator.SetBool("isMoving", false);
        yield return null;
    }
}
