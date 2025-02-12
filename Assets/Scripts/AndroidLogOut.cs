using UnityEngine;
using UnityEngine.UI;

public class AndroidLogOut : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private void Start()
    {
        button.onClick.AddListener(() => 
        { 
            AndroidSignIn.SignOut();

            CheckClick.IsCan = true;
        });
    }
}