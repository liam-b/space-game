using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
  public static ObjectPooler SharedInstance;
  public List<GameObject> pooledObjects = new List<GameObject>();
  public GameObject poolObject;

  void Awake() {
    SharedInstance = this;
  }

  public GameObject DrawObject(Vector2 position, Quaternion rotation, Transform parent) {
    for (int i = 0; i < pooledObjects.Count; i++) {
      if (!pooledObjects[i].activeInHierarchy) {
        pooledObjects[i].SetActive(true);
        pooledObjects[i].transform.position = position;
        pooledObjects[i].transform.rotation = rotation;
        pooledObjects[i].transform.parent = parent;

        return pooledObjects[i];
      }
    }
    
    return newObject(position, rotation, parent);
  }

  public void ReleaseObject(GameObject obj) {
    obj.SetActive(false);
  }

  GameObject newObject(Vector2 position, Quaternion rotation, Transform parent) {
    GameObject obj = (GameObject)Instantiate(poolObject, position, rotation, parent);
    pooledObjects.Add(obj);
    obj.SetActive(true);
    return obj;
  }
}
