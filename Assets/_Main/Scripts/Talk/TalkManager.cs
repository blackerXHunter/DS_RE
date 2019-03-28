using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    public Fungus.Flowchart chat;
    // Start is called before the first frame update
    void Start()
    {
        chat = GetComponent<Fungus.Flowchart>();
        chat.SendFungusMessage("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
