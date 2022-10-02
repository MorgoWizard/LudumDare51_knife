using UnityEngine;

public class EventController : MonoBehaviour
{
    public bool fireSword;

    public void RandomEvent()
    {
        ResetEvents();
        int caseNumber = Random.Range(1, 3);
        Debug.Log(caseNumber);
        switch (caseNumber)
        {
            case(1):
                fireSword = true;
                Debug.Log("Sword is on fire!");
                break;
            case(2):
                fireSword = false;
                Debug.Log("Second Event");
                break;
        }

        Random.Range(1, 2);
    }

    private void ResetEvents()
    {
        fireSword = false;
    }
}
