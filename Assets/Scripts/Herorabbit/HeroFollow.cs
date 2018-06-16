using UnityEngine;

namespace Herorabbit
{
    public class HeroFollow : MonoBehaviour
    {
        public HeroRabbit Rabbit;
        public float GlueFactor = 4;

        void Update()
        {
            var rabitTransform = Rabbit.transform;
            var cameraTransform = this.transform;

            var rabitPosition = rabitTransform.position;
            var cameraPosition = cameraTransform.position;

            cameraPosition.x = cameraPosition.x.Lerp(rabitPosition.x, GlueFactor * Time.deltaTime);
            cameraPosition.y = cameraPosition.y.Lerp(rabitPosition.y, GlueFactor * Time.deltaTime);

            cameraTransform.position = cameraPosition;
        }
    }
}