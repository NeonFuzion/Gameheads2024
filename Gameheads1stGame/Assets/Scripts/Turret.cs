using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float timer, rotateSpd;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform spawner;

    Transform target;
    float curTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (!IsAggro()) return;
        Vector3 targDir = target.position - transform.position;
        float singleStep = rotateSpd * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targDir, singleStep, 0);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (curTime < timer) return;
        curTime = 0;
        Instantiate(prefabBullet, spawner.position, spawner.rotation);
    }

    bool IsAggro()
    {
        return target != null;
    }

    bool IsPlayer(Collider col)
    {
        Transform parent = col.transform;
        while (parent.parent) parent = parent.parent;
        return parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!IsPlayer(col)) return;
        target = col.transform;
    }

    private void OnTriggerExit(Collider col)
    {
        if (!IsPlayer(col)) return;
        target = null;
    }
}
