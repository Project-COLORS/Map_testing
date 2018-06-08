using UnityEngine;

public class tile
{
    public int m_elevation=0;
    public basechar m_occupyChar;
    public float[] m_centre;

    public tile()
    {

    }

    public tile(float[] centre)
    {
        m_centre=centre;
    }
}
