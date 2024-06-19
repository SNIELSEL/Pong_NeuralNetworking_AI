using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerMovement : MonoBehaviour
{
    public Vector3 moveSpeed;
    public Vector3 posR;

    public Transform[] clamps;

    public void Awake()
    {
        posR.x = transform.position.x;

        clamps[0] = this.gameObject.transform.parent.transform.GetChild(10);
        clamps[1] = this.gameObject.transform.parent.transform.GetChild(11);
    }

    public void Update()
    {
        if(gameObject.tag == "BouncerL")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.position += moveSpeed * Time.deltaTime * 475;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position -= moveSpeed * Time.deltaTime * 475;
            }

            if(transform.position.y > 1250)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

            if (transform.position.y < 200)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

        }

        if (gameObject.tag == "BouncerR")
        {
            if (Input.GetKey(KeyCode.Keypad6))
            {
                gameObject.transform.position += moveSpeed * Time.deltaTime * 475;
            }

            if (Input.GetKey(KeyCode.Alpha0))
            {
                transform.position -= moveSpeed * Time.deltaTime * 475;
            }

            if (transform.position.y > 1280)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

            if (transform.position.y < 200)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

        }

        if (gameObject.tag == "AgentL")
        {
            if (transform.position.y > 1250)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

            if (transform.position.y < 200)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

        }

        if (gameObject.tag == "AgentR")
        {
            if (transform.position.y > 1280)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

            if (transform.position.y < 200)
            {
                posR.y = Mathf.Clamp(transform.position.y, clamps[0].position.y, clamps[1].position.y);
                transform.position = posR;
            }

        }
    }
}
