using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

/// <summary>
/// 双方のフィールドを行き来するためのギミッククラス
/// </summary>

public class FieldConnecter : MonoBehaviour
{
    // ステージ繋ぐ為のデータ
    [System.Serializable]
    class ConnectData
    {
        [SerializeField] GameObject _field;
        [SerializeField] Vector3 _offset;
        [SerializeField] float _scale = 1;

        public Vector3 Offset
        {
            get
            {
                if (_field == null)
                {
                    return Vector3.zero;
                }
                else
                {
                    return _field.transform.position + _offset; 
                }
            }
        }
        public float Scale => _scale;
        public int ID { get; set; }
        public string Name => _field.name;
    }

    [System.Serializable]
    class EventData
    {
        [SerializeField] float _eventSpeed = 1;
        [SerializeField] float _tolerance;
        
        public float EventSpeed => _eventSpeed;
        public float Tolerance => _tolerance;
    }

    [SerializeField] ConnectData _connectData1;
    [SerializeField] ConnectData _connectData2;
    [SerializeField] EventData _eventData;

    float _eventTimer;

    List<ConnectData> _connectDataList;
    List<FieldConnecterEvent> _fieldConnecterEventList;

    void Start()
    {
        _fieldConnecterEventList = new List<FieldConnecterEvent>();
        // データの登録
        _connectDataList = new List<ConnectData>();
        _connectDataList.Add(_connectData1);
        _connectDataList.Add(_connectData2);

        // Event用データの作成
        for (int index = 0; index < 2; index++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);

            ConnectData connectData = _connectDataList[index];
            obj.name = $"Connecter : {connectData.Name}";

            connectData.ID = index;
            CreateConnecter(obj, connectData);
        }
    }

    void CreateConnecter(GameObject obj, ConnectData connectData)
    {
        // Colliderの生成
        BoxCollider collider = obj.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        collider.transform.localPosition = connectData.Offset;
        collider.transform.localScale = Vector3.one * connectData.Scale;

        // Eventの発行
        FieldConnecterEvent field = obj.AddComponent<FieldConnecterEvent>();
        field.SetData(CallBack, connectData.ID);

        _fieldConnecterEventList.Add(field);
    }

    // Eventが購読された際の遷移処理
    void CallBack(int currentID, Transform target, IFieldEventHandler handler)
    {
        ConnectData data = _connectDataList.First(c => c.ID != currentID);
        StartCoroutine(SetTransition(data.Offset, target, handler));
    }

    IEnumerator SetTransition(Vector3 endPos, Transform target, IFieldEventHandler handler)
    {
        _eventTimer = 0;
        handler.FieldEventExecution = true;
        _fieldConnecterEventList.ForEach(f => f.ColliderActive(false));

        yield return new WaitUntil(() => Execute(endPos, target));

        handler.FieldEventExecution = false;

        yield return new WaitForSeconds(2f);

        _fieldConnecterEventList.ForEach(f => f.ColliderActive(true));
    }

    bool Execute(Vector3 endPos, Transform target)
    {
        _eventTimer += Time.deltaTime * _eventData.EventSpeed;

        Vector3 currentPos = Vector3.Slerp(target.position, endPos, _eventTimer);
        target.position = currentPos;

        return _eventData.Tolerance > Vector3.Distance(currentPos, endPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(_connectData1.Offset, Vector3.one);
        Gizmos.DrawWireCube(_connectData2.Offset, Vector3.one);
    }
}
