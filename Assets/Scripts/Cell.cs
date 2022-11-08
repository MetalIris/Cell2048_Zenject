using System;
using UnityEngine;
using Zenject;

public class Cell : MonoBehaviour
{
    public Cell right;
    public Cell left;
    public Cell up;
    public Cell down;

    public GameController _control;

    [Inject]
    public void Construct(GameController control)
    {
        _control = control;
    }

    public Fill _fill;

    [Inject]
    public void Construct(Fill fill)
    {
        _fill = fill;
    }

    private void OnEnable()
    {
        GameController.slide += OnSlide;
    }
    private void OnDisable()
    {
        GameController.slide -= OnSlide;
    }

    private void OnSlide(string whatWasSent)
    {
        CellCheck();
        Debug.Log(whatWasSent);
        if(whatWasSent == "w")
        {
            if(up != null)
            {
                return;
            }
            Cell currentCell = this;
            SlideUp(currentCell);
        }
        if (whatWasSent == "d")
        {
            if (right != null)
            {
                return;
            }
            Cell currentCell = this;
            SlideRight(currentCell);
        }
        if (whatWasSent == "s")
        {
            if (down != null)
            {
                return;
            }
            Cell currentCell = this;
            SlideDown(currentCell);
        }
        if (whatWasSent == "a")
        {
            if (left != null)
            {
                return;
            }
            Cell currentCell = this;
            SlideLeft(currentCell);
        }

        GameController.timer++;
        if(GameController.timer == 3)
        {
            _control.StartSpawnFill();
        }

    }

    void SlideUp(Cell currentCell)
    {
        if(currentCell.down == null)
        {
            return;
        }

        if(currentCell._fill != null)
        {
            Cell nextCell = currentCell.down;
            while(nextCell.down != null && nextCell._fill == null)
            {
                nextCell = nextCell.down;
            }
            if(nextCell._fill != null)
            {
                if(currentCell._fill.Value == nextCell._fill.Value)
                {
                    nextCell._fill.Double();
                    nextCell._fill.transform.parent = currentCell.transform;
                    currentCell._fill = nextCell._fill;
                    nextCell._fill = null;
                }
                else if (currentCell.down._fill != nextCell._fill)
                {
                    Debug.Log("!doubled");
                    nextCell._fill.transform.parent = currentCell.down.transform;
                    currentCell.down._fill = nextCell._fill;
                    nextCell._fill = null;
                }
            }
        }
        else 
        {
            Cell nextCell = currentCell.down;
            while (nextCell.down != null && nextCell._fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell._fill != null)
            {
                nextCell._fill.transform.parent = currentCell.transform;
                currentCell._fill = nextCell._fill;
                currentCell._fill = nextCell._fill;
                nextCell._fill = null;
                SlideUp(currentCell);
                Debug.Log("slide to emp");
            }
        }

        if(currentCell.down == null)
        {
            return ;
        }

        SlideUp(currentCell.down);
    }

    void SlideRight(Cell currentCell)
    {
        if (currentCell.left == null)
        {
            return;
        }

        if (currentCell._fill != null)
        {
            Cell nextCell = currentCell.left;
            while (nextCell.left != null && nextCell._fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell._fill != null)
            {
                if (currentCell._fill.Value == nextCell._fill.Value)
                {
                    nextCell._fill.Double();
                    nextCell._fill.transform.parent = currentCell.transform;
                    currentCell._fill = nextCell._fill;
                    nextCell._fill = null;
                }
                else if (currentCell.left._fill != nextCell._fill)
                {
                    Debug.Log("!doubled");
                    nextCell._fill.transform.parent = currentCell.left.transform;
                    currentCell.left._fill = nextCell._fill;
                    nextCell._fill = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.left;
            while (nextCell.left != null && nextCell._fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell._fill != null)
            {
                nextCell._fill.transform.parent = currentCell.transform;
                currentCell._fill = nextCell._fill;
                currentCell._fill = nextCell._fill;
                nextCell._fill = null;
                SlideRight(currentCell);
                Debug.Log("slide to emp");
            }
        }

        if (currentCell.left == null)
        {
            return;
        }

        SlideRight(currentCell.left);
    }

    void SlideLeft(Cell currentCell)
    {
        if (currentCell.right == null)
        {
            return;
        }

        if (currentCell._fill != null)
        {
            Cell nextCell = currentCell.right;
            while (nextCell.right != null && nextCell._fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell._fill != null)
            {
                if (currentCell._fill.Value == nextCell._fill.Value)
                {
                    nextCell._fill.Double();
                    nextCell._fill.transform.parent = currentCell.transform;
                    currentCell._fill = nextCell._fill;
                    nextCell._fill = null;
                }
                else if(currentCell.right._fill != nextCell._fill)
                {
                    Debug.Log("!doubled");
                    nextCell._fill.transform.parent = currentCell.right.transform;
                    currentCell.right._fill = nextCell._fill;
                    nextCell._fill = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.right;
            while (nextCell.right != null && nextCell._fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell._fill != null)
            {
                nextCell._fill.transform.parent = currentCell.transform;
                currentCell._fill = nextCell._fill;
                currentCell._fill = nextCell._fill;
                nextCell._fill = null;
                SlideLeft(currentCell);
                Debug.Log("slide to emp");
            }
        }

        if (currentCell.right == null)
        {
            return;
        }

        SlideLeft(currentCell.right);
    }

    void SlideDown(Cell currentCell)
    {
        if (currentCell.up == null)
        {
            return;
        }

        if (currentCell._fill != null)
        {
            Cell nextCell = currentCell.up;
            while (nextCell.up != null && nextCell._fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell._fill != null)
            {
                if (currentCell._fill.Value == nextCell._fill.Value)
                {
                    nextCell._fill.Double();
                    nextCell._fill.transform.parent = currentCell.transform;
                    currentCell._fill = nextCell._fill;
                    nextCell._fill = null;
                }
                else if (currentCell.up._fill != nextCell._fill)
                {
                    Debug.Log("!doubled");
                    nextCell._fill.transform.parent = currentCell.up.transform;
                    currentCell.up._fill = nextCell._fill;
                    nextCell._fill = null;
                }
            }
        }
        else
        {
            Cell nextCell = currentCell.up;
            while (nextCell.up != null && nextCell._fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell._fill != null)
            {
                nextCell._fill.transform.parent = currentCell.transform;
                currentCell._fill = nextCell._fill;
                currentCell._fill = nextCell._fill;
                nextCell._fill = null;
                SlideDown(currentCell);
                Debug.Log("slide to emp");
            }
        }

        if (currentCell.up == null)
        {
            return;
        }

        SlideDown(currentCell.up);
    }

    public void CellCheck()
    {
        if(_fill == null)
        {
            return;
        }
        //up
        if(up != null)
        {
            if(up._fill == null)
            {
                return;
            }
            if(up._fill.Value == _fill.Value)
            {
                return;
            }
        }
        //down
        if (_fill == null)
        {
            return;
        }

        if (down != null)
        {
            if (down._fill == null)
            {
                return;
            }
            if (down._fill.Value == _fill.Value)
            {
                return;
            }
        }
        //right
        if (right != null)
        {
            if (right._fill == null)
            {
                return;
            }
            if (right._fill.Value == _fill.Value)
            {
                return;
            }
        }
        //left
        if (left != null)
        {
            if (left._fill == null)
            {
                return;
            }
            if (left._fill.Value == _fill.Value)
            {
                return;
            }
        }

        _control.GameOverCheck();
    }
}
