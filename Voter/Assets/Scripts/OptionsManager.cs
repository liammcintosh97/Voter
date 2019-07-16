using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
  public float optionsPadding;
  public GameObject pollOptionsPrefab;
  public GameObject scrollViewContents;

  [Header("User Inputs")]
  public GameObject addOptionButton;
  public GameObject submitButton;
  public GameObject resultsButton;
  public Toggle editToggle;

  private List<GameObject> pollOptions = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
  }

  #region Button Methods

  public void OnAddOptionClick() {

    //Instatiate the new poll option and place it into the contents and system
    GameObject newPollOption = Instantiate(pollOptionsPrefab, scrollViewContents.transform);

    newPollOption.GetComponent<PollOption>().InitPollOption(addOptionButton.GetComponent<Button>()
      , submitButton.GetComponent<Button>(), resultsButton.GetComponent<Button>(), editToggle);

    RectTransform rect = newPollOption.GetComponent<RectTransform>();

    pollOptions.Add(newPollOption);

    ShiftAddOptionsToBottom();
  }

  public void OnEditTogglePress(bool b) {

    bool edit = editToggle.isOn;

    //Enable editing
    if (edit){
      addOptionButton.SetActive(true);
      foreach (GameObject p in pollOptions) p.GetComponent<PollOption>().SetEditbale(true);
    }
    //Disable Editing
    else {
      addOptionButton.SetActive(false);
      foreach (GameObject p in pollOptions) p.GetComponent<PollOption>().SetEditbale(false);
    }

  }

  #endregion

  #region Private Methods

  private void ShiftAddOptionsToBottom()
  {
    addOptionButton.transform.SetAsLastSibling();
  }

  #endregion
}
