using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    public string nameZone;
    public Image characterImage;
    public GameObject _uiManager;
    public int[]  _choiceidx;
    public Sprite _characterImage;
    private int index;
    private int buttonidx;
    public float typingSpeed;
    [SerializeField] private List<DialogueLine> _dialogueList = new List<DialogueLine>();


    private void Start() 
    {
    }
    void Update()
    {
        
        if (_uiManager.GetComponent<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue || _uiManager.GetComponent<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.GoodEnd ||_uiManager.GetComponent<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.BadEnd )
        {
            _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(true);
        }
        if (_uiManager.GetComponent<UiManager>().dialogueBox.text == _dialogueList[index].text && _dialogueList[index].type == DialogueLine.DialogueType.Choice)
        {
            _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
            for (int i = 0; i <  _uiManager.GetComponent<UiManager>()._choiceButton.Count; i++) 
            {
                _uiManager.GetComponent<UiManager>()._choiceButton[i].gameObject.SetActive(true);
            }
        }
        
        
    }
    //gère le défilement de lettre
        IEnumerator Type(){
        foreach (char letter in _dialogueList[index].text.ToCharArray())
        {
            _uiManager.GetComponent<UiManager>().dialogueBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void ContinueText()
    {
        if (this.gameObject.activeSelf)
        {
            _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
        if (index < _dialogueList.Count -1 && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue || index < _dialogueList.Count -1 && _dialogueList[index].type == DialogueLine.DialogueType.Dialogue )
        {
            index = _dialogueList[index].nextLineIndex;
            _uiManager.GetComponent<UiManager>().dialogueBox.text = "";
            StartCoroutine(Type());
        }
        switch (_dialogueList[index].type)
        {
            case DialogueLine.DialogueType.GoodEnd:

                    _uiManager.GetComponent<UiManager>().dialogueBox.text = "";
                _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                _uiManager.SetActive(false);
                    Debug.Log("Good");
                GameManager.Singleton.SetHelp(true);
                break;
            case DialogueLine.DialogueType.BadEnd:
                _uiManager.GetComponent<UiManager>().dialogueBox.text = "";
                _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                _uiManager.SetActive(false);
                GameManager.Singleton.SetHelp(false);
                break;
            case DialogueLine.DialogueType.Choice:
                for (int i = 0; i < _uiManager.GetComponent<UiManager>()._choiceButton.Count; i++)
                {   
                _choiceidx[i] = _dialogueList[index].ChoiceIdx[i];
                Debug.Log(_choiceidx[i]);
                _uiManager.GetComponent<UiManager>()._choiceButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _dialogueList[_choiceidx[i]].text;
                Debug.Log(_uiManager.GetComponent<UiManager>()._choiceButton[i]);
                }
                break;
            default:
                break;
        }  
        }
        
    }
    public void ChoiceMaker(int ChoiceParameter)
    {
        if (this.gameObject.activeSelf){
            index = _dialogueList[_choiceidx[ChoiceParameter]].nextLineIndex;
            _uiManager.GetComponent<UiManager>().dialogueBox.text = "";
            StartCoroutine(Type());
            for (int i = 0; i < _uiManager.GetComponent<UiManager>()._choiceButton.Count ; i++)
            {
                _uiManager.GetComponent<UiManager>()._choiceButton[i].gameObject.SetActive(false);
            }
            _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
        }
            
    }
    public void OnEnable() {
        if (_dialogueList[index].type == DialogueLine.DialogueType.Choice )
        {
            for (int i = 0; i < _uiManager.GetComponent<UiManager>()._choiceButton.Count; i++)
            {   
            _choiceidx[i] = _dialogueList[index].ChoiceIdx[i];
             _uiManager.GetComponent<UiManager>()._choiceButton[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _dialogueList[_choiceidx[i]].text;
            }
        }
        _uiManager.GetComponent<UiManager>().nameZone.text = nameZone;
        _uiManager.GetComponent<UiManager>().characterImage = characterImage;
        _uiManager.SetActive(true);
        StartCoroutine(Type());
    }
    private void OnDisable() {
        for (int i = 0; i < _uiManager.GetComponent<UiManager>()._choiceButton.Count ; i++)
            {
                _uiManager.GetComponent<UiManager>()._choiceButton[i].gameObject.SetActive(false);
            }    
        _uiManager.GetComponent<UiManager>().dialogueBox.text = "";
        index = 0;
        StopCoroutine(Type());
        _uiManager.GetComponent<UiManager>().nameZone.text = "";
        //characterImage.sprite = null;
        _uiManager.GetComponent<UiManager>().characterImage = null; 
        _uiManager.SetActive(false);
        _uiManager.GetComponent<UiManager>().continueButton.gameObject.SetActive(false);
    }
}
