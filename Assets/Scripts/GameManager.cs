using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

/// <summary>
/// STARTER — Score and win condition for the "Orb Collector" lab goal.
/// Create an empty GameObject named "GameManager" and attach this script.
///
/// ALREADY WORKS: a public Collect() method the Player calls when it grabs
/// an orb, plus an OnGUI() label showing the live score in the top-left corner.
///
/// YOUR LAB TASKS:
///   TODO 6: in Awake(), find all existing orbs with GameObject.FindGameObjectsWithTag
///           and set totalOrbs from the result.
///   TODO 7: in OnGUI(), when collected == totalOrbs show a big centered
///           "YOU WIN" message instead of the score line.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("State")]



    int totalOrbs;

    private int collected;

    void Awake()
    {
        // Simple singleton so the player can call GameManager.Instance.Collect().
        Instance = this;
        totalOrbs = GameObject.FindGameObjectsWithTag("Orb").Length;
        // TODO 6: replace the serialized totalOrbs with a live count:
        // GameObject[] orbs = GameObject.FindGameObjectsWithTag("Orb");
        // totalOrbs = orbs.Length;
    }

    public void Collect()
    {
        collected++;
        Debug.Log($"[GameManager] Collected {collected}/{totalOrbs}");
    }

    void OnGUI()
    {
        // Immediate-mode UI: runs several times per frame, so keep it cheap.
        GUI.Label(new Rect(12, 10, 400, 30), $"Orbs collected: {collected} / {totalOrbs}", new GUIStyle(GUI.skin.label) { fontSize = 20, normal ={textColor = Color.white} });

        // TODO 7: when collected >= totalOrbs, draw a large centered "YOU WIN!" label.
        if (collected >= totalOrbs)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "You Win!", new GUIStyle(GUI.skin.label) { fontSize = 48, alignment = TextAnchor.MiddleCenter });
        }

    }
}
