using UnityEngine;
using UnityEngine.Audio;

public class TakableItemHologram : TakableItem
{
    [Header("Item Hologram")]
    [SerializeField] protected GameObject hologramContent;

	public override void Start()
	{
		base.Start();
        hologramContent.SetActive(false);
	}

    override public void Put(Transform point)
    {
        base.Put(point);
        hologramContent.transform.SetPositionAndRotation(point.transform.position, point.transform.rotation);
		hologramContent.SetActive(true);
    }

    override public void Unput(Transform point)
    {
        base.Unput(point);
        hologramContent.SetActive(false);
    }
}
