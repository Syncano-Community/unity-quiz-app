using UnityEngine;
using System.Collections;

public class FoxPanel : MonoBehaviour
{
    private Animator animator;

	void Awake ()
    {
        animator = GetComponent<Animator>();
	}
	
    public void PlayCorrect()
    {
        animator.SetTrigger("Correct");
    }

    public void PlayIncorrect()
    {
        animator.SetTrigger("Incorrect");
    }
}
