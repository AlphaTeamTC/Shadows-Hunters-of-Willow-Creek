using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace  NPC
{
    public class NPCNavigation : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        private NavMeshAgent NPC;
        private int index = 0; 
        
        // Start is called before the first frame update
        void Start()
        {
            NPC = GetComponent<NavMeshAgent>();
            StartCoroutine(WalkAround());

        }

        private IEnumerator WalkAround()
        {
            while (true)
            {
                NPC.destination = points[index].position;
                yield return new WaitForSeconds(4);
                print($"NPC reached {points[index].name}");
                
                index = (index + 1) % points.Length;
            }
            yield return null; 
        }
    } 
}

