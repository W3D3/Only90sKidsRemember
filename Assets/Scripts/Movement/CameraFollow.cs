using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraFollow : MonoBehaviour
{
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
        float neededYFromX = ((float) Math.Pow(camera.aspect, -1)) * neededX; // height / width * width
        float neededY = cameraDelta.y / 2;

        var neededMinY = Math.Max(neededY, neededYFromX);
        minimalCameraSize = Math.Max(MinimalCameraSize, neededMinY);
    }

    void LateUpdate()
    {
        InitCameraPosition();
        InitCameraSize();

        GetComponent<Camera>().orthographicSize = minimalCameraSize;
        GetComponent<Transform>().position = new Vector3(cameraCenter.x, cameraCenter.y, -10);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(cameraCenter, new Vector2(1, 1));
    }
}