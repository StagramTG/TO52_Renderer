using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsManager : MonoBehaviour
{
    /** Prefab used to instanciate sim agents */
    public GameObject agentPrefab;

    public bool InitAgents(List<CharacterAgentData> pagentsData)
    {
        foreach(CharacterAgentData agent in pagentsData)
        {
            GameObject ag = Instantiate(agentPrefab, transform);
            ag.name = agent.Name;
        }

        return true;
    }
}
