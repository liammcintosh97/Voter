using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{

  public GameObject pollOptionsPrefab;
  public GameObject scrollViewContents;
  public GameObject addOptionButton;

  private List<GameObject> pollOptions = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    pollOptions.Add(addOptionButton);
  }

  // Update is called once per frame
  void Update()
  {

  }

  #region Button Methods

  public void OnAddOptionClick() {

    GameObject newPollOption = Instantiate(pollOptionsPrefab, scrollViewContents.transform);
    RectTransform rect = newPollOption.GetComponent<RectTransform>();

    rect.anchoredPosition3D = new Vector3(10, -10, 0);

    pollOptions.Add(newPollOption);

    foreach (GameObject po in pollOptions)
    {
      RectTransform r = po.GetComponent<RectTransform>();
      r.anchoredPosition3D = new Vector3(10, r.anchoredPosition3D.y - 40, 0);
    }
  }

  #endregion
}
