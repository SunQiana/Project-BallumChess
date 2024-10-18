using UnityEngine;
using System.Threading;

namespace Grid
{
    public class Grid
    {
        int width; //x
        int height; //z
        float cellSize;
        float[,] gridArray;

        public Grid(int width, int height, float cellSize = 1f)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            gridArray = new float[width, height];
        }

        bool IfGridPosInvalid(int x, int z)
        {
            if (x >= width || z >= height || x < 0 || z < 0)
            {
                Debug.LogError($"Invalid Grid Pos Value({x},{z})");
                return true;
            }

            return false;
        }

        public Vector3 GetWorldPosByGridPos(int x, int z)
        {
            return new Vector3(x * cellSize, 0, z * cellSize);
        }

        public void SetValue(int x, int z, float value)
        {
            if (IfGridPosInvalid(x, z))
                return;

            gridArray[x, z] = value;
        }

        public void SetValue(Vector3 worldPos, float value)
        {
            int x, z;
            TryGetXZ(worldPos, out x, out z);
            SetValue(x, z, value);
        }

        public bool TryGetXZ(Vector3 worldPos, out int x, out int z)
        {
            var xValue = Mathf.FloorToInt(worldPos.x / cellSize);
            var zValue = Mathf.FloorToInt(worldPos.z / cellSize);

            if (IfGridPosInvalid(xValue, zValue))
            {
                x = 0;
                z = 0;
                Debug.LogWarning($"Target Pos {worldPos} Is Out Of Grid");
                return false;
            }

            x = xValue;
            z = zValue;
            return true;
        }

        public bool TryGetValue(int x, int z, out float value)
        {
            if (IfGridPosInvalid(x, z))
            {
                value = 0f;
                return false;
            }

            value = gridArray[x, z];
            return true;
        }

        public bool TryGetValue(Vector3 worldPos, out float value)
        {
            int x, z;
            TryGetXZ(worldPos, out x, out z);

            if (TryGetValue(x, z, out value))
                return true;

            return false;
        }

        public void GenDebugGridLine(int x, int z, CancellationToken token)
        {
            float scaledX = x * cellSize;
            float scaledZ = z * cellSize;

            while (!token.IsCancellationRequested)
            {
                DrawGrid();
                DrawBoarder();
            }

            void DrawBoarder()
            {
                Vector3 ForwardZ = new Vector3(scaledZ, 0, 0);
                Vector3 ForwardX = new Vector3(0, 0, scaledX);
                Vector3 ForwardXZ = new Vector3(scaledZ, 0, scaledX);

                Debug.DrawLine(Vector3.zero, ForwardZ, Color.red);
                Debug.DrawLine(Vector3.zero, ForwardX, Color.red);
                Debug.DrawLine(ForwardX, ForwardXZ, Color.red);
                Debug.DrawLine(ForwardZ, ForwardXZ, Color.red);
            }

            void DrawGrid()
            {
                for (int lineX = 0; lineX <= width; lineX++)
                {
                    float posX = lineX * cellSize;
                    Debug.DrawLine(new Vector3(posX, 0, 0), new Vector3(posX, 0, height * cellSize), Color.black);
                }

                for (int lineZ = 0; lineZ <= height; lineZ++)
                {
                    float posZ = lineZ * cellSize;
                    Debug.DrawLine(new Vector3(0, 0, posZ), new Vector3(width * cellSize, 0, posZ), Color.black);
                }
            }
        }



    }
}
