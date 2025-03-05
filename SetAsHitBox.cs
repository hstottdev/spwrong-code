using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsHitBox : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<SpriteRenderer>().size = boxCollider.size;
        transform.localPosition = boxCollider.offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
