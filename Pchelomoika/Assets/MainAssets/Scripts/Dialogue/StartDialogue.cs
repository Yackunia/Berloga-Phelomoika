using UnityEngine;
using DialogueEditor;
using System;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private bool isPlayer;

    public NPCConversation conversation;

    private PlayerMovement move;
    private PlayerHealth health;
    private PlayerAttackSistem attackSistem;
    private GameObject canv;

    private void Awake()
    {
        if (isPlayer)
        {
            SetParameters();

            if (isStart) StartNpcDialogue();
        }
        else if (isStart)
        {
            ConversationManager.Instance.StartConversation(conversation);
        }

        
    }

    private void SetParameters()
    {
        move = GameObject.FindObjectOfType<PlayerMovement>();
        health = GameObject.FindObjectOfType<PlayerHealth>();
        attackSistem = GameObject.FindObjectOfType<PlayerAttackSistem>();

        canv = GameObject.Find("HUD");
    }

    public void StartNpcDialogue()
    {
        move.StopPlayer();
        attackSistem.DisableCombat();
        canv.SetActive(false);

        Time.timeScale = 0;

        ConversationManager.Instance.StartConversation(conversation);
    }

    public void EndNpcDialogue()
    {
        move.UnFreezePlayer();
        attackSistem.EnableCombat();
        canv.SetActive(true);

        Time.timeScale = 1f;

        ConversationManager.Instance.EndConversation();
    }
}
