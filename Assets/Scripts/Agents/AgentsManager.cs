using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsManager : MonoBehaviour
{
    /** Prefab used to instanciate sim agents */
    public GameObject agentPrefab;
    public Dictionary<string, GameObject> characters;

    public bool InitAgents(List<CharacterAgentData> pagentsData)
    {
        characters = new Dictionary<string, GameObject>();

        foreach(CharacterAgentData agent in pagentsData)
        {
            GameObject ag = Instantiate(agentPrefab, transform);
            ag.transform.position = new UnityEngine.Vector3(agent.Position.X, agent.Position.Y, agent.Position.Z);
            ag.name = agent.Name;

            characters.Add(ag.name, ag);
        }

        return true;
    }

    public void ClearCharacterAgents()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
