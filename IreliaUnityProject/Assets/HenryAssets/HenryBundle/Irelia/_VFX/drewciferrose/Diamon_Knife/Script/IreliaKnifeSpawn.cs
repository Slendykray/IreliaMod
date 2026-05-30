using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class IreliaKnifeSpawn : MonoBehaviour
{
    [SerializeField] GameObject knifeObj;
    [SerializeField] Transform transformParent;
    [SerializeField] Transform[] startPoint;
    [SerializeField] Transform[] endPoint;
    [SerializeField] float duration = 1f;
    [SerializeField] Vector2 durationOffset;

    int spawnCount;
    List<GameObject> knifeList = new List<GameObject>();

    void OnEnable()
    {
        DestroyAllKnife();
        SpawnKnife();
    }

    void SpawnKnife()
    {
        spawnCount = startPoint.Length;
        for(int i = 0; i< spawnCount; i++)
        {
            GameObject knife = Instantiate(knifeObj);
            knife.transform.SetParent(transformParent);
            knifeList.Add(knife);

            Transform start = startPoint[i];    
            Transform end = endPoint[i]; 

            StartCoroutine(SetPosKnife(knife.transform, start, end)) ;
        }
    }

    IEnumerator SetPosKnife(Transform knifeT, Transform start, Transform end)
    {
        float timer = 0;
        float t;
        float newDuration = duration;
        newDuration += Random.Range(durationOffset.x, durationOffset.y);

        while(timer < newDuration)
        {
            timer += Time.deltaTime;
            t = timer/newDuration;

            knifeT.transform.position = Vector3.Lerp(start.position,end.position,t );
            knifeT.transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, t);
            knifeT.transform.localScale = Vector3.Lerp(start.localScale, end.localScale, t);

            yield return null;
        }
    }

    void DestroyAllKnife()
    {
        for(int i = 0; i< knifeList.Count; i++)
        {
            Destroy(knifeList[i]);
        }
        knifeList.Clear();
    }
}
