using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // シングルトン
    static GameManager s_instance = new GameManager();
    public static GameManager Instance => s_instance;

    public Transform GameUser { get; set; }
}
