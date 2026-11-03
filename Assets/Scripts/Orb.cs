using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityPipeline.System.Reflection.Metadata.Ecma335;

/// <summary>
/// STARTER — A collectable orb for the "Orb Collector" lab goal.
/// Attach to a small sphere (scale 0.6), give it a Collider with
/// "Is Trigger" checked, and tag the object "Orb".
///
/// ALREADY WORKS: click-to-recolor via OnMouseDown (GetMouseButtonDown
/// fallback included), a gentle bob + spin every frame, and a
/// gameObject.SetActive(false) hide on trigger entry is handled by the
/// player (see PlayerController TODO 2).
/// </summary>
public class Orb : MonoBehaviour

{

    [Header("Click palette")]
    [SerializeField] private Color[] clickColors = { Color.cyan, Color.magenta, Color.yellow };

    [Header("Bob & spin")]
    [SerializeField] private float bobHeight = 0.15f;
    [SerializeField] private float spinSpeed = 90f;

    private Vector3 basePosition;
    private Vector3 baseScale;
    private int clickIndex;
    private float hideTime = -1f;



    void Start()
    {

    }
    void Awake()
    {
        basePosition = transform.position;
        baseScale = transform.localScale;
    }

    void Update()
    {

        // Bob up and down using Time.time (seconds since Play started).
        float bob = Mathf.Sin(Time.time * 3f) * bobHeight;
        transform.position = basePosition + Vector3.up * bob;
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }

    private void OnMouseDown()
    {

        // Click the orb's collider → cycle its color. Uses GetComponent<T>().
        clickIndex = (clickIndex + 1) % clickColors.Length;
        GetComponent<Renderer>().material.color = clickColors[clickIndex];
        Debug.Log($"[Orb] Clicked → {clickColors[clickIndex]}");
    }

    void OnMouseDrag()
    {
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, basePosition.y, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 newPosition = ray.GetPoint(distance);


            // Keep within the arena borders (between -14 and 14)
            float clampedX = Mathf.Clamp(newPosition.x, -14f, 14f);
            float clampedZ = Mathf.Clamp(newPosition.z, -14f, 14f);

            basePosition = new Vector3(clampedX, basePosition.y, clampedZ);

            transform.position = basePosition + Vector3.up * (Mathf.Sin(Time.time * 3f) * bobHeight);
        }
    }

    // NOTE: The player script collects orbs in its OnTriggerEnter. This script
    // only handles looking pretty, being clicked, and bobbing.
}
