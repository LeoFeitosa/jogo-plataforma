using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform[] bgs;
    public float[] paralaxVel;
    public float smooth;
    public Transform cam;

    private Vector3 previewCam;

    // Start is called before the first frame update
    void Start()
    {
        previewCam = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<bgs.Length; i++)
        {
            float paralax = (previewCam.x - cam.position.x) * paralaxVel [i];
            float targetPosX = bgs[i].position.x - paralax;
            Vector3 targetPos = new Vector3(targetPosX, bgs[i].position.y, bgs[i].position.y);
            bgs[i].position = Vector3.Lerp(bgs[i].position, targetPos, smooth * Time.deltaTime);
        }
        previewCam = cam.position;
    }
}
