using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class VirtualAttackBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool push_chk = false;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("AttackChk");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        push_chk = false;
    }

    private IEnumerator AttackChk()
    {

        push_chk = true;
        yield return new WaitForSeconds(.1f);
        push_chk = false;
        yield return null;
        

    }

}
