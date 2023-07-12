using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_4_5_MyScript : MonoBehaviour
{
    public bool PauseMovement = true;

    public GameObject TravelingBall = null;
    public GameObject RedTarget = null;

    public float BallSpeed = 0.01f;    // units per second
    public bool DrawVelocity = false;
    private float VelocityDrawFactor = 20f;        // So that we can see the vector drawn

    public Vector3 WindDirection = Vector3.zero;
    public float WindSpeed = 0.01f;
    public bool ApplyWind = false;
    public bool DrawWind = false;
    public bool CompensateWind = false;
    public bool RandomWind = false;

    private MyVector ShowVelocity = null;
    private MyVector ShowWindVector = null;
    private MyVector ShowActualVelocity = null;
    private MyVector ShowWindCompensationVector = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TravelingBall != null);
        Debug.Assert(RedTarget != null);

        ShowVelocity = new MyVector()
        {
            VectorColor = Color.green,
            DrawVectorComponents = false
        };

        ShowWindVector = new MyVector() {
            VectorColor = new Color(0.8f, 0.3f, 0.3f, 1.0f),
            DrawVectorComponents = false
        };

        ShowActualVelocity = new MyVector() {
            VectorColor = new Color(0.3f, 0.3f, 0.8f, 1.0f),
            DrawVectorComponents = false
        };

        ShowWindCompensationVector = new MyVector() {
            VectorColor = Color.magenta,
            DrawVectorComponents = false
        };

    }

    // Update is called once per frame
    void Update()
    {
        //10% Chance of wind
        if (RandomWind)
        {
            if (Random.Range(1, 100) < 11)
            {
                WindSpeed = ((float)Random.Range(1, 100)) / 100;
            }
            else
            {
                WindSpeed = 0.0f;
            }
        }
        else
        {
            WindSpeed = 0.0f;
        }
        
        
        
        Vector3 vDir = RedTarget.transform.localPosition - TravelingBall.transform.localPosition;
        float distance = vDir.magnitude;

        if (distance > float.Epsilon)  // if not already at the target
        {
            vDir.Normalize();
            WindDirection.Normalize();

            Vector3 vT = BallSpeed * vDir;
            Vector3 vWind = WindSpeed * WindDirection;
            Vector3 vA = vT + vWind;
            Vector3 vC = vA - vWind;

            #region Display the vectors
            ShowVelocity.VectorAt = TravelingBall.transform.localPosition;
            ShowVelocity.Magnitude = BallSpeed * VelocityDrawFactor;
            ShowVelocity.Direction = vDir;
            ShowVelocity.DrawVector = DrawVelocity;
                
            ShowWindVector.VectorAt = TravelingBall.transform.localPosition + (ShowVelocity.Magnitude * ShowVelocity.Direction);
            ShowWindVector.Direction = WindDirection;
            ShowWindVector.Magnitude = WindSpeed * VelocityDrawFactor;
            ShowWindVector.DrawVector = DrawWind;

            ShowActualVelocity.VectorAt = TravelingBall.transform.localPosition;
            ShowActualVelocity.Direction = vA;
            ShowActualVelocity.Magnitude = vA.magnitude * VelocityDrawFactor;
            ShowActualVelocity.DrawVector = DrawWind;

            ShowWindCompensationVector.VectorAt = TravelingBall.transform.localPosition + (ShowVelocity.Magnitude * ShowVelocity.Direction);
            ShowWindCompensationVector.Direction = WindDirection;
            ShowWindCompensationVector.Magnitude = -WindSpeed * VelocityDrawFactor;
            ShowWindCompensationVector.DrawVector = CompensateWind;
            #endregion 

            if (PauseMovement)
                return;

            if (ApplyWind)
            {
                if (CompensateWind)
                {
                    TravelingBall.transform.localPosition += vC * Time.deltaTime;
                }
                else
                {
                    TravelingBall.transform.localPosition += vA * Time.deltaTime;
                }
            }
            else
            {
                TravelingBall.transform.localPosition += vT * Time.deltaTime;
            }
        }
    }
}
