using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject barrier;  // Rào cản để tắt

    public void DisableBarrier()
    {
        // Tắt rào cản
        barrier.SetActive(false);
    }
}
