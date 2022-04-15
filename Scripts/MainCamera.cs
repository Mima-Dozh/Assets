using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float minZoom, maxZoom;
    [SerializeField] private float mapWidth, mapHeight;
    // Start is called before the first frame update
    internal static float leftX, rightX, leftY, rightY;
    void Start()
    {
        var tilemap = GameObject.Find("Tilemap");
        leftX = -mapWidth/2;
        leftY = -mapHeight/2;
        rightX = mapWidth/2;
        rightY = mapHeight/2;
    }

    private Vector3 GetNewCameraPosition(Vector3 direction)
    {
        var leftDownAngle = transform.position - Camera.main.ScreenToWorldPoint(Vector2.zero);
        var rightUpAngle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - transform.position;
        var k = transform.position + direction / 25;
        return new Vector3
        (
            Mathf.Clamp(k.x, leftX + leftDownAngle.x, rightX - rightUpAngle.x),
            Mathf.Clamp(k.y, leftY + leftDownAngle.y, rightY - rightUpAngle.y),
            k.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPause)
            return;
        
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var leftDownAngle = Camera.main.ScreenToWorldPoint(Vector2.zero);
        var rightUpAngle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        if(leftX - leftDownAngle.x > 0.5
        && rightX - rightUpAngle.x < 0.5
        || leftY - leftDownAngle.y > 0.5
        && rightY - rightUpAngle.y < 0.5)
            maxZoom = GetComponent<Camera>().orthographicSize - (float)0.5;

        if(scroll != 0)
            GetComponent<Camera>().orthographicSize 
            = Mathf.Clamp(GetComponent<Camera>().orthographicSize - scroll * 5, minZoom, maxZoom);

        var mousePosition = Input.mousePosition;
        var direction = Vector3.zero;
        if(mousePosition.x > Screen.width - 1)
            direction += Vector3.right;
        if(mousePosition.y > Screen.height - 1)
            direction += Vector3.up;
        if(mousePosition.x < 1)
            direction += Vector3.left;
        if(mousePosition.y < 1)
            direction += Vector3.down;
        transform.position = GetNewCameraPosition(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(-mapWidth/2, mapHeight/2), new Vector2(mapWidth/2, mapHeight/2));
        Gizmos.DrawLine(new Vector2(-mapWidth/2, -mapHeight/2), new Vector2(mapWidth/2, -mapHeight/2));
        Gizmos.DrawLine(new Vector2(-mapWidth/2, -mapHeight/2), new Vector2(-mapWidth/2, mapHeight/2));
        Gizmos.DrawLine(new Vector2(mapWidth/2, -mapHeight/2), new Vector2(mapWidth/2, mapHeight/2));
    }
}
