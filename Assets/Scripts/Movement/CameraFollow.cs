using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraFollow : MonoBehaviour
{
    public Vector2 FocusAreaSize;
    private FocusArea focusArea;

    /// <summary>
    /// Additional camera padding around the outer players.
    /// </summary>
    public Vector2 CameraPadding;

    /// <summary>
    /// The desired minimal size for the camera.
    /// </summary>
    public float MinimalCameraSize;

    /// <summary>
    /// The offset for the camera center.
    /// </summary>
    public Vector2 CameraOffset;

    public GameManagerScript GameManager;

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
    
    /// <summary>
    /// The center and diagonal of all Players, saved in fields for performance.
    /// </summary>
    /// <returns></returns>
    private void InitCameraPosition()
    {
        float maxX = float.MinValue, minX = float.MaxValue;
        float maxY = float.MinValue, minY = float.MaxValue;

        var positions = GameManager.AllPlayers.Select(p => p.gameObject.transform.position);
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

        var deltaX = maxX - minX + CameraPadding.x * 2;
        var deltaY = maxY - minY + CameraPadding.y * 2;

        cameraDelta = new Vector2(deltaX, deltaY);
        cameraCenter = new Vector2(
            minX + deltaX / 2 - CameraPadding.x + CameraOffset.x,
            minY + deltaY / 2 - CameraPadding.y + CameraOffset.y);
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

        var neededMinY = Math.Max(neededY, neededYFromX);
        minimalCameraSize = Math.Max(MinimalCameraSize, neededMinY);
    }

    void Start()
    {
        FocusAreaSize = new Vector2(6, 6);
        focusArea = new FocusArea(CameraCenterBounds, FocusAreaSize);
    }

    void LateUpdate()
    {
        InitCameraPosition();
        InitCameraSize();

//        LateUpdateOld();
//        return;

        focusArea = new FocusArea(CameraCenterBounds, FocusAreaSize);
        focusArea.Update(CameraCenterBounds);

        GetComponent<Camera>().orthographicSize = minimalCameraSize;
        GetComponent<Transform>().position = new Vector3(cameraCenter.x, cameraCenter.y, -10);
    }

    Vector2 verticalOffset = new Vector2(0, 0);
    float lookAheadDirX = 0;
    private float currentLookAheadX = 0;
    private float targetLookAheadX = 0;
    private float smoothLookVelocityX = 1;
    private float smoothVelocityY = 1;
    private float lookSmoothTimeX = 1;
    private float verticalSmoothTime = 1;

    void LateUpdateOld()
    {
        focusArea.Update(CameraCenterBounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;
        if (focusArea.velocity.x != 0)
        {
            //lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            //if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
            //{
            //    lookAheadStopped = false;
            //    targetLookAheadX = lookAheadDirX * lookAheadDstX;
            //}
            //else
            //{
            //    if (!lookAheadStopped)
            //    {
            //        lookAheadStopped = true;
            //        targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
            //    }
            //}
        }

        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, FocusAreaSize);
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