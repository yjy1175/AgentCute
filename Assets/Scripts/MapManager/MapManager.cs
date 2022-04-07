using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleToneMaker<MapManager>
{
    #region variables
    private Transform[,] tileMapList = new Transform[3, 3];
    private Vector3 nowPos;
    private int baseY = 19;
    private int baseX = 25;
    bool isChange = false;
    #endregion

    #region method
    void Start()
    {
        // 현재 플레이어 위치 세팅
        nowPos = Vector3.zero;
        // 맵 리스트 초기화
        initMapList();
    }
    void Update()
    {
        moveMap();
        Debug.Log(nowPos);
    }

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
            string tmp = map.name.Split('/')[1];
            string[] pos = tmp.Split(',');
            int y = int.Parse(pos[0]);
            int x = int.Parse(pos[1]);
            tileMapList[y, x] = map;
        }
        isChange = false;
    }

    // 플레이어의 위치에 따라 맵을 이동시켜줍니다.
    private void moveMap()
    {
        // 매 프레임마다 플레이어의 위치를 받아온다
        // 추후에 플레이어 매니저를 통하여 받아오도록 수정
        Vector3 playerPos = GameObject.Find("PlayerObject").transform.position;
        // Todo : 플레이어의 위치증감에 따라 맵을 이동시켜주는 로직 구현
        float changeY = playerPos.y - nowPos.y;
        float changeX = playerPos.x - nowPos.x;
        // 맵이동이 있었는지 판별
        if (!isChange)
        {
            // 상
            if (changeY >= baseY)
            {
                isChange = true;
                moveTileMap(2, 0, 3, 0);
                moveTileMap(2, 1, 3, 0);
                moveTileMap(2, 2, 3, 0);
                nowPos.y += baseY;
                tileMapList[2, 0].name = "TilemapSet/" + 0 + "," + 0;
                tileMapList[2, 1].name = "TilemapSet/" + 0 + "," + 1;
                tileMapList[2, 2].name = "TilemapSet/" + 0 + "," + 2;
                tileMapList[1, 0].name = "TilemapSet/" + 2 + "," + 0;
                tileMapList[1, 1].name = "TilemapSet/" + 2 + "," + 1;
                tileMapList[1, 2].name = "TilemapSet/" + 2 + "," + 2;
                tileMapList[0, 0].name = "TilemapSet/" + 1 + "," + 0;
                tileMapList[0, 1].name = "TilemapSet/" + 1 + "," + 1;
                tileMapList[0, 2].name = "TilemapSet/" + 1 + "," + 2;
            }
            // 하
            else if (changeY <= -baseY)
            {
                isChange = true;
                moveTileMap(0, 0, -3, 0);
                moveTileMap(0, 1, -3, 0);
                moveTileMap(0, 2, -3, 0);
                nowPos.y -= baseY;
                tileMapList[0, 0].name = "TilemapSet/" + 2 + "," + 0;
                tileMapList[0, 1].name = "TilemapSet/" + 2 + "," + 1;
                tileMapList[0, 2].name = "TilemapSet/" + 2 + "," + 2;
                tileMapList[1, 0].name = "TilemapSet/" + 0 + "," + 0;
                tileMapList[1, 1].name = "TilemapSet/" + 0 + "," + 1;
                tileMapList[1, 2].name = "TilemapSet/" + 0 + "," + 2;
                tileMapList[2, 0].name = "TilemapSet/" + 1 + "," + 0;
                tileMapList[2, 1].name = "TilemapSet/" + 1 + "," + 1;
                tileMapList[2, 2].name = "TilemapSet/" + 1 + "," + 2;
            }
            // 좌
            else if (changeX <= -baseX)
            {
                isChange = true;
                moveTileMap(0, 2, 0, -3);
                moveTileMap(1, 2, 0, -3);
                moveTileMap(2, 2, 0, -3);
                nowPos.x -= baseX;
                tileMapList[0, 2].name = "TilemapSet/" + 0 + "," + 0;
                tileMapList[1, 2].name = "TilemapSet/" + 1 + "," + 0;
                tileMapList[2, 2].name = "TilemapSet/" + 2 + "," + 0;
                tileMapList[0, 1].name = "TilemapSet/" + 0 + "," + 2;
                tileMapList[1, 1].name = "TilemapSet/" + 1 + "," + 2;
                tileMapList[2, 1].name = "TilemapSet/" + 2 + "," + 2;
                tileMapList[0, 0].name = "TilemapSet/" + 0 + "," + 1;
                tileMapList[1, 0].name = "TilemapSet/" + 1 + "," + 1;
                tileMapList[2, 0].name = "TilemapSet/" + 2 + "," + 1;
            }
            // 우
            else if (changeX >= baseX)
            {
                isChange = true;
                moveTileMap(0, 0, 0, 3);
                moveTileMap(1, 0, 0, 3);
                moveTileMap(2, 0, 0, 3);
                nowPos.x += baseX;
                tileMapList[0, 0].name = "TilemapSet/" + 0 + "," + 2;
                tileMapList[1, 0].name = "TilemapSet/" + 1 + "," + 2;
                tileMapList[2, 0].name = "TilemapSet/" + 2 + "," + 2;
                tileMapList[0, 1].name = "TilemapSet/" + 0 + "," + 0;
                tileMapList[1, 1].name = "TilemapSet/" + 1 + "," + 0;
                tileMapList[2, 1].name = "TilemapSet/" + 2 + "," + 0;
                tileMapList[0, 2].name = "TilemapSet/" + 0 + "," + 1;
                tileMapList[1, 2].name = "TilemapSet/" + 1 + "," + 1;
                tileMapList[2, 2].name = "TilemapSet/" + 2 + "," + 1;
            }
        }
        // 만약 변화가 있었다면 타일 배열을 초기화 해준다.
        if (isChange)
        {
            initMapList();
        }
    }

    private void moveTileMap(int _y, int _x, int cy, int cx)
    {
        Transform moveTilemap = tileMapList[_y, _x];
        moveTilemap.position = moveTilemap.position + new Vector3(baseX * cx, baseY * cy, 0);
    }
    #endregion
}
