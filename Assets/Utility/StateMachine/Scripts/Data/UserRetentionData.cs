using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MonoState.Data
{
    public interface IRetentionData
    {
        string Path { get; }
        Object RetentionData();
    }

    public class UserRetentionData
    {
        public UserRetentionData(GameObject user)
        {
            User = user;
        }

        Dictionary<string, IRetentionData> _retentionDataDic = new Dictionary<string, IRetentionData>();
        
        public GameObject User { get; private set; }

        public void SetRetentionData(IRetentionData data)
        {
            _retentionDataDic.Add(data.Path, data);
        }

        public Data GetRetentionData<Data>(string key) where Data : Object, IRetentionData
        {
            IRetentionData retentionData = _retentionDataDic.First(d => d.Key == key).Value;
            Object data = retentionData.RetentionData();

            return (Data)data;
        }
    }
}