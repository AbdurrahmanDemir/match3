using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class boards : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject tilePrefabs;

    public gem[] gems;
    public gem[,] allGems;

    public float gemSpeed;

    //public int buildIndex;

    public enum boardPhase { waiting, moving}
    public boardPhase currentPhase = boardPhase.moving;
    public gem bomba;
    public float bombaSans = 2f;

    public matchController matchController;

    private void Awake()
    {
        matchController = Object.FindObjectOfType<matchController>();

        allGems = new gem[width, height];

    }
    void Start()
    {
        //buildIndex = SceneManager.GetActiveScene().buildIndex;
        tileCreate();
    }

    public void tileCreate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);

                GameObject BGtile = Instantiate(tilePrefabs, pos, Quaternion.identity);
                BGtile.transform.parent = this.transform;

                BGtile.name = "bgtile - " + pos.x + ", " + pos.y;

                int randomGems = Random.Range(0, gems.Length);

                int kontrolSayaci = 0;
                while (eslesmeVarMý(new Vector2Int(x, y), gems[randomGems]) && kontrolSayaci < 100)
                {
                    randomGems = Random.Range(0, gems.Length);
                    kontrolSayaci++;
                }
                gemCreate(new Vector2Int(x, y), gems[randomGems]);

            }
        }
    }

    //private void Update()
    //{
    //    matchController.matchFind();
    //}

    public void gemCreate(Vector2Int pos, gem creatingGem)
    {


        if (Random.Range(0f,100f)<bombaSans)
        {
            creatingGem = bomba;
        }


        gem gem = Instantiate(creatingGem, new Vector3(pos.x, pos.y+height, 0f), Quaternion.identity);
        gem.transform.parent = this.transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.gemsIndex(pos, this);

    }

    bool eslesmeVarMý(Vector2Int posControl, gem controlGem)
    {
        if (posControl.x > 1)
        {
            if (allGems[posControl.x - 1, posControl.y].unit == controlGem.unit && allGems[posControl.x - 2, posControl.y].unit == controlGem.unit)
            {
                return true;
            }
        }

        if (posControl.y > 1)
        {
            if (allGems[posControl.x, posControl.y - 1].unit == controlGem.unit && allGems[posControl.x, posControl.y - 2].unit == controlGem.unit)
            {
                return true;
            }
        }



        return false;
    }

    void matchDestroy(Vector2Int pos) //eþleþen sadece bir ögeyi yok etme
    {
        if (allGems[pos.x, pos.y] != null)
        {
            if (allGems[pos.x, pos.y].eslestiMi)
            {

                Instantiate(allGems[pos.x, pos.y].gemEffect, new Vector2(pos.x,pos.y), Quaternion.identity);
                Destroy(allGems[pos.x, pos.y].gameObject);
                allGems[pos.x, pos.y] = null;

            }
        }
    }

    public void allMatchDestroy() //eþleþen her þeyi yok etme
    {
        for (int i = 0; i < matchController.BulunanGemListesi.Count; i++)
        {
            if (matchController.BulunanGemListesi[i] != null)
            {
                Saha.instance.ScoreArttr(matchController.BulunanGemListesi[i].scoreDegeri);
                matchDestroy(matchController.BulunanGemListesi[i].posIndex);
            }
        }

        StartCoroutine(altaKaydýr());
    }



    int boslukSayac = 0;
    IEnumerator altaKaydýr()
    {
        yield return new WaitForSeconds(0.2f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x,y]==null )
                {
                    boslukSayac++;
                }
                else if(boslukSayac>0)
                {
                    allGems[x,y].posIndex.y-=boslukSayac;
                    allGems[x,y-boslukSayac]=allGems[x,y];
                    allGems[x, y] = null;
                }
            }

            boslukSayac = 0;
        }

        StartCoroutine(returnGemFill());
    }


    IEnumerator returnGemFill()
    {
        yield return new WaitForSeconds(0.2f);
        bosluklarýDoldur();

        yield return new WaitForSeconds(0.2f);
        matchController.matchFind();

        if (matchController.BulunanGemListesi.Count>0)
        {
            yield return new WaitForSeconds(0.3f);
            allMatchDestroy();
        } else
        {
            yield return new WaitForSeconds(.2f);
            currentPhase = boardPhase.moving;
        }

    }

    void bosluklarýDoldur()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x,y]==null)
                {
                    int randomGems = Random.Range(0, gems.Length);
                    gemCreate(new Vector2Int(x, y), gems[randomGems]);
                }
            }


        }

        failFill();
    }

    public void failFill()
    {
        List<gem> bulunanGemList = new List<gem>();

        bulunanGemList.AddRange(FindObjectsOfType<gem>());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (bulunanGemList.Contains(allGems[x,y]))
                {
                    bulunanGemList.Remove(allGems[x, y]);
                }
            }
        }

        foreach (gem gems in bulunanGemList)
        {
            Destroy(gems.gameObject);
        }
    }

    public void boardMix()
    {
        if (currentPhase != boardPhase.waiting)
        {
            currentPhase = boardPhase.waiting;

            List<gem> boardsGemList = new List<gem>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    boardsGemList.Add(allGems[x, y]);
                    allGems[x, y] = null;
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int kullanilacakGem= Random.Range(0,boardsGemList.Count);

                    int kontrolsayac = 0;

                    while (eslesmeVarMý(new Vector2Int(x, y), boardsGemList[kullanilacakGem]) && kontrolsayac < 100 && boardsGemList.Count > 1)
                    {
                        kullanilacakGem = Random.Range(0, boardsGemList.Count);
                        kontrolsayac++;
                    }


                    //boardsGemList[kullanilacakGem].gemMovement(new Vector2Int(x, y), this);
                    allGems[x,y]= boardsGemList[kullanilacakGem];
                    boardsGemList.RemoveAt(kullanilacakGem);
                }
            }

            StartCoroutine(altaKaydýr());
        }
    }
}
