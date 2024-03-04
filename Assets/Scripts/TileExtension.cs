using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "fantasy rule tile", menuName = "Tiles/MyTile")]
public class MyTileClass : RuleTile  // or TileBase or RuleTile or other
{
    // will be able to plug in value you want in Inspector for asset
    public int exampleIntergerField;
}