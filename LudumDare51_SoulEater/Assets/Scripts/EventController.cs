using TMPro;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public bool fireSword;

    [SerializeField] private TextMeshProUGUI eventMesseege;

    //public void RandomEvent()
    //{
    //    ResetEvents();
    //    int caseNumber = Random.Range(1, 3);
    //    switch (caseNumber)
    //    {
    //        case(1):
    //            fireSword = true;
    //            eventMesseege.text = "FIRE SWORD!";
    //            break;
    //        case(2):
    //            fireSword = false;
    //            eventMesseege.text = "not FIRE SWORD!";
    //            break;
    //    }

    //    Random.Range(1, 2);
    //}

    private void ResetEvents()
    {
        fireSword = false;
    }
}
