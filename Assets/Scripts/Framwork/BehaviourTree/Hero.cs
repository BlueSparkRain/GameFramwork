using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    BehaviourTree tree;
    [SerializeField] NavMeshAgent agent;
    public GameObject Treasure;
    public GameObject Treasure2;
    private void Awake()
    {
        tree = new BehaviourTree("Hero");

        Sequence gOToTreasure = new Sequence("ToTreasure",2);
        gOToTreasure.AddChild(new Leaf("PreasentTreasure", new Condition(() => Treasure.activeSelf)));
        gOToTreasure.AddChild(new Leaf("MoveToTreasure", new ActionStrategy(() => { agent.SetDestination(Treasure.transform.position); Debug.Log("我发现了宝藏1"); })));

        Sequence gOToTreasure2 = new Sequence("ToTreasure2",1);
        gOToTreasure2.AddChild(new Leaf("PreasentTreasure2", new Condition(() => Treasure2.activeSelf)));
        gOToTreasure2.AddChild(new Leaf("MoveToTreasure2", new ActionStrategy(() => { agent.SetDestination(Treasure2.transform.position); Debug.Log("我发现了宝藏2"); })));
        PrioritySelector treasureSelector = new PrioritySelector("treasureSelector");//node的priority越大优先级越高

        treasureSelector.AddChild(gOToTreasure);
        treasureSelector.AddChild(gOToTreasure2);

        tree.AddChild(treasureSelector);
    }
    private void Update()
    {
        tree.Process();
    }

}
