using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float _radius = 0.5f;
    [SerializeField] Vector3 _offset;
    [SerializeField] GameObject[] _enemyList;

    void Start()
    {
        Set();
    }

    void Set()
    {
        for (int index = 0; index < _enemyList.Length; index++)
        {
            GameObject obj = Instantiate(_enemyList[index]);
            obj.transform.SetParent(transform);

            // ランダムポジションの生成
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            float radius = Random.Range(1, _radius);

            Vector3 setPos = new Vector3(x, 0, z).normalized * radius;

            obj.transform.position = transform.position + setPos + _offset;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
