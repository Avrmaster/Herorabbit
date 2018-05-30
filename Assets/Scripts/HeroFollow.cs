using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    public HeroRabbit Rabbit;
    public float GlueFactor = 4;

    private float Lerp(float cur, float goal, float percentage)
    {
        if (percentage > 1)
            percentage = 1;
        if (percentage < 0)
            percentage = 0;
        return cur * (1 - percentage) + goal * percentage;
    }

    void Update()
    {
        var rabitTransform = Rabbit.transform;
        var cameraTransform = this.transform;

        var rabitPosition = rabitTransform.position;
        var cameraPosition = cameraTransform.position;

        cameraPosition.x = Lerp(cameraPosition.x, rabitPosition.x, GlueFactor*Time.deltaTime);
        cameraPosition.y = Lerp(cameraPosition.y, rabitPosition.y, GlueFactor*Time.deltaTime);

        cameraTransform.position = cameraPosition;
    }
}