using UnityEngine;

public class CheckSwapper : MonoBehaviour
{
    public GameObject TrueState;
    public GameObject FalseState;

    public bool Checked;

    private void Update()
    {
        if (!TrueState) return;
        if (!FalseState) return;

        TrueState.gameObject.SetActive(Checked);
        FalseState.gameObject.SetActive(!Checked);
    }
}