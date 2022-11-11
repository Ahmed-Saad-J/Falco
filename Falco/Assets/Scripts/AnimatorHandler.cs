using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator animator;
    int vertical;
    int horizontal;
    public bool canRot;
    public void Init()
    {
        animator = GetComponent<Animator>();
        //to compare animation state with integers instead of strings,
        //comparing intigers are faster than strings
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");

    }

    public void UpdateAnimatorValues(float verticalM, float horizantalM)
    {
        #region vertical

        float v = 0;
        if (verticalM > 0 && verticalM < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalM > 0.55f)
        {
            v = 1;
        }
        else if (verticalM < 0 && verticalM > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalM < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0;
        if (horizantalM > 0 && horizantalM < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizantalM > 0.55f)
        {
            h = 1;
        }
        else if (horizantalM < 0 && horizantalM > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizantalM < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion
        //send cordination values to the animator to activate transitions
        animator.SetFloat(vertical, v, 0.2f, Time.deltaTime);
        animator.SetFloat(horizontal, h, 0.2f, Time.deltaTime);

    }
    public void CanRotate()
    {
        canRot = true;
    }
    public void CantRotate()
    {
        canRot = false;
    }


}
