using UnityEngine;

public class GameTile : MonoBehaviour
{
    public int Row { get; private set; }
    public int Col { get; private set; }
    public TileData Properties;

    private Material _material;

    private void Start()
    {
        Row = (int) (Mathf.Round(transform.position.x/0.996f));
        Col = (int) (Mathf.Round(transform.position.z/0.996f));

        // print(string.Format("{0},{1}",Mathf.Round(transform.position.x/0.996f),Mathf.Round(transform.position.z/0.996f)));

        Properties.OnCurrentColorChange += onTileColorChange;
    }

    private void onTileColorChange(object source, TileData.ColorChangeEventArgs changeData)
    {
        GetComponent<Renderer>().material = MaterialCache.getTileMaterial(changeData.NewColor);
    }
}
