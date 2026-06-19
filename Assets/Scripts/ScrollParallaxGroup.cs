using UnityEngine;

public class ScrollParallaxGroup : MonoBehaviour
{
	[SerializeField] public ScrollParallax[] scrollParallaxes;
	
  public void EnableScrolling()
  {
		foreach (var parallax in scrollParallaxes) {
			parallax.scrolling = true;
		}
  }

  public void DisableScrolling()
  {
		foreach (var parallax in scrollParallaxes) {
			parallax.scrolling = false;
		}
  }

  public float groupSpeed = 1f;
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