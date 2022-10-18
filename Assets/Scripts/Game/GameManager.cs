using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IManager
{
    string Key { get; }
    Object Type();
}

public enum GameInputType
{
    Player,
    UI,
    Both,
}

public class GameManager
{
    // ƒVƒ“ƒOƒ‹ƒgƒ“
    static GameManager s_instance = new GameManager();
    public static GameManager Instance => s_instance;

    Dictionary<string, IManager> _managerDic = new Dictionary<string, IManager>();

    public Transform GameUser { get; set; }

    public GameInputType InputType { get; set; }

    public void AddManager(IManager manager)
    {
        _managerDic.Add(manager.Key, manager);
    }

    public Manager GetManager<Manager>(string key) where Manager : IManager
    {
        return (Manager)_managerDic.FirstOrDefault(m => m.Key == key).Value;
    }
}
