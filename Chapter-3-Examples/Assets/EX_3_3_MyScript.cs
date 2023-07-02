using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_3_3_MyScript : MonoBehaviour
{
    private MySphereBound TaxiBound = null;
    private MySphereBound CarBound = null;

    private MySphereBound TaxiFrontTireBound = null;
    private MySphereBound TaxiRearTireBound = null;

    private MySphereBound CarFrontTireBound = null;
    private MySphereBound CarRearTireBound = null;

    public GameObject TheTaxi = null;
    public float TaxiBoundRadius = 2.0f;
    public bool DrawTaxiBound = true;

    public GameObject TaxiFrontTireLeft = null;
    public GameObject TaxiFrontTireRight = null;
    public GameObject TaxiRearTireLeft = null;
    public GameObject TaxiRearTireRight = null;
    public float TaxiTireBoundRadii = 1.45f;
    public bool DrawTaxiTireBound = true;

    public GameObject TheCar = null;
    public float CarBoundRadius = 2.0f;
    public bool DrawCarBound = true;

    public GameObject CarFrontTireLeft = null;
    public GameObject CarFrontTireRight = null;
    public GameObject CarRearTireLeft = null;
    public GameObject CarRearTireRight = null;
    public float CarTireBoundRadii = 1.45f;
    public bool DrawCarTireBound = true;

    public float DistanceBetween = 0.0f;
    public float DistBtwnTaxiFrontCarFront = 0.0f;
    public float DistBtwnTaxiRearCarRear = 0.0f;
    public float DistBtwnTaxiFrontCarRear = 0.0f;
    public float DistBtwnTaxiRearCarFront = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TheTaxi != null);	    // Make sure proper editor setup
        Debug.Assert(TheCar != null);

        TaxiBound = new MySphereBound();
        CarBound = new MySphereBound();

        TaxiFrontTireBound = new MySphereBound();
        TaxiRearTireBound = new MySphereBound();

        CarFrontTireBound = new MySphereBound();
        CarRearTireBound= new MySphereBound();
    }

    // Update is called once per frame
    void Update()
    {
        // Step 1: Assume no intersection
        TaxiBound.BoundColor = MySphereBound.NoCollisionColor;
        CarBound.BoundColor = MySphereBound.NoCollisionColor;

        TaxiFrontTireBound.BoundColor = MySphereBound.NoCollisionColor;
        TaxiRearTireBound.BoundColor = MySphereBound.NoCollisionColor;

        CarFrontTireBound.BoundColor = MySphereBound.NoCollisionColor;
        CarRearTireBound.BoundColor = MySphereBound.NoCollisionColor;

        // Step 2: Update the Taxi sphere bound
        TaxiBound.Center = TheTaxi.transform.localPosition;
        TaxiBound.Radius = TaxiBoundRadius;
        TaxiBound.DrawBound = DrawTaxiBound;

        Vector3 TaxiFrontTireBoundCenter = new Vector3
            (TheTaxi.transform.position.x,
            TaxiFrontTireLeft.transform.position.y,
            TaxiFrontTireLeft.transform.position.z);
        TaxiFrontTireBound.Center = TaxiFrontTireBoundCenter;
        Vector3 TaxiRearTireBoundCenter = new Vector3
            (TheTaxi.transform.position.x,
            TaxiRearTireLeft.transform.position.y,
            TaxiRearTireLeft.transform.position.z);
        TaxiRearTireBound.Center = TaxiRearTireBoundCenter;

        TaxiFrontTireBound.Radius = TaxiTireBoundRadii;
        TaxiRearTireBound.Radius = TaxiTireBoundRadii;
        TaxiFrontTireBound.DrawBound = DrawTaxiTireBound;
        TaxiRearTireBound.DrawBound = DrawTaxiTireBound;

        // Step 3: Update the Car sphere bound
        CarBound.Center = TheCar.transform.localPosition;
        CarBound.Radius = CarBoundRadius;
        CarBound.DrawBound = DrawCarBound;

        Vector3 CarFrontTireBoundCenter = new Vector3
            (TheCar.transform.position.x,
            CarFrontTireLeft.transform.position.y,
            CarFrontTireLeft.transform.position.z);
        CarFrontTireBound.Center = CarFrontTireBoundCenter;
        Vector3 CarRearTireBoundCenter = new Vector3
            (TheCar.transform.position.x,
            CarRearTireLeft.transform.position.y,
            CarRearTireLeft.transform.position.z);
        CarRearTireBound.Center = CarRearTireBoundCenter;

        CarFrontTireBound.Radius = CarTireBoundRadii;
        CarRearTireBound.Radius = CarTireBoundRadii;
        CarFrontTireBound.DrawBound = DrawCarTireBound;
        CarRearTireBound.DrawBound = DrawCarTireBound;

        // Step 4: Compute the distance between the sphere bounds as magnitude of a Vector3 
        Vector3 diff = TaxiBound.Center - CarBound.Center;
        DistanceBetween = diff.magnitude;

        Vector3 diffTaxiFrontCarFront = TaxiFrontTireBound.Center - CarFrontTireBound.Center;
        DistBtwnTaxiFrontCarFront = diffTaxiFrontCarFront.magnitude;

        Vector3 diffTaxiRearCarRear = TaxiRearTireBound.Center - CarRearTireBound.Center;
        DistBtwnTaxiRearCarRear = diffTaxiRearCarRear.magnitude;

        Vector3 diffTaxiFrontCarRear = TaxiFrontTireBound.Center - CarRearTireBound.Center;
        DistBtwnTaxiFrontCarRear = diffTaxiFrontCarRear.magnitude;

        Vector3 diffTaxiRearCarFront = TaxiRearTireBound.Center - CarFrontTireBound.Center;
        DistBtwnTaxiRearCarFront = diffTaxiRearCarFront.magnitude;

        // Step 5: Testing and showing intersection status
        bool hasIntersection = DistanceBetween <= (TaxiBound.Radius + CarBound.Radius);
        bool hasIntersectTaxiFrontCarFront = DistBtwnTaxiFrontCarFront <= (TaxiFrontTireBound.Radius + CarFrontTireBound.Radius);
        bool hasIntersectTaxiRearCarRear = DistBtwnTaxiRearCarRear <= (TaxiRearTireBound.Radius + CarRearTireBound.Radius);
        bool hasIntersectTaxiFrontCarRear = DistBtwnTaxiFrontCarRear <= (TaxiFrontTireBound.Radius + CarRearTireBound.Radius);
        bool hasIntersectTaxiRearCarFront = DistBtwnTaxiRearCarFront <= (TaxiRearTireBound.Radius + CarFrontTireBound.Radius);

        if (!hasIntersection) { return; }
        
        Debug.Log("Intersect!! Distance:" + DistanceBetween);
        TaxiBound.BoundColor = MySphereBound.CollisionColor;
        CarBound.BoundColor = MySphereBound.CollisionColor;

        // The collision functionality is supported by the MySphereBound class as well
        Debug.Assert(TaxiBound.SpheresIntersects(CarBound)); 

        if (hasIntersectTaxiFrontCarFront)
        {
            Debug.Log("Intersect!! Distance:" + DistBtwnTaxiFrontCarFront);
            TaxiFrontTireBound.BoundColor = MySphereBound.CollisionColor;
            CarFrontTireBound.BoundColor = MySphereBound.CollisionColor;

            Debug.Assert(TaxiFrontTireBound.SpheresIntersects(CarFrontTireBound));
        }

        if (hasIntersectTaxiRearCarRear)
        {
            Debug.Log("Intersect!! Distance:" + DistBtwnTaxiRearCarRear);
            TaxiRearTireBound.BoundColor = MySphereBound.CollisionColor;
            CarRearTireBound.BoundColor = MySphereBound.CollisionColor;

            Debug.Assert(TaxiRearTireBound.SpheresIntersects(CarRearTireBound));
        }

        if (hasIntersectTaxiFrontCarRear)
        {
            Debug.Log("Intersect!! Distance:" + DistBtwnTaxiFrontCarRear);
            TaxiFrontTireBound.BoundColor = MySphereBound.CollisionColor;
            CarRearTireBound.BoundColor = MySphereBound.CollisionColor;

            Debug.Assert(TaxiFrontTireBound.SpheresIntersects(CarRearTireBound));
        }

        if (hasIntersectTaxiRearCarFront)
        {
            Debug.Log("Intersect!! Distance:" + DistBtwnTaxiRearCarFront);
            TaxiRearTireBound.BoundColor = MySphereBound.CollisionColor;
            CarFrontTireBound.BoundColor = MySphereBound.CollisionColor;

            Debug.Assert(TaxiRearTireBound.SpheresIntersects(CarFrontTireBound));
        }
    }
}