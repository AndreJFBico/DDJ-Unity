using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Vector2z
{


    public static Vector2z zero
    {
        get { return new Vector2z(0, 0); }
    }

    public static Vector2z Lerp(Vector2z a, Vector2z b, float val)
    {
        Vector2z lerped = new Vector2z();
        lerped.vector2 = Vector2.Lerp(a.vector2, b.vector2, val);
        return lerped;
    }



    public float x = 0;
    public float z = 0;

    public Vector2z()
    {
        //nothing
    }

    public Vector2z(float _x, float _z)
    {
        x = _x;
        z = _z;
    }

    public Vector2z(Vector2 v2)
    {
        x = v2.x;
        z = v2.y;
    }

    public Vector2z(Vector3 v3)
    {
        x = v3.x;
        z = v3.z;
    }

    public Vector3 vector3
    {
        get { return new Vector3(x, 0, z); }

        set
        {
            x = value.x;
            z = value.z;
        }
    }

    public Vector2 vector2
    {
        get { return new Vector3(x, z); }

        set
        {
            x = value.x;
            z = value.y;
        }
    }

    public float y
    {
        get { return z; }
        set { z = value; }
    }

    public static Vector2z operator +(Vector2z a, Vector2z b)
    {
        return new Vector2z(a.x + b.x, a.z + b.z);
    }

    public static Vector2z operator -(Vector2z a, Vector2z b)
    {
        return new Vector2z(a.x - b.x, a.z - b.z);
    }

    public static Vector2z operator *(Vector2z a, Vector2z b)
    {
        return new Vector2z(a.x * b.x, a.z * b.z);
    }

    public static Vector2z operator /(Vector2z a, Vector2z b)
    {
        return new Vector2z(a.x / b.x, a.z / b.z);
    }
}

public class EarClipTriangle
{
	public Vector2z a;
	public Vector2z b;
	public Vector2z c;
	public Rect bounds;
	
	public EarClipTriangle(Vector2z a, Vector2z b, Vector2z c)
	{
		bounds = new Rect(a.x,a.z,0,0);
		Vector2z[] points = new Vector2z[]{a,b,c};
		for(int i=1; i<3; i++)
		{
			if(bounds.xMin < points[i].x)
				bounds.xMin = points[i].x;
			if(bounds.xMax < points[i].x)
				bounds.xMax = points[i].x;
			if(bounds.yMin < points[i].z)
				bounds.yMin = points[i].z;
			if(bounds.yMax < points[i].z)
				bounds.yMax = points[i].z;
		}
	}
}

public class EarClipper 
{

	public static int[] Triangulate( Vector2z[] points)
	{
		int numberOfPoints = points.Length;
		List<int> usePoints = new List<int>();
		for(int p=0; p<numberOfPoints; p++)
			usePoints.Add(p);
		int numberOfUsablePoints = usePoints.Count;
		List<int> indices = new List<int>();
		
        if (numberOfPoints < 3)
            return indices.ToArray();
		
		int it = 100;
		while(numberOfUsablePoints > 3)
		{
			for(int i=0; i<numberOfUsablePoints; i++)
			{
				int a,b,c;
				
				a=usePoints[i];
				
				if(i>=numberOfUsablePoints-1)
					b=usePoints[0];
				else
					b=usePoints[i+1];
				
				if(i>=numberOfUsablePoints-2)
					c=usePoints[(i+2)-numberOfUsablePoints];
				else
					c=usePoints[i+2];
				
				Vector2 pA = points[b].vector2;
				Vector2 pB = points[a].vector2;
				Vector2 pC = points[c].vector2;
				
				float dA = Vector2.Distance(pA,pB);
				float dB = Vector2.Distance(pB,pC);
				float dC = Vector2.Distance(pC,pA);
				
				float angle = Mathf.Acos((Mathf.Pow(dB,2)-Mathf.Pow(dA,2)-Mathf.Pow(dC,2))/(2*dA*dC))*Mathf.Rad2Deg * Mathf.Sign(Sign(points[a],points[b],points[c]));
				if(angle < 0)
				{
					continue;//angle is not reflex
				}
				
				bool freeOfIntersections = true;
				for(int p=0; p<numberOfUsablePoints; p++)
				{
					int pu = usePoints[p];
					if(pu==a || pu==b || pu==c)
						continue;
					
					if(IntersectsTriangle2(points[a],points[b],points[c],points[pu]))
					{
						freeOfIntersections=false;
						break;
					}
				}
				
				if(freeOfIntersections)
				{
					indices.Add(a);
					indices.Add(b);
					indices.Add(c);
					usePoints.Remove(b);
					it=100;
					numberOfUsablePoints = usePoints.Count;
					i--;
					break;
				}
			}
			it--;
			if(it<0)
				break;
		}
		
		indices.Add(usePoints[0]);
		indices.Add(usePoints[1]);
		indices.Add(usePoints[2]);
		indices.Reverse();
		
		return indices.ToArray();
	}
	
	private static bool IntersectsTriangle(Vector2z A, Vector2z B, Vector2z C, Vector2z P)
	{
		bool b1, b2, b3;

		b1 = Sign(P, A, B) < 0.0f;
		b2 = Sign(P, B, C) < 0.0f;
		b3 = Sign(P, C, A) < 0.0f;
		
		return ((b1 == b2) && (b2 == b3));
	}
	
	private static float Sign(Vector2z p1, Vector2z p2, Vector2z p3)
	{
		return (p1.x - p3.x) * (p2.z - p3.z) - (p2.x - p3.x) * (p1.z - p3.z);
	}
					
	private static bool IntersectsTriangle2(Vector2z A, Vector2z B, Vector2z C, Vector2z P)
	{
			float planeAB = (A.x-P.x)*(B.y-P.y)-(B.x-P.x)*(A.y-P.y);
			float planeBC = (B.x-P.x)*(C.y-P.y)-(C.x - P.x)*(B.y-P.y);
			float planeCA = (C.x-P.x)*(A.y-P.y)-(A.x - P.x)*(C.y-P.y);
			return Sign(planeAB)==Sign(planeBC) && Sign(planeBC)==Sign(planeCA);
	}
	
	private static int Sign(float n) 
	{
		return (int)(Mathf.Abs(n)/n);
	}
}