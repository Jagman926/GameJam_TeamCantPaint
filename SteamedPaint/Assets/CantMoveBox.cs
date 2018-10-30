using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMoveBox : MonoBehaviour
{
    private Rigidbody2D movableRB;
    [SerializeField]
    private GameObject touchingMovableObject;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "HeavyMove")
        {
            touchingMovableObject = col.gameObject;
            movableRB = touchingMovableObject.GetComponent<Rigidbody2D>();
			movableRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "HeavyMove")
        {
            movableRB.constraints = RigidbodyConstraints2D.None;
			movableRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            touchingMovableObject = null;
            movableRB = null;
        }
    }
}
