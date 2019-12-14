using UnityEngine;
using System.Collections;

public class SimpleWater : MonoBehaviour {

    public delegate void ItweenComplete();
    public ItweenComplete WaterComplete;

	public float width = 100;

	public float height = 100;
	
	public int waterSubdivisions = 1;
	
    private Mesh proceduralMesh;

    public Color deepColor = new Color(0.2f,0.2f,0.2f,1);

    public Color surfaceColor = new Color(1f,1f,1f,1);

    public Material waterMaterial;

	private Material myMaterial;

	void Awake()
	{
		if (GetComponent<MeshFilter>() == null)
			CreateWaterPlane();
        myMaterial = GetComponent<Renderer>().material;
        myMaterial.SetFloat("_WaterHeight",0f);
	}

    float currentHeight = 0f;
    Color currentColor;
    bool tweenAlpha = false;
    public void SetHeight(float waterHeight,bool isRightNow = false,bool tweenAlpha = false)
    {
        currentColor = myMaterial.GetColor("_Color");
        this.tweenAlpha = tweenAlpha;
        if(waterHeight>0 && waterHeight<0.1f){
            waterHeight = 0.1f;
        }
        if(isRightNow){
            currentHeight = waterHeight;
            myMaterial.SetFloat("_WaterHeight",currentHeight);
        }else{
            TweenWater(currentHeight,waterHeight);
            currentHeight = waterHeight;
        }
    }

    public void SetTexture(Texture tex){
        myMaterial.SetTexture("_MainTex",tex);
    }

    private void TweenWater(float from,float to){
        Hashtable args = new Hashtable();
        args.Add("from",from);
        args.Add("to",to);
        args.Add("time",1);
        args.Add("easetype",iTween.EaseType.easeInSine);
        args.Add("onstart","onstart");
        args.Add("onstarttarget",this.gameObject);
        args.Add("onupdate","onupdate");
        args.Add("onupdatetarget",this.gameObject);
        args.Add("oncomplete","oncomplete");
        args.Add("oncompletetarget",this.gameObject);
        iTween.ValueTo(this.gameObject,args);
    } 
    
    void onstart(){
        //Debug.Log("onstart");
    }
    // int split = 1;
    void onupdate(float v){
        myMaterial.SetFloat("_WaterHeight",v);
    }
    
    void oncomplete(){
        myMaterial.SetFloat("_WaterHeight",currentHeight);
        if(tweenAlpha){
            SetAlpha();
        }else{
            CallBack();
        }
    }

    void SetAlpha(){
        Hashtable args = new Hashtable();
        args.Add("from",0);
        args.Add("to",1);
        args.Add("time",1);
        args.Add("easetype",iTween.EaseType.linear);
        args.Add("onstart","onstartalpha");
        args.Add("onstarttarget",this.gameObject);
        args.Add("onupdate","onupdatealpha");
        args.Add("onupdatetarget",this.gameObject);
        args.Add("oncomplete","oncompletealpha");
        args.Add("oncompletetarget",this.gameObject);
        iTween.ValueTo(this.gameObject,args);
    }

    void onstartalpha(){

    }
  
    void onupdatealpha(int v){
        currentColor.a = 1;
        myMaterial.SetColor("_Color",currentColor);
    }

    void oncompletealpha(){
        currentColor.a = 0;
        myMaterial.SetColor("_Color",currentColor);
        CallBack();
    }

    private void CallBack(){
        if(WaterComplete!=null){
            WaterComplete();
        }
    }

	public void CreateWaterPlane()
	{
		int widthSegments = waterSubdivisions;
		int lengthSegments = 1;
		
		MeshFilter tempMeshfilter = GetComponent<MeshFilter>();
#if UNITY_EDITOR
		if (Application.isEditor)
			DestroyWater();
		if (tempMeshfilter != null)
			DestroyImmediate(tempMeshfilter);
		if (GetComponent<Renderer>() != null)
			DestroyImmediate(GetComponent<Renderer>());
#endif
        proceduralMesh = new Mesh();
        proceduralMesh.name = "Water plane";
		
        int hCount2 = widthSegments+1;
        int vCount2 = lengthSegments+1;

        int numTriangles = widthSegments * lengthSegments * 6;
        int numVertices = hCount2 * vCount2;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        float uvFactorX = 1.0f/widthSegments;
        float uvFactorY = 1.0f/lengthSegments;
        float scaleX = width/widthSegments;
        float scaleY = height/lengthSegments;

        int index = 0;
        for (float y = 0.0f; y < vCount2; y++)
        {
            for (float x = 0.0f; x < hCount2; x++)
            {
                vertices[index] = new Vector3(x*scaleX - width/2f, y*scaleY - height/2f, 0.0f);
				uvs[index++] = new Vector2(x*uvFactorX, y*uvFactorY);
            }
        }

        index = 0;
        for (int y = 0; y < lengthSegments; y++)
        {
            for (int x = 0; x < widthSegments; x++)
            {
                triangles[index]   = (y     * hCount2) + x;
                triangles[index+1] = ((y+1) * hCount2) + x;
                triangles[index+2] = (y     * hCount2) + x + 1;

                triangles[index+3] = ((y+1) * hCount2) + x;
                triangles[index+4] = ((y+1) * hCount2) + x + 1;
                triangles[index+5] = (y     * hCount2) + x + 1;

                index += 6;
            }
        }

		//Vertex color:
		Color[] colors = new Color[vertices.Length];
		for (int i = 0; i <(int)(vertices.Length*0.5f); i++) //Deep vertex
		{
			colors[i] = deepColor;
		}
		for (int i = (int)(vertices.Length*0.5f); i <vertices.Length; i++) //Surface vertex
		{
			colors[i] = surfaceColor;
		}

		//Asign mesh properties
		proceduralMesh.vertices 	= vertices;
		proceduralMesh.triangles 	= triangles;
		proceduralMesh.uv 			= uvs;
		proceduralMesh.colors		= colors;

        MeshFilter myMeshFilter = gameObject.AddComponent<MeshFilter>();
        myMeshFilter.mesh = proceduralMesh;
        
        gameObject.AddComponent<MeshRenderer>();
        
        GetComponent<Renderer>().sharedMaterial = waterMaterial;
	}
	
	public void DestroyWater()
	{
		DestroyImmediate(GetComponent<Renderer>());
		DestroyImmediate(GetComponent<Collider>());
		DestroyImmediate(GetComponent<MeshFilter>());
		DestroyImmediate(GetComponent<LineRenderer>());	
	}
}
