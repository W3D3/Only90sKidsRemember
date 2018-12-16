using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraFollow : MonoBehaviour
{
    public Vector2 focusAreaSize;
    public Vector2 CameraOffset;

    public GameManagerScript gameManager;

    private Vector2 cameraCenter;
    private Vector2 cameraDelta;
    private FocusArea focusArea;

    void Start()
    {
    }

    /// <summary>
    /// The center of all Players, saved in fields.
    /// </summary>
    /// <returns></returns>
    private void CalculateCameraCenter()
    {
        float maxX = float.MinValue, minX = float.MaxValue;
        float maxY = float.MinValue, minY = float.MaxValue;

        var positions = gameManager.AllPlayers.Select(p => p.gameObject.transform.position);
        foreach (Vector3 position in positions)
        {
            if (maxX < position.x)
                maxX = position.x;
            if (minX > position.x)
                minX = position.x;
            if (maxY < position.y)
                maxY = position.y;
            if (minY > position.y)
                minY = position.y;
        }

        var deltaX = (maxX - minX);
        var deltaY = (maxY - minY);
        cameraDelta = new Vector2(deltaX, deltaY);
        cameraCenter = new Vector2(minX + deltaX / 2 + CameraOffset.x, minY + deltaY / 2 + CameraOffset.y);
    }

    private Bounds CameraCenterBounds
    {
        get { return new Bounds(cameraCenter, Vector3.one); }
    }

    private void CalculateCameraSize()
    {
        // width / height
        var cam = this.GetComponent<Camera>();
        float height = (float) (Math.Pow(cam.aspect, -1) * cameraDelta.x);
        float width = cam.aspect * cameraDelta.y;
        var normalY = cameraCenter.y - cameraDelta.y / 2;

        cam.orthographicSize = Math.Max(cameraDelta.y/2, normalY);
    }

    void LateUpdate()
    {
        CalculateCameraCenter();
        focusArea = new FocusArea(CameraCenterBounds, focusAreaSize);
        focusArea.Update(CameraCenterBounds);
        CalculateCameraSize();

        GetComponent<Transform>().position = new Vector3(cameraCenter.x, cameraCenter.y+2, -10);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }

            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}