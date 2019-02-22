﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public AnimationCurve curve;
    public bool inverted;

    private Vector3 gemPosition;

    // Start is called before the first frame update
    void Start()
    {
        curve = new AnimationCurve(new Keyframe(0,0), new Keyframe(0.8f, 0.2f));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        gemPosition = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        if (inverted)
            transform.position = new Vector3(transform.position.x, gemPosition.y - curve.Evaluate(Time.time), transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, gemPosition.y + curve.Evaluate(Time.time), transform.position.z);
    }

}
