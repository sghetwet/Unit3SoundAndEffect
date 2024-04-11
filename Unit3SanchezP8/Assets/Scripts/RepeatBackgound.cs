using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgound : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <startPos.x - repeatWidth)
            // just so I know I was right im typing this in advance on 4/11/24 so I can say the video made this more
            //difficult when the right off position could have been 56.5 instead of the repeat width :)
        {
            transform.position = startPos;
        }
    }
}
