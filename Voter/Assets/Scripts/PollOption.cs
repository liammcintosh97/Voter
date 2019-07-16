using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PollOption : MonoBehaviour{

  [SerializeField]
  public List<Toggle> toggles = new List<Toggle>();
  public InputField optionNameInputField;

  //UI references
  private Button addOptionsButton;
  private Button submitButton;
  private Button resultsButton;
  private Toggle editToggle;

  private int countTotal = 0;

   // Start is called before the first frame update
  void Start(){

    if (addOptionsButton.IsActive()) addOptionsButton.interactable = false;
    InitOptionsInputfield();
    ResetToggles();
  }

  // Update is called once per frame
  void Update(){
 
  }

  #region Init Methods

  private void InitOptionsInputfield()
  {

    optionNameInputField.Select();
    optionNameInputField.ActivateInputField();

    EventTrigger eventTrigger = optionNameInputField.GetComponent<EventTrigger>();

    EventTrigger.Entry deselectEntry = new EventTrigger.Entry();
    deselectEntry.eventID = EventTriggerType.Deselect;
    deselectEntry.callback.AddListener((data) => { OnDeselect(data); });

    optionNameInputField.onEndEdit.AddListener(delegate { OnEndEdit(optionNameInputField); });

    EventTrigger.Entry selectEntry = new EventTrigger.Entry();
    selectEntry.eventID = EventTriggerType.Select;
    selectEntry.callback.AddListener((data) => { OnSelect(data); });

    eventTrigger.triggers.Add(deselectEntry);
    eventTrigger.triggers.Add(selectEntry);
  }

  public void InitPollOption(Button addB, Button sB, Button rB, Toggle eT) {

    addOptionsButton = addB;
    submitButton = sB;
    resultsButton = rB;
    editToggle = eT;

    Focus(true);
}

  #endregion

  #region Public Methods

  public void Focus(bool focus) {

    if (addOptionsButton.IsActive()) addOptionsButton.interactable = !focus;
    submitButton.interactable = !focus;
    resultsButton.interactable = !focus;
    editToggle.interactable = !focus;

    if (focus)
    {
      optionNameInputField.Select();
      optionNameInputField.ActivateInputField();
    }
  }

  public void SetEditbale(bool editable) {

    if (editable)
    {
      optionNameInputField.interactable = false;
    }
    else {
      optionNameInputField.interactable = false;
    }
  }

  public int  ReturnSelection() {

    foreach (Toggle t in toggles){
      if (t.isOn) {
        Text toggleText = t.gameObject.GetComponentInChildren<Text>();
        return int.Parse(toggleText.text);
      }
    }

    Debug.LogError("Unbale to return selection from toggles so returning 1");
    return -1;
  }

  public void SubmitSelection() {

    countTotal += ReturnSelection();
  }

  #region Getters and Setters

  public int GetCountTotal() { return countTotal; }

  #endregion
  
  #endregion

  #region Private Methods

  private void ResetToggles() {

    foreach (Toggle t in toggles){
      t.isOn = false;

      EventTrigger eventTrigger = t.GetComponent<EventTrigger>();

      EventTrigger.Entry toggleEntry = new EventTrigger.Entry();
      toggleEntry.eventID = EventTriggerType.PointerClick;
      toggleEntry.callback.AddListener((data) => { OnToggleSelection(data); });

      eventTrigger.triggers.Add(toggleEntry);
    }
  }

  #region Delegates

  private void OnEndEdit(InputField input) {
    OnDeselect(null);
  }

  #endregion

  #region Event Triggers

  private void OnToggleSelection(BaseEventData data){

    Toggle selectedToggle = data.selectedObject.GetComponent<Toggle>();

    if (selectedToggle == null) Debug.LogError("Toggle is null!");

    foreach (Toggle t in toggles) t.isOn = false;
    selectedToggle.isOn = true;
  }

  private void OnDeselect(BaseEventData data) {

    //The poll options is not complete
    if (optionNameInputField.text.Equals("") || optionNameInputField.text.Equals(null)) Focus(true);
    //The poll options is complete
    else Focus(false);
 
  }

  private void OnSelect(BaseEventData data){
    Focus(true);
  }

  #endregion

  #endregion
}
