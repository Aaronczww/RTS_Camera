﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHelper : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
    }
}
