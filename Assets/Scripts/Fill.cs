using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Fill : MonoBehaviour
{
    Image myImage;

    public int Value;

    [SerializeField] TextMeshProUGUI valueDisplay;
    [SerializeField] float speed;

    bool hasCombined;

    public GameController _control;

    [Inject]
    public void Construct(GameController control)
    {
        _control = control;
    }

    public void FillValueUpdate(int valueIn)
    {
        Value = valueIn;
        valueDisplay.text = valueIn.ToString();

        int colorIndex = GetColorIndex(Value);
        myImage = GetComponent<Image>();
        Debug.Log(colorIndex);
        myImage.color = _control.fillColors[colorIndex];
    }

    int GetColorIndex(int valueIn)
    {
        int index = 0;

        while(valueIn != 1)
        {
            index++;
            valueIn /=2;
        }

        index--;
        return index;
    }

    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            hasCombined = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if (hasCombined == false)
        {
            if (transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombined = true;
        }
    }

    public void Double()
    {
        Value *= 2;
        _control.ScoreUpdate(Value);
        valueDisplay.text = Value.ToString();

        int colorIndex = GetColorIndex(Value);
        myImage.color = _control.fillColors[colorIndex];
    }

    public class Factory : PlaceholderFactory<Fill>
    {
    }
}
