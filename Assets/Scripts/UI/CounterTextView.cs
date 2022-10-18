using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CounterTextView : MonoBehaviour
{
    [SerializeField] List<ViewData> _viewDataLsit;

    [System.Serializable]
    class ViewData
    {
        [SerializeField] string _path;
        [SerializeField] Image _source;

        public string Path => _path;
        public Image Source => _source;
        public int ID { get; set; }
    }

    public void Setup()
    {
        for (int index = 0; index < _viewDataLsit.Count; index++)
        {
            _viewDataLsit[index].ID = index;
        }
    }

    public void ViewUpdate(int id)
    {
        foreach (ViewData data in _viewDataLsit)
        {
            Image source = data.Source;
            
            if (data.ID == id)
            {
                source.color = new Color(source.color.r, source.color.g, source.color.b, 1);
                source.transform.SetAsLastSibling();
            }
            else
            {
                source.color = new Color(source.color.r, source.color.g, source.color.b, 0.5f);
            }
        }
    }
}
