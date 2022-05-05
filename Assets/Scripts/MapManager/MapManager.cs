using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingleToneMaker<MapManager>
{
    #region variables
    // 추후에 타일맵 클래스 생성 예정(데이터셋과 함께)
    [SerializeField]
    private StringGameObject maps;
    [SerializeField]
    private Transform[,] tileMapList = new Transform[3, 3];
    [SerializeField]
    private MapType mCurrentMapType;

    public MapType CurrentMapType
    {
        get { return mCurrentMapType; }
        set { mCurrentMapType = value; }
    }

    [SerializeField]
    private Vector3 nowPos;
    [SerializeField]
    private int baseY;
    [SerializeField]
    private int baseX;
    [SerializeField]
    private bool mIsChange = false;
    [SerializeField]
    private bool mIsUpdate = false;

    public enum MapType
    {
        Field,
        Volcano,
        Ice,
        Dungeon,
        BossRelay,
        Exit
    }
    #endregion
    
    #region method
    void Start()
    {
        // 현재 플레이어 위치 세팅
        nowPos = Vector3.zero;
        // 선택된 맵 리스트 초기화
        // 맵 데이터 파싱후 넣기
        MapDataParse();
    }
    void Update()
    {
        if(mIsUpdate)
            moveMap();
    }
    private void MapDataParse()
    {
        // 프리펩을 불러와서
        GameObject[] mapList = Resources.LoadAll<GameObject>("Prefabs/Map");
        // Dic에 저장
        for (int i = 0; i < mapList.Length; i++)
            maps.Add(mapList[i].name, mapList[i]);

        // csv파일 읽기
        List<Dictionary<string, object>> mapData = CSVReader.Read("CSVFile/Map");

        for(int i = 0; i < mapList.Length; i++)
        {
            Map item = maps[mapData[i]["MapInName"].ToString()].GetComponent<Map>();

            item.ID = int.Parse(mapData[i]["ID"].ToString());
            item.Width = int.Parse(mapData[i]["Width"].ToString());
            item.Height = int.Parse(mapData[i]["Height"].ToString());
        }
    }
    /*
     *  맵에 따라 나오는 몬스터가 다르니
     *  return 값으로 해당 맵의 이름을 반환해준다
     *  추후에 몬스터 스폰에서 api이용하시면 됩니다.
     */
    public void RandMapSelect()
    {
        // 맵번호를 랜덤으로 뽑고
        MapType  ranNum = (MapType)Random.Range((int)MapType.Field, (int)MapType.Exit);
        //CurrentMapType = MapType.Volcano;

        // 해당 맵을 Dic에서 불러온후
        GameObject newMap = maps[mCurrentMapType.ToString()];
        // Grid안에 생성해준다
        Instantiate(newMap, Vector3.zero, Quaternion.identity, GameObject.Find("Grid").transform);
        // 맵을 초기화 해준다.
        baseX = newMap.GetComponent<Map>().Width;
        baseY = newMap.GetComponent<Map>().Height;
        if(baseX > 0 && baseY > 0)
        {
            initMapList();
            moveTileMap(0, 0, 1, -1);
            moveTileMap(0, 1, 1, 0);
            moveTileMap(0, 2, 1, 1);
            moveTileMap(1, 0, 0, -1);
            moveTileMap(1, 1, 0, 0);
            moveTileMap(1, 2, 0, 1);
            moveTileMap(2, 0, -1, -1);
            moveTileMap(2, 1, -1, 0);
            moveTileMap(2, 2, -1, 1);
            mIsUpdate = true;
        }
    }

    //DEBUG용 맵선택 API
    public void MapSelect()
    {
        // 맵번호를 랜덤으로 뽑고
        //MapType  ranNum = (MapType)Random.Range((int)MapType.Field, (int)MapType.Exit);
        //CurrentMapType = MapType.Volcano;
        
        // 해당 맵을 Dic에서 불러온후
        GameObject newMap = maps[mCurrentMapType.ToString()];
        // Grid안에 생성해준다
        Instantiate(newMap, Vector3.zero, Quaternion.identity, GameObject.Find("Grid").transform);
        // 맵을 초기화 해준다.
        baseX = newMap.GetComponent<Map>().Width;
        baseY = newMap.GetComponent<Map>().Height;
        if (baseX > 0 && baseY > 0)
        {
            initMapList();
            moveTileMap(0, 0, 1, -1);
            moveTileMap(0, 1, 1, 0);
            moveTileMap(0, 2, 1, 1);
            moveTileMap(1, 0, 0, -1);
            moveTileMap(1, 1, 0, 0);
            moveTileMap(1, 2, 0, 1);
            moveTileMap(2, 0, -1, -1);
            moveTileMap(2, 1, -1, 0);
            moveTileMap(2, 2, -1, 1);
            mIsUpdate = true;
        }
    }
    /*
     * 해당 position에 object타일맵의 타일이 있는지 판단해주는 api입니다.
     * _pos : 스폰할 위치
     * return : true면 가능 false면 불가능
     */
    private void initMapList()
    {
        // 상위 부모그룹 Grid를 불러온다
        Transform grid = GameObject.Find("Grid").transform;
        // Gird의 자식 타일맵셋을 불러온다.
        Transform[] mapList = grid.GetComponentsInChildren<Transform>();
        foreach(Transform map in mapList)
        {
            // 상위 부모와 하위자식은 제외
            if (map.name == grid.name || map.CompareTag("Tilemap"))
                continue;
            // 각 자식들의 위치를 파싱하여 해당 위치로 넣어준다.
            string[] pos = map.name.Split(',');
            int y;
            int x;
            int.TryParse(pos[0], out y);
            int.TryParse(pos[1], out x);
            tileMapList[y, x] = map;
        }
        mIsChange = false;
    }

    // 플레이어의 위치에 따라 맵을 이동시켜줍니다.
    private void moveMap()
    {
        // 매 프레임마다 플레이어의 위치를 받아온다
        // 추후에 플레이어 매니저를 통하여 받아오도록 수정
        Vector3 playerPos = PlayerManager.Instance.Player.transform.position;
        // Todo : 플레이어의 위치증감에 따라 맵을 이동시켜주는 로직 구현
        float changeY = playerPos.y - nowPos.y;
        float changeX = playerPos.x - nowPos.x;
        // 맵이동이 있었는지 판별
        if (!mIsChange)
        {
            // 상
            if (changeY >= baseY)
            {
                mIsChange = true;
                moveTileMap(2, 0, 3, 0);
                moveTileMap(2, 1, 3, 0);
                moveTileMap(2, 2, 3, 0);
                nowPos.y += baseY;
                tileMapList[2, 0].name = 0 + "," + 0;
                tileMapList[2, 1].name = 0 + "," + 1;
                tileMapList[2, 2].name = 0 + "," + 2;
                tileMapList[1, 0].name = 2 + "," + 0;
                tileMapList[1, 1].name = 2 + "," + 1;
                tileMapList[1, 2].name = 2 + "," + 2;
                tileMapList[0, 0].name = 1 + "," + 0;
                tileMapList[0, 1].name = 1 + "," + 1;
                tileMapList[0, 2].name = 1 + "," + 2;
            }
            // 하
            else if (changeY <= -baseY)
            {
                mIsChange = true;
                moveTileMap(0, 0, -3, 0);
                moveTileMap(0, 1, -3, 0);
                moveTileMap(0, 2, -3, 0);
                nowPos.y -= baseY;
                tileMapList[0, 0].name = 2 + "," + 0;
                tileMapList[0, 1].name = 2 + "," + 1;
                tileMapList[0, 2].name = 2 + "," + 2;
                tileMapList[1, 0].name = 0 + "," + 0;
                tileMapList[1, 1].name = 0 + "," + 1;
                tileMapList[1, 2].name = 0 + "," + 2;
                tileMapList[2, 0].name = 1 + "," + 0;
                tileMapList[2, 1].name = 1 + "," + 1;
                tileMapList[2, 2].name = 1 + "," + 2;
            }
            // 좌
            else if (changeX <= -baseX)
            {
                mIsChange = true;
                moveTileMap(0, 2, 0, -3);
                moveTileMap(1, 2, 0, -3);
                moveTileMap(2, 2, 0, -3);
                nowPos.x -= baseX;
                tileMapList[0, 2].name = 0 + "," + 0;
                tileMapList[1, 2].name = 1 + "," + 0;
                tileMapList[2, 2].name = 2 + "," + 0;
                tileMapList[0, 1].name = 0 + "," + 2;
                tileMapList[1, 1].name = 1 + "," + 2;
                tileMapList[2, 1].name = 2 + "," + 2;
                tileMapList[0, 0].name = 0 + "," + 1;
                tileMapList[1, 0].name = 1 + "," + 1;
                tileMapList[2, 0].name = 2 + "," + 1;
            }
            // 우
            else if (changeX >= baseX)
            {
                mIsChange = true;
                moveTileMap(0, 0, 0, 3);
                moveTileMap(1, 0, 0, 3);
                moveTileMap(2, 0, 0, 3);
                nowPos.x += baseX;
                tileMapList[0, 0].name = 0 + "," + 2;
                tileMapList[1, 0].name = 1 + "," + 2;
                tileMapList[2, 0].name = 2 + "," + 2;
                tileMapList[0, 1].name = 0 + "," + 0;
                tileMapList[1, 1].name = 1 + "," + 0;
                tileMapList[2, 1].name = 2 + "," + 0;
                tileMapList[0, 2].name = 0 + "," + 1;
                tileMapList[1, 2].name = 1 + "," + 1;
                tileMapList[2, 2].name = 2 + "," + 1;
            }
        }
        // 맵이동이 있었다면 타일 배열을 초기화 해준다.
        if (mIsChange)
        {
            initMapList();
        }
    }
    /*
     * 타일맵을 월드 좌표계에서 옮겨주는 함수
     * _y : 타일맵리스트의 y좌표
     * _x : 타일맵리스트의 x좌표
     * cy : 좌표계에서 옮길 y좌표의 계수
     * cx : 좌표계에서 옮길 x좌표의 계수
     */
    private void moveTileMap(int _y, int _x, int cy, int cx)
    {
        tileMapList[_y, _x].position = tileMapList[_y, _x].position + new Vector3(baseX * cx, baseY * cy, 0);
    }
    #endregion
}
