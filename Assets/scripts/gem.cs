using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class gem : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]
    public boards boards;

    Vector2 firstTouch;
    Vector2 endTouch;
    Vector2Int firstPos;

    bool dragStarted;
    float movedAngle;
    public int scoreDegeri;

    public int bombaVolue;

    gem otherGem;

    public bool eslestiMi;

    public GameObject gemEffect;

    public enum gemUnit {mavi,pembe,sarý,ayesil,kyesil,bomba}
    public gemUnit unit;



    private void Update()
    {
        if (Vector2.Distance(transform.position,posIndex)>.01f)
        {
            transform.position = Vector2.Lerp(transform.position, posIndex, boards.gemSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
        }

        if (dragStarted && Input.GetMouseButtonUp(0))
        {   
            dragStarted = false;

            if (boards.currentPhase == boards.boardPhase.moving)
            {
                endTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                AngleHesapla();
            }
            
        }

        
    }
    public void gemsIndex(Vector2Int pos, boards theboars)
    {
        posIndex = pos;
        boards = theboars;
    }
    private void OnMouseDown()
    {
        if (boards.currentPhase==boards.boardPhase.moving)
        {
            firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStarted = true;
        }
        
    }

    public void AngleHesapla()
    {
        float dx= endTouch.x - firstTouch.x;
        float dy= endTouch.y - firstTouch.y;
        movedAngle = Mathf.Atan2(dy,dx);
        movedAngle= movedAngle*180/ Mathf.PI;

        if (Vector3.Distance(firstTouch,endTouch) > 0.4f) //
        {
            gemMovement();
        }
    }

    public void gemMovement()
    {
        firstPos=posIndex;

        if (movedAngle<45 && movedAngle>-45&& posIndex.x< boards.width-1)
        {
            otherGem = boards.allGems[posIndex.x + 1, posIndex.y];
            otherGem.posIndex.x--;
            posIndex.x++;
        }
        else if (movedAngle > 45 && movedAngle <= 135 && posIndex.y < boards.height - 1)
        {
            otherGem = boards.allGems[posIndex.x, posIndex.y+1];
            otherGem.posIndex.y--;
            posIndex.y++;
        }
        else if (movedAngle < -45 && movedAngle >= -135 && posIndex.y > 0)
        {
            otherGem = boards.allGems[posIndex.x, posIndex.y - 1];
            otherGem.posIndex.y++;
            posIndex.y--;
        }
        else if (movedAngle > 135 || movedAngle >= -135 && posIndex.x>0)
        {
            otherGem = boards.allGems[posIndex.x-1, posIndex.y];
            otherGem.posIndex.x++;
            posIndex.x--;
        }

        boards.allGems[posIndex.x, posIndex.y] = this;
        boards.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;

        StartCoroutine(movedController());
    }

    public IEnumerator movedController()
    {
        boards.currentPhase = boards.boardPhase.waiting;

        yield return new WaitForSeconds(.2f);

        boards.matchController.matchFind();

        if (otherGem!=null)
        {
            if (!eslestiMi&&!otherGem.eslestiMi)
            {
                otherGem.posIndex = posIndex;
                posIndex = firstPos;

                boards.allGems[posIndex.x, posIndex.y] = this;
                boards.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;

                yield return new WaitForSeconds(.2f);
                    
                boards.currentPhase = boards.boardPhase.moving;

            }
            else //yok etme
            {
                boards.allMatchDestroy();
            }
        }

    }
}
