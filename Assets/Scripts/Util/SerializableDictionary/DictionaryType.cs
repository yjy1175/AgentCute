using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PBL
{
    [System.Serializable]
    public class IntGameObject : SerializableDictionary<int, GameObject> { }
    [System.Serializable]
    public class StringGameObject : SerializableDictionary<string, GameObject> { }
    [System.Serializable]
    public class IntStringGameObject : SerializableDictionary<int, StringGameObject> { }
    [System.Serializable]
    public class StringIntGameObject : SerializableDictionary<string, IntGameObject> { }
}
