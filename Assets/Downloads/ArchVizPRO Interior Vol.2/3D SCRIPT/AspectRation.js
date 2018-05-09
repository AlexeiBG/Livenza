#pragma strict

var newAspectRatio = 16.0 / 8.0;
 
function Awake()
{
    var variance = newAspectRatio / Camera.main.aspect;
    if (variance < 1.0)
        Camera.main.rect = Rect ((1.0 - variance) / 2.0, 0 , variance, 1.0);
    else
    {
        variance = 1.0 / variance;
        Camera.main.rect = Rect (0, (1.0 - variance) / 2.0 , 1.0, variance);
    }
}
 