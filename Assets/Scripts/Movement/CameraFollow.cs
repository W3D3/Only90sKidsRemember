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

    /// <summary>
    /// The middle between all players.
    /// The center of the desired visible area.
    /// </summary>
    private Vector2 cameraCenter;

    /// <summary>
    /// The diagonal between the desired visible area.
    /// </summary>
    private Vector2 cameraDelta;

    /// <summary>
    /// The minimal needed camera size to catch all players.
    /// </summary>
    private float minimalCameraSize;

    private FocusArea focusArea;

    void Start()
    {
    }

    /// <summary>
    /// The center and diagonal of all Players, saved in fields for performance.
    /// </summary>
    /// <returns></returns>
    private void InitCameraPosition()
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

    /// <summary>
    /// Calculates the minimal needed camera size to show all players.
    /// </summary>
    private void InitCameraSize()                                                                                                                                                                                  
    {
        // aspect ration = width / height
        var camera = this.GetComponent<Camera>();

        float neededX = cameraDelta.x / 2;
        float neededYFromX = ((float)Math.Pow(camera.aspect, -1)) * neededX; // height / width * width
        float neededY = cameraDelta.y / 2;

         minimalCameraSize  = Math.Max(neededY, neededYFromX);
    }

    void LateUpdate()
    {
        InitCameraPosition();
        InitCameraSize();
        focusArea = new FocusArea(CameraCenterBounds, focusAreaSize);
        focusArea.Update(CameraCenterBounds);

        GetComponent<Camera>().orthographicSize = minimalCameraSize+1;
        GetComponent<Transform>().position = new Vector3(cameraCenter.x, cameraCenter.y, -10);
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