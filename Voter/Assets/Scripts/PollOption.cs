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

  private int countTotal = 0;

   // Start is called before the first frame update
  void Start(){

    InitOptionsInputfield();
    ResetToggles();
  }

  // Update is called once per frame
  void Update(){

  }

  #region Public Methods

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

  private void InitOptionsInputfield() {

    optionNameInputField.Select();
    optionNameInputField.ActivateInputField();

    EventTrigger eventTrigger = optionNameInputField.GetComponent<EventTrigger>();

    EventTrigger.Entry toggleEntry = new EventTrigger.Entry();
    toggleEntry.eventID = EventTriggerType.Deselect;
    toggleEntry.callback.AddListener((data) => { OnDeselect(data); });

    eventTrigger.triggers.Add(toggleEntry);
  }

  #region Event Triggers

  private void OnToggleSelection(BaseEventData data){

    Toggle selectedToggle = data.selectedObject.GetComponent<Toggle>();

    if (selectedToggle == null) Debug.LogError("Toggle is null!");

    foreach (Toggle t in toggles) t.isOn = false;
    selectedToggle.isOn = true;
  }

  private void OnDeselect(BaseEventData data) {

    if (optionNameInputField.text.Equals("") || optionNameInputField.text.Equals(null)){
      optionNameInputField.Select();
      optionNameInputField.ActivateInputField();
    }
  }

  #endregion

  
  #endregion
}
