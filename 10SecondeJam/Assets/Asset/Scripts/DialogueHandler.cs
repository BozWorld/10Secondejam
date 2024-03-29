﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public string nameZone;
    public int CharOrder;
    public int[]  _choiceidx;
    public Sprite[] _characterSprite;
    private int index;
    private int buttonidx;
    public float typingSpeed;
    [SerializeField] private List<DialogueLine> _dialogueList = new List<DialogueLine>();
    private bool GoodEnding;


    private void Start() 
    {
    }
    void Update()
    {
        
        if (FindObjectOfType<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue || FindObjectOfType<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.GoodEnd ||FindObjectOfType<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.BadEnd )
        {
            FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(true);
        }
        if (FindObjectOfType<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.Choice)
        {
            FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
            for (int i = 0; i <  FindObjectOfType<UiManager>()._choiceButton.Count; i++) 
            {
                FindObjectOfType<UiManager>()._choiceButton[i].gameObject.SetActive(true);
            }
        }
        
        
    }
    //gère le défilement de lettre
        IEnumerator Type(){
        foreach (char letter in _dialogueList[index].text.ToCharArray())
        {
            FindObjectOfType<UiManager>().dialogueBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void ContinueText()
    {

        if (_dialogueList[index].activeEffect)
        {
            FindObjectOfType<WordWobble>().enabled = true;
        }
        else
        {
            FindObjectOfType<WordWobble>().enabled = false;
        }
        if (_dialogueList[index].isCharacterTalking)
        {
            FindObjectOfType<UiManager>().dialogueBox.color = FindObjectOfType<UiManager>().characterTalkColor;
            FindObjectOfType<UiManager>().nameZone.text = "";
        }
        else if (isActiveAndEnabled)
        {
            FindObjectOfType<UiManager>().dialogueBox.color = Color.black;
            FindObjectOfType<UiManager>().nameZone.text = nameZone;
        }
        if (isActiveAndEnabled)
        {
            FindObjectOfType<UiManager>().characterImage.sprite = _characterSprite[_dialogueList[index].characterSpriteIdx];
        }
        if (this.gameObject.activeSelf)
        {
            FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
        if (index < _dialogueList.Count -1 && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue || index < _dialogueList.Count -1 && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue )
        {
            index = _dialogueList[index].nextLineIndex;
            FindObjectOfType<UiManager>().dialogueBox.text = "";
            StartCoroutine(Type());
        }
        switch (_dialogueList[index].type)
        {
            case DialogueLine.DialogueType.GoodEnd:

                    FindObjectOfType<UiManager>().dialogueBox.text = "";
                FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
                FindObjectOfType<UiManager>().gameObject.SetActive(false);
                    GoodEnding = true;
                    this.gameObject.SetActive(false);
                    break;
            case DialogueLine.DialogueType.BadEnd:
                FindObjectOfType<UiManager>().dialogueBox.text = "";
                FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
                FindObjectOfType<UiManager>().gameObject.SetActive(false);
                    GoodEnding = false;
                    this.gameObject.SetActive(false);
                    break;
            case DialogueLine.DialogueType.Choice:
                for (int i = 0; i < FindObjectOfType<UiManager>()._choiceButton.Count; i++)
                {   
                _choiceidx[i] = _dialogueList[index].ChoiceIdx[i];
                FindObjectOfType<UiManager>()._choiceButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _dialogueList[_choiceidx[i]].text;
                }
                break;
            default:
                break;
        }  
        }
        
    }
    public void ChoiceMaker(int ChoiceParameter)
    {
        if (_dialogueList[index].isCharacterTalking && isActiveAndEnabled)
        {
            FindObjectOfType<UiManager>().dialogueBox.color = FindObjectOfType<UiManager>().characterTalkColor;
            FindObjectOfType<UiManager>().nameZone.text = "";
        }
        /*else
        {
            FindObjectOfType<UiManager>().dialogueBox.color = Color.black;
        }*/
        if (isActiveAndEnabled)
        {
            FindObjectOfType<UiManager>().characterImage.sprite = _characterSprite[_dialogueList[index].characterSpriteIdx];
        }
        FindObjectOfType<AudioManager>().Play("clickSound");
        if (this.gameObject.activeSelf){
            index = _dialogueList[_choiceidx[ChoiceParameter]].nextLineIndex;
            FindObjectOfType<UiManager>().dialogueBox.text = "";
            StartCoroutine(Type());
            for (int i = 0; i < FindObjectOfType<UiManager>()._choiceButton.Count ; i++)
            {
                FindObjectOfType<UiManager>()._choiceButton[i].gameObject.SetActive(false);
            }
            FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
        }
            
    }
    public void OnEnable() {
        if (_dialogueList[index].type == DialogueLine.DialogueType.Choice )
        {
            for (int i = 0; i < FindObjectOfType<UiManager>()._choiceButton.Count; i++)
            {   
            _choiceidx[i] = _dialogueList[index].ChoiceIdx[i];
             FindObjectOfType<UiManager>()._choiceButton[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _dialogueList[_choiceidx[i]].text;
            }
        }
        if (isActiveAndEnabled)
        {
            FindObjectOfType<UiManager>().nameZone.text = nameZone;
            FindObjectOfType<UiManager>().characterImage.sprite = _characterSprite[0];
            FindObjectOfType<UiManager>().gameObject.SetActive(true);
            StartCoroutine(Type());
        }
    }
    private void OnDisable() {

        if (GoodEnding)
        {
            GameManager.Singleton.SetHelp(true, CharOrder);
        }
        else
        {
            GameManager.Singleton.SetHelp(false, CharOrder);
        }

        for (int i = 0; i < FindObjectOfType<UiManager>()._choiceButton.Count ; i++)
            {
                FindObjectOfType<UiManager>()._choiceButton[i].gameObject.SetActive(false);
            }    
        FindObjectOfType<UiManager>().dialogueBox.text = "";
        index = 0;
        StopCoroutine(Type());
        FindObjectOfType<UiManager>().nameZone.text = "";
        //characterImage.sprite = null;
        FindObjectOfType<UiManager>().characterImage = null; 
        FindObjectOfType<UiManager>().gameObject.SetActive(false);
        FindObjectOfType<UiManager>().continueButton.gameObject.SetActive(false);
    }
}
