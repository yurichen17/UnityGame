using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //properties
    public enum TileKind
    {
        One,
        Three,
        Two,
        Clue,
        Mine
        
    }
    public enum TileState
    {
        Normal,
        Flagged
    }

    public bool isCovered = true;
    
    public Sprite coveredSprite;
    public Sprite flagSprite;
    public Sprite mineClicked;
    public Sprite flagNotAMineSprite;

    public TileKind tileKind = TileKind.One;
    public TileState tileState = TileState.Normal;
    private Sprite defaultSprite;

    private void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = coveredSprite;
    }
    public void SetIsCovered(bool covered)
    {
        isCovered = false;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
    public void SetClickedMine()
    {
        GetComponent<SpriteRenderer>().sprite = mineClicked;
    }
    public void SetNotAMineFlagged()
    {
        GetComponent<SpriteRenderer>().sprite = flagNotAMineSprite;
    }
    //mouse over only works on 2d colliders
    private void OnMouseOver()
    {
        if (isCovered)
        {
            //Debug.Log("Mouse Over" + this.name);
            if (Input.GetMouseButtonUp(1))
            {
                if (tileState == TileState.Normal)
                {
                    tileState = TileState.Flagged;
                    GetComponent<SpriteRenderer>().sprite = flagSprite;
                }
                else
                {
                    tileState = TileState.Normal;
                    GetComponent<SpriteRenderer>().sprite = coveredSprite;
                }
            }
        }
    }
}
