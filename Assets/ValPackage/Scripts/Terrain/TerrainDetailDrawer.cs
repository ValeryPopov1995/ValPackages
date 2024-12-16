using UnityEngine;

namespace ValPackage.Common.TerrainDraw
{
    public class TerrainDetailDrawer : MonoBehaviour
    {
        [SerializeField] private Terrain _terrain;
        private TerrainData _terrainData;
        int[][,] _detailMaps;

        private void Awake()
        {
            _terrainData = _terrain.terrainData;

            int layersCount = _terrainData.detailPatchCount;
            _detailMaps = new int[layersCount][,];
            for (int i = 0; i < layersCount; i++)
            {
                int[,] map = _terrainData.GetDetailLayer(0, 0, _terrainData.detailWidth, _terrainData.detailHeight, i);
                _detailMaps[i] = map;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _detailMaps.Length; i++)
                _terrainData.SetDetailLayer(0, 0, i, _detailMaps[i]);
        }



        public void Draw(Vector3 point, int detailLayer, int detailCount = 1)
        {
            Vector2Int mapPoint = World2DetailMap(point);
            int[,] detailMap = _terrainData.GetDetailLayer(mapPoint.x, mapPoint.y, 1, 1, detailLayer);
            detailMap[0, 0] = detailCount;

            _terrainData.SetDetailLayer(mapPoint.x, mapPoint.y, detailLayer, detailMap);
            _terrain.Flush();

            // also usefull
            //int numDetails = _terrainData.detailPrototypes.Length;
            //_terrainData.SetDetailLayer(0, 0, detailLayer, detailMap);
        }

        private Vector2Int World2DetailMap(Vector3 point)
        {
            point -= _terrain.transform.position;
            Vector2 lerp = new(point.x / _terrainData.size.x,
                point.z / _terrainData.size.z);

            Vector2Int map = new(Mathf.RoundToInt(_terrainData.detailWidth * lerp.x),
                Mathf.RoundToInt(_terrainData.detailHeight * lerp.y));

            return map;
        }
    }
}