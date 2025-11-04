using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    public Animator anim;
    public string animName = "Star"; 
    public float loopInterval = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= loopInterval)
        {
            anim.Play(animName, -1, 0f); 
            timer = 0f;
        }
    }
}
