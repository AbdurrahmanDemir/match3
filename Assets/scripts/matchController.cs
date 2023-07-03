using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class matchController : MonoBehaviour
{
    public boards boards;

    public List<gem> BulunanGemListesi=new List<gem>();

    private void Awake()
    {
        boards = Object.FindObjectOfType<boards>(); 
    }

    public void matchFind()
    {

        BulunanGemListesi.Clear();
        for (int x = 0; x < boards.width; x++)
        {
            for (int y = 0; y < boards.height; y++)
            {
                gem gecerliGem = boards.allGems[x, y];
                if (gecerliGem!=null)
                {
                    if (x>0 && x<boards.width-1)
                    {
                        gem leftGem = boards.allGems[x - 1, y];
                        gem rightGem = boards.allGems[x+1, y];

                        if (leftGem!=null && rightGem!=null)
                        {
                            if (leftGem.unit==gecerliGem.unit && rightGem.unit==gecerliGem.unit)
                            {
                                gecerliGem.eslestiMi = true;
                                rightGem.eslestiMi = true;
                                leftGem.eslestiMi = true;
                                BulunanGemListesi.Add(leftGem);
                                BulunanGemListesi.Add(rightGem);
                                BulunanGemListesi.Add(gecerliGem);


                            }
                        }
                    }

                    if (y > 0 && y < boards.height - 1)
                    {
                        gem bottomGem = boards.allGems[x, y-1];
                        gem topGem = boards.allGems[x, y+1];

                        if (bottomGem != null && topGem != null)
                        {
                            if (bottomGem.unit == gecerliGem.unit && topGem.unit == gecerliGem.unit)
                            {
                                gecerliGem.eslestiMi = true;
                                topGem.eslestiMi = true;
                                bottomGem.eslestiMi = true;

                                BulunanGemListesi.Add(gecerliGem);
                                BulunanGemListesi.Add(topGem);
                                BulunanGemListesi.Add(bottomGem);



                            }
                        }
                    }
                }

            }


        }

        if (BulunanGemListesi.Count>0)
        {
            BulunanGemListesi = BulunanGemListesi.Distinct().ToList();
        }

        BombaFind();
    }


    public void BombaFind()
    {
        for (int i = 0; i < BulunanGemListesi.Count; i++)
        {
            gem gem = BulunanGemListesi[i];

            int x = gem.posIndex.x;
            int y = gem.posIndex.y;

            if (gem.posIndex.x>0)
            {
                if (boards.allGems[x-1,y]!=null)
                {
                    if (boards.allGems[x-1,y].unit==gem.gemUnit.bomba)
                    {
                        bombaZone(new Vector2Int(x - 1, y), boards.allGems[x-1,y]);
                    }
                }
            }

            if (gem.posIndex.x <boards.width-1)
            {
                if (boards.allGems[x + 1, y] != null)
                {
                    if (boards.allGems[x + 1, y].unit == gem.gemUnit.bomba)
                    {
                        bombaZone(new Vector2Int(x + 1, y), boards.allGems[x + 1, y]);
                    }
                }
            }

            if (gem.posIndex.y > 0)
            {
                if (boards.allGems[x , y-1] != null)
                {
                    if (boards.allGems[x, y - 1].unit == gem.gemUnit.bomba)
                    {
                        bombaZone(new Vector2Int(x, y - 1), boards.allGems[x, y - 1]);
                    }
                }
            }
            if (gem.posIndex.y <boards.height-1)
            {
                if (boards.allGems[x, y + 1] != null)
                {
                    if (boards.allGems[x, y + 1].unit == gem.gemUnit.bomba)
                    {
                        bombaZone(new Vector2Int(x, y + 1), boards.allGems[x, y + 1]);
                    }
                }
            }
        }
    }


    public void bombaZone(Vector2Int bombaPos, gem bomba)
    {
        for (int x = bombaPos.x-bomba.bombaVolue; x <= bombaPos.x + bomba.bombaVolue; x++)
        {
            for (int y = bombaPos.y - bomba.bombaVolue; y <= bombaPos.y + bomba.bombaVolue; y++)
            {
                if (x>=0 && x<boards.width-1 && y>=0 && y<boards.height-1)
                {
                    if (boards.allGems[x,y]!=null)
                    {
                        boards.allGems[x, y].eslestiMi = true;
                        BulunanGemListesi.Add(boards.allGems[x,y]);
                    }
                }
            }
        }

        if (BulunanGemListesi.Count > 0)
        {
            BulunanGemListesi = BulunanGemListesi.Distinct().ToList();
        }

    }
}