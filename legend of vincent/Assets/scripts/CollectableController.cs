using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public Transform player;
    public CollectableAggregateController collectableAggregateController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        collectableAggregateController = GameObject.Find("ObjectiveItems").GetComponent<CollectableAggregateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameObject.transform.position - player.position).magnitude < 1.25f){
            collectableAggregateController.totalCollected += 1;
            collectableAggregateController.startDialogue();
            
            Destroy(gameObject);
        }
    }
}
