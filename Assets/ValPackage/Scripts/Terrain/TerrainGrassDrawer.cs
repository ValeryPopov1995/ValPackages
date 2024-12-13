using UnityEngine;

namespace ValeryPopov.Common.TerrainDraw
{
    public class TerrainGrassDrawer : MonoBehaviour
    {
        [SerializeField] private Terrain _terrain;
        private TerrainData _terrainData;
        private float[,,] _defaultAlphaMaps;

        private void Awake()
        {
            _terrainData = _terrain.terrainData;
            _defaultAlphaMaps = _terrainData.GetAlphamaps(0, 0, _terrainData.alphamapWidth, _terrainData.alphamapHeight);
        }

        private void OnDestroy()
        {
            _terrainData.SetAlphamaps(0, 0, _defaultAlphaMaps);
        }

        public float GetAlphaLayer01(Vector3 point, int alphaLayer)
        {
            var mapPoint = World2AlphaMap(point);
            return _terrainData.GetAlphamaps(mapPoint.x, mapPoint.y, 1, 1)[0, 0, alphaLayer];
        }

        public void Draw(Vector3 point, float alpha, int alphaLayer, bool redrawOthers = true)
        {
            Vector2Int alphaPoint = World2AlphaMap(point);

            var map = _terrainData.GetAlphamaps(alphaPoint.x, alphaPoint.y, 1, 1);
            map[0, 0, alphaLayer] = alpha;

            if (redrawOthers)
                RedrawOtherLayers(map, alphaLayer);

            _terrainData.SetAlphamaps(alphaPoint.x, alphaPoint.y, map);
        }

        private float[,,] RedrawOtherLayers(float[,,] alphaMap, int excludeLayer)
        {
            for (int x = 0; x < alphaMap.GetLength(0); x++)
            {
                for (int y = 0; y < alphaMap.GetLength(1); y++)
                {
                    float sumAlpha = 1 - alphaMap[x, y, excludeLayer];

                    for (int layer = 0; layer < alphaMap.GetLength(2); layer++)
                    {
                        if (layer == excludeLayer) continue;
                        alphaMap[x, y, layer] *= sumAlpha;
                    }
                }
            }

            return alphaMap;
        }

        public void DrawGradientCircle(Vector3 point, float radius)
        {
            Vector3 leftDown = point + Vector3.back * radius + Vector3.left * radius;
            //Vector3 rightUp = point + Vector3.forward * radius + Vector3.right * radius;

            int width = Mathf.RoundToInt(radius * 2 / _terrainData.size.x * _terrainData.alphamapWidth);
            //int height = Mathf.RoundToInt(radius * 2 / _terrainData.size.z * _terrainData.alphamapHeight);
            int height = width;

            Vector2Int mapLeftDown = new(World2AlphaMap(leftDown).x, World2AlphaMap(leftDown).y);
            var maps = _terrainData.GetAlphamaps(mapLeftDown.x, mapLeftDown.y, width, height);

            float mapRadius = width / 2f;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float lerp01 = new Vector2(x - mapRadius, y - mapRadius).magnitude / mapRadius;
                    float alpha = Mathf.Clamp01(1 - lerp01);

                    if (maps[x, y, 1] < alpha)
                    {
                        maps[x, y, 1] = alpha;
                        maps[x, y, 0] = 1 - alpha;
                    }
                }
            }

            _terrainData.SetAlphamaps(mapLeftDown.x, mapLeftDown.y, maps);
            //_terrain.Flush();
        }

        private Vector2Int World2AlphaMap(Vector3 point)
        {
            point -= _terrain.transform.position;
            Vector2 lerp = new(point.x / _terrainData.size.x,
                point.z / _terrainData.size.z);

            Vector2Int map = new(Mathf.RoundToInt(_terrainData.alphamapWidth * lerp.x),
                Mathf.RoundToInt(_terrainData.alphamapHeight * lerp.y));

            return map;
        }
    }
}