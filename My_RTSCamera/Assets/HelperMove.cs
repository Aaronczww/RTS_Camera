using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMove : MonoBehaviour {

    //float moveMentSpeed;
    //bool moveKeyboarInput_bool;
    //private void Start()
    //{
    //    moveMentSpeed = GameObject.FindWithTag("MainCamera").GetComponent<RTS_Camera>().moveMentSpeed;
    //    moveKeyboarInput_bool = GameObject.FindWithTag("MainCamera").GetComponent<RTS_Camera>().moveKeyboarInput_bool;

    //}
    //// Update is called once per frame
    //void Update () {
    //    if(moveKeyboarInput_bool)
    //    {
    //        var h = Input.GetAxisRaw("Horizontal");
    //        var v = Input.GetAxisRaw("Vertical");
    //        if (Mathf.Abs(h) > Mathf.Epsilon || Mathf.Abs(v) > Mathf.Epsilon)
    //        {
    //            Vector3 s = new Vector3(h, 0, v);
    //            s = s.normalized;
    //            s = s * moveMentSpeed * Time.deltaTime;
    //            Debug.LogWarning(s);
    //            transform.Translate(s);
    //        }
    //    }
    //}
}
