using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelect : MonoBehaviour
{
    [SerializeField] private int maxUnits = 100;
    [SerializeField] private Image mainRect;
    private Rect rect;
    private Vector2 startPosition, endPosition;
    private Color original, clear, curColor;
    private bool canDraw;
	public static UnitComponent units;
	private static List<UnitComponent> unitSelected;
	private static int unitCount = 0;
	// Start is called before the first frame update
	public static void AddUnit(UnitComponent comp)
	{
		units.nextComponent = comp;
		comp.previousComponent = units;
		units = comp;
		unitCount++;
	}
    void Awake()
    {
		unitCount = 0;
		units = new UnitComponent();
		unitSelected = new List<UnitComponent>();
		original = mainRect.color;
		clear = original;
		clear.a = 0;
		curColor = clear;
		mainRect.color = clear;
    }

    void Draw()
	{
		endPosition = Input.mousePosition;
		if(startPosition == endPosition || !canDraw) 
            return;

		curColor = original;

		rect = new Rect(Mathf.Min(endPosition.x, startPosition.x),
			Screen.height - Mathf.Max(endPosition.y, startPosition.y),
			Mathf.Max(endPosition.x, startPosition.x) - Mathf.Min(endPosition.x, startPosition.x),
			Mathf.Max(endPosition.y, startPosition.y) - Mathf.Min(endPosition.y, startPosition.y)
		);

		mainRect.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);

		mainRect.rectTransform.anchoredPosition = new Vector2(rect.x + mainRect.rectTransform.sizeDelta.x/2, 
			Mathf.Max(endPosition.y, startPosition.y) - mainRect.rectTransform.sizeDelta.y/2);
	}

	void SetSelect()
	{
		foreach(var unit in units)
		{
			if(unit != null)
			{
				var position = Camera.main.WorldToScreenPoint(unit.transform.position);
				if(rect.Contains(new Vector2(position.x, Screen.height - position.y)))
				{
					unit.Select();
					unitSelected.Add(unit);
				}
			}
		}
	}

	public static void SetDeselect()
	{
		foreach(var unit in unitSelected)
		{
			unit.Deselect();
		}
		unitSelected = new List<UnitComponent>();
	}

	// Update is called once per frame
	void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rect = new Rect();
            startPosition = Input.mousePosition;
            canDraw = true;
			SetDeselect();
        }

        if (Input.GetMouseButtonUp(0))
        {
            curColor = clear;
            canDraw = false;
			SetSelect();
        }

		if(Input.GetMouseButton(1))
			foreach (var unit in unitSelected)
				unit.finishPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		

        Draw();

        mainRect.color = Color.Lerp(mainRect.color, curColor, 10 * Time.deltaTime);
    }
}
