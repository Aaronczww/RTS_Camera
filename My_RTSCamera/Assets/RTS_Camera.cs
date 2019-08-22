using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Camera : MonoBehaviour {
    public string horizontalAxisName;
    public string verticalAxisName;
    public float keyboardMovementSpeed;
    public bool moveKeyboarInput_bool;

    public float mouseMovementSpeed;

    public bool edgeInput_bool;
    public bool panningWithMouse_bool;
    public bool limitMovement_bool;

    public bool rotationKeyboardInput_bool;
    public bool rotationMouseInput_bool;

    public bool autoHeight;
    public float heightDampening;

    public bool keyBoardZooming_bool;
    public float keyBoardSensity;

    public bool scrollwheelZooming_bool;
    public float scrollwheelSensity;

    public float maxHeight;
    public float minHeight;

    public KeyCode ZoomIn = KeyCode.Z;
    public KeyCode ZoomOut = KeyCode.X;

    public KeyCode RotateLeft = KeyCode.Q;
    public KeyCode RotateRight = KeyCode.E;
    public KeyCode RotateUp = KeyCode.R;
    public KeyCode RotateDown = KeyCode.F;

    public float KeyboardRotateSensity;

    public float MouseRotateSensity;

    public LayerMask groundMask;

    private GameObject follower;

    public string rotateXAxisName;
    public string rotateYAxisName;

    private Vector3 LookAt;

    public Vector3 Boundary;

    private Vector3 _moveVector;

    public enum MaskLayer
    {
        Nothing = 0 ,
        Everything = -1,
        Defalut = 1 << 0,
        TransparentFX = 1 << 1,
        IgnoreCast = 1 << 2,
        Water = 1 << 4,
        UI = 1 << 5,
        PostProcessing = 1 << 8,
    }

    public MaskLayer Groundmask;

    private float Rotation;
    private float Tilt;

    private Camera camera;

    float Distance;

    private void Awake()
    {
        follower = GameObject.Find("FollowHelper");

        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        LookAt = Vector3.zero;

        Distance = 16.0f;

        GetComponent<Camera>().cullingMask = 1 << groundMask;

        Rotation = 0f;

        Tilt = 45f;

    }
    private void Update()
    {
        camera.cullingMask =(int) Groundmask;

        if (moveKeyboarInput_bool)
        {
            var h = Input.GetAxisRaw(horizontalAxisName);
            var v = Input.GetAxisRaw(verticalAxisName);
            if (Mathf.Abs(h) > Mathf.Epsilon || Mathf.Abs(v) > Mathf.Epsilon)
            {
                Vector3 s = new Vector3(h, 0, v);
                s = s.normalized;
                s = s * keyboardMovementSpeed * Time.deltaTime;
                LookAt += Quaternion.Euler(0, Rotation, 0) * s;

                follower.transform.position = Vector3.Lerp(follower.transform.position, LookAt, Time.deltaTime * keyboardMovementSpeed);
            }
        }
        if (scrollwheelZooming_bool)
        {
            float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollwheelSensity * 10;

            if (wheel > 0 && Distance > minHeight)
            {
                Distance -= wheel;
                transform.Translate(Vector3.forward * wheel);
            }
            else if(wheel < 0 && Distance < maxHeight)
            {
                Distance -= wheel;
                transform.Translate(Vector3.forward * wheel);
            }
        }

        if (keyBoardZooming_bool)
        {
            if (Input.GetKey(ZoomIn) && Distance > minHeight)
            {
                float wheel = Time.deltaTime * keyBoardSensity * 10;
                Distance -= wheel;
                transform.Translate(Vector3.forward * wheel);
            }

            if (Input.GetKey(ZoomOut) && Distance < maxHeight)
            {
                float wheel = Time.deltaTime * keyBoardSensity * -10;
                Distance -= wheel;
                transform.Translate(Vector3.forward * wheel);
            }
        }

        if (rotationKeyboardInput_bool)
        {
            Vector3 centerPoint = follower.transform.position;
            if (Input.GetKey(RotateRight))
            {
                Rotation += KeyboardRotateSensity * Time.deltaTime;
            }
            if (Input.GetKey(RotateLeft))
            {
                Rotation -= KeyboardRotateSensity * Time.deltaTime;

            }
            if (Input.GetKey(RotateUp))
            {
                Tilt += KeyboardRotateSensity * Time.deltaTime;

            }
            if (Input.GetKey(RotateDown))
            {
                Tilt -= KeyboardRotateSensity * Time.deltaTime;
            }

            var rotation = Quaternion.Euler(Tilt, Rotation, 0);
            var v = new Vector3(0.0f, 0.0f, -Distance);
            var position = rotation * v + follower.transform.position;

            transform.rotation = rotation;
            //follower.transform.rotation = rotation;
            transform.position = position;
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (panningWithMouse_bool && Input.GetKey(KeyCode.LeftShift))
            {
                    var panX = Input.GetAxisRaw("Mouse X");
                    var panZ = Input.GetAxisRaw("Mouse Y");
                    if (Mathf.Abs(panX) > Mathf.Epsilon || Mathf.Abs(panZ) > Mathf.Epsilon)
                    {
                        Vector3 s = new Vector3(panX, 0, panZ);
                        s = s.normalized;
                        s = s * mouseMovementSpeed * Time.deltaTime;
                        LookAt += Quaternion.Euler(0, Rotation, 0) * s;

                        follower.transform.position = Vector3.Lerp(follower.transform.position, LookAt, Time.deltaTime * mouseMovementSpeed);
                }
            }

            else if (rotationMouseInput_bool)
            {
                float axisX = Input.GetAxisRaw(rotateXAxisName);

                float axisY = Input.GetAxisRaw(rotateYAxisName);

                Rotation += axisX * MouseRotateSensity * Time.deltaTime;

                Tilt -= axisY * MouseRotateSensity * Time.deltaTime;

                var rotation = Quaternion.Euler(Tilt, Rotation, 0);
                var v = new Vector3(0.0f, 0.0f, -Distance);
                var position = rotation * v + follower.transform.position;

                transform.rotation = rotation;
                transform.position = position;
            }
        }

        if (edgeInput_bool)
        {
                if (Input.mousePosition.y > (Screen.height - 4))
                {
                    _moveVector += new Vector3(0, 0, Time.deltaTime * mouseMovementSpeed);
                }
                else if (Input.mousePosition.y < 4)
                {
                    _moveVector += new Vector3(0, 0, -Time.deltaTime * mouseMovementSpeed);
                }

                if (Input.mousePosition.x > (Screen.width - 4))
                {
                    _moveVector += new Vector3(mouseMovementSpeed * Time.deltaTime, 0, 0);
                }
                else if (Input.mousePosition.x < 4)
                {
                    _moveVector += new Vector3(-mouseMovementSpeed * Time.deltaTime, 0, 0);
                }
                LookAt += Quaternion.Euler(0, Rotation, 0) * _moveVector;
                follower.transform.position = Vector3.Lerp(follower.transform.position, LookAt, Time.deltaTime * mouseMovementSpeed);
                _moveVector = Vector3.zero;
        }

        if (limitMovement_bool)
        {
            follower.transform.position = new Vector3(Mathf.Clamp(follower.transform.position.x, -Boundary.x, Boundary.x),
            Mathf.Clamp(follower.transform.position.y, -Boundary.y, Boundary.y),
            Mathf.Clamp(follower.transform.position.z, -Boundary.z, Boundary.z));
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(2 * Boundary.x,2 * Boundary.y,2 * Boundary.z));
    }

}
