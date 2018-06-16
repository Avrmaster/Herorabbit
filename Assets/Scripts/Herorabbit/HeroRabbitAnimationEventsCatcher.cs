using UnityEngine;

namespace Herorabbit
{
    public class HeroRabbitAnimationEventsCatcher : MonoBehaviour
    {
        private HeroRabbit Parent()
        {
            return transform.parent.GetComponent<HeroRabbit>();
        }
        
        public void OnDied()
        {
            Parent().OnDied();
        }
    }
}