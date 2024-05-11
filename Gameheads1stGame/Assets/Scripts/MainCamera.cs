using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (-player.forward + player.position) + (-player.forward * 1.5f + Vector3.up) * 4;
        transform.eulerAngles = new Vector3(20, player.eulerAngles.y, 0);
    }
}
