using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cameraObject;


    public float moveSpeed = 10f;
    [SerializeField] private int scale = 1;
    public int minScale = 1;
    public int maxScale = 3;
    private int screenHeight = 360;
    private int screenWidth = 640;
    public Vector2Int screenResolution { get { return new Vector2Int(screenWidth, screenHeight); } }

    // Hex map
    public MapPattern mapPattern;

    // Event manager
    //public PlayerCameraEventManager eventManager { get; private set; } = new PlayerCameraEventManager();

    // Camera bounds
    public struct CameraBounds
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float width { get { return xMax - xMin; } }
        public float height { get { return yMax - yMin; } }

        public CameraBounds(float xMin, float xMax, float yMin, float yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }

        public void SetBounds(float xMin, float xMax, float yMin, float yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CameraBounds))
                return false;

            CameraBounds bounds = (CameraBounds)obj;
            if (xMin == bounds.xMin && xMax == bounds.xMax && yMin == bounds.yMin && yMax == bounds.yMax)
                return true;
            return false;
        }

        public override string ToString()
        {
            return "(" + xMin + ", " + xMax + "; " + yMin + ", " + yMax + ")";
        }
    }
    public CameraBounds cameraBounds = new CameraBounds();


    // Instantiate
    public static PlayerCamera InstantiatePlayerCamera()
    {
        PlayerCamera camera = Instantiate(Resources.Load<PlayerCamera>("Prefabs/Player/Player Camera"));
        camera.Initialize();
        return camera;
    }


    // Initialize
    public void Initialize()
    {
        Debug.Log("Initializing player camera");
        CalculateOrthographicSize();
    }


    // Reset
    public void Reset()
    {
        CalculateOrthographicSize();
    }


    // Subscribe to piece events
    public void SubscribePieceEvents(Piece piece)
    {
        piece.eventManager.onCenterCameraOnPiece.Subscribe(CenterCameraOnPiece);
    }
    public void UnsubscribePieceEvents(Piece piece)
    {
        piece.eventManager.onCenterCameraOnPiece.Unsubscribe(CenterCameraOnPiece);
    }


    // Set map pattern
    public void SetMapPattern(MapPattern pattern)
    {
        mapPattern = pattern;
        CalculateCameraBounds();
    }


    // Set screen resolution
    public void SetResolutionWidth(int width)
    {
        screenWidth = width;
        CalculateOrthographicSize();
    }
    public void SetResolutionHeight(int height)
    {
        screenHeight = height;
        CalculateOrthographicSize();
    }
    public void SetResolution(Vector2Int resolution)
    {
        screenWidth = resolution.x;
        screenHeight = resolution.y;
        CalculateOrthographicSize();
    }



    // Set scale
    public int GetScale()
    {
        return scale;
    }
    public void SetScale(int scale)
    {
        this.scale = Mathf.Clamp(scale, minScale, maxScale);
        CalculateOrthographicSize();
    }


    // Zoom camera
    public void ZoomCamera(float moveZ)
    {
        if (moveZ == 0)
            return;

        SetScale(scale + (int)Mathf.Sign(moveZ));
        CalculateOrthographicSize();
    }


    // Move camera
    public void MoveCamera(Vector3 position, bool ignoreBounds = false)
    {
        if (!ignoreBounds)
            cameraObject.transform.position = new Vector3(
                Mathf.Clamp(position.x, cameraBounds.xMin, cameraBounds.xMax),
                Mathf.Clamp(position.y, cameraBounds.yMin, cameraBounds.yMax),
                -10
            );
        else
            cameraObject.transform.position = new Vector3(position.x, position.y, -10);
        //eventManager.OnMove(cameraObject.transform.position);
    }
    private void CenterCameraOnPiece(Piece piece, bool ignoreBounds)
    {
        MoveCamera(piece.transform.position, ignoreBounds);
    }


    // Calculate camera bounds
    public void CalculateCameraBounds()
    {
        if (mapPattern == null)
            return;

        // Y Bounds
        int mapHeight = mapPattern.mapPixelHeight + Hex.HEX_HEIGHT * 2;
        int heightDiff = (int)(mapHeight - (cameraObject.orthographicSize * 200));
        Debug.Log("map height: " + mapHeight);
        Debug.Log("height diff: " + heightDiff);
        Debug.Log("screen height: " + (cameraObject.orthographicSize * 200));

        float yOffset;
        float minYPos = 0;
        float maxYPos = 0;
        if (heightDiff > 0)
        {
            yOffset = (float)heightDiff / 100 / 2;
            minYPos += -yOffset;
            maxYPos += yOffset;
            Debug.Log("y offset: " + yOffset);
        }

        // X Bounds
        int mapWidth = mapPattern.mapPixelWidth + Hex.HEX_WIDTH * 2;
        float baseOrthoSize = screenHeight / 200f;
        int currentScreenWidth = (int)(screenWidth * cameraObject.orthographicSize / baseOrthoSize);
        int widthDiff = mapWidth - currentScreenWidth;
        Debug.Log("map width: " + mapWidth);
        Debug.Log("current screen width: " + currentScreenWidth);

        float xOffset;
        float minXPos = 0;
        float maxXPos = 0;
        if (widthDiff > 0)
        {
            xOffset = (float)widthDiff / 100 / 2;
            minXPos += -xOffset;
            maxXPos += xOffset;
        }
        cameraBounds.SetBounds(minXPos, maxXPos, minYPos, maxYPos);
        MoveCamera(cameraObject.transform.position);
    }


    // Calculate orthographic size
    public void CalculateOrthographicSize()
    {
        float orthoSize = (float)screenHeight / (200 * scale);
        cameraObject.orthographicSize = orthoSize;
        CalculateCameraBounds();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
