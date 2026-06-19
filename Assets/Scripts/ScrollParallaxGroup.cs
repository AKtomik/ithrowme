using UnityEngine;

public class ScrollParallaxGroup : MonoBehaviour
{
	[SerializeField] public ScrollParallax[] scrollParallaxes;
	
  public void EnableGroupScrolling()
  {
		foreach (var parallax in scrollParallaxes) {
			parallax.scrolling = true;
		}
  }

  public float groupSpeed;
  private float cachedSpeed;

	void Update()
	{
		if (cachedSpeed == groupSpeed) return;
		cachedSpeed = groupSpeed;
		foreach (var parallax in scrollParallaxes) {
			parallax.ScrollParentSpeed = groupSpeed;
		}
	}
}