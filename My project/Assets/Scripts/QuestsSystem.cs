using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsSystem : MonoBehaviour
{
    public List<Quest> activeQuests;
    public List<Quest> doneQuests;

    public static QuestsSystem Instance;

    private void Awake()
    {
        Instance = this;
    }
}