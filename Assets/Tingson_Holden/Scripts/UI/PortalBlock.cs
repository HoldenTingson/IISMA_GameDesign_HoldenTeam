using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBlock : Singleton<PortalBlock>
{
    public void OpenPortal()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
    }
}
