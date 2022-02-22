
using UnityEngine;

[ExecuteAlways]
public class Test : MonoBehaviour
{
    public virtual void TestVirtual()
    {

    }
    private float vector2;
    // public abstract void TestAbstact();

    private void Update()
    {
        Debug.Log(vector2 = new Vector2(4, 3).magnitude);
    }


}

