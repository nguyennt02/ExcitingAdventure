using UnityEngine;

public class CreateChain : MonoBehaviour
{
    [SerializeField] private GameObject OBJ_Prefabs;
    [SerializeField] private Transform[] arr_Point; 
    [SerializeField] private float _scare; 

    private void Start()
    {
        for (int i = 0; i < arr_Point.Length - 1; i++){
            Create(i);
        }
    }
    
    private void Create(int index)
    {
        var chain = arr_Point[index+1].position - arr_Point[index].position;
        var direction = chain.normalized;
        float distance = 100;
        var target = arr_Point[index].position;
        while (distance > _scare)
        {
            target += direction * _scare;
            GameObject tf_chain = Instantiate(OBJ_Prefabs, target, Quaternion.identity);
            tf_chain.transform.SetParent(transform);
            distance = (arr_Point[index + 1].position - target).magnitude;
        }
    }
}
