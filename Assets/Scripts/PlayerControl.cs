using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public           float _speed    = 5f;
    private readonly float _rotation = 35f;
    private          float _xmin, _xmax;
    private const float EDGE_SPACE = 0.8f;

    private void Start()
    {
        EdgeRestrict();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            transform.position    += Vector3.left * _speed * Time.smoothDeltaTime;
            transform.eulerAngles =  new Vector3(0, -_rotation);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position    += Vector3.right * _speed * Time.smoothDeltaTime;
            transform.eulerAngles =  new Vector3(0, _rotation);
        } else {
            transform.eulerAngles = new Vector3(0, 0);
        }

        RestrictPlayer(transform);
    }

    private void RestrictPlayer(Transform transform)
    {
        float newX = Mathf.Clamp(transform.position.x, _xmin, _xmax);
        transform.position = new Vector3(newX, transform.position.y);
    }

    private void EdgeRestrict()
    {
        float   distance  = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost  = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        _xmin = leftMost.x  + EDGE_SPACE;
        _xmax = rightMost.x - EDGE_SPACE;
    }
}
