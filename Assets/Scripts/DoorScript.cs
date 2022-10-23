using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private GameObject target;
    private Vector3 pos;

    public float distance = .7f;

    private void Start()
    {
        pos = this.transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(pos, target.transform.position) < distance)
            StartCoroutine(DoorDrop());
        else
            StartCoroutine(DoorUp());
    }

    IEnumerator DoorDrop()
    {
        for (float i = 1; i >= pos.y - 1; i -= 0.1f)
        {
            transform.position = new Vector3(pos.x, i, pos.z);
            yield return null;
        }
    }

    IEnumerator DoorUp()
    {
        for (float i = pos.y; i <= 1; i += 0.1f)
        {
            transform.position = new Vector3(pos.x, i, pos.z);
            yield return null;
        }
    }
}
