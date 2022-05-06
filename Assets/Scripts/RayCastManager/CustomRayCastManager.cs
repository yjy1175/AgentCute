using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRayCastManager : SingleToneMaker<CustomRayCastManager>
{
    //상,하,좌,우,좌상,우상,좌하,우하
    private Vector3[] mVectorDir = new Vector3[8] { Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            Vector3.up+ Vector3.left, Vector3.up+ Vector3.right, Vector3.down+ Vector3.left, Vector3.down+ Vector3.right};

    //target : target의 위치
    //_x,_y : target좌표로부터 쏘고싶은 ray의 위치
    //_target: 정규화된거리에서 레이를 쏘고싶은 위치 
    //__rayDist : 쏘려는 ray의 길이
    //_drawRay : 디버그용으로 ray를 쏠지에대한 여부
    //rayPos ray가 쏴진 지역에 대한 좌표값을 얻고싶을 경우 참조
    //target위치에서 x,y떨이진 위치의 타일의 좌하단에서 _tatgetDist만큼 떨어진 길이에서 _rayDist만큼 레이를 쏴 이동가능 유무를 return
    //장애물이 있어 이동불가능이면 false
    //장애물이 없어 이동가능이면 true
    public bool NomarlizeMoveableWithRay(Vector3 _target, int _x, int _y, float _targetDist, float _rayDist, bool _drawRay, ref Vector3 _rayPos)
    {
        //레이를 쏘고 싶은 타일의 위치를 구함
        Vector3 targetPosition = _target;
        targetPosition.x += _x;
        targetPosition.y += _y;
        float distance = Vector3.Distance(targetPosition, _target);
        Vector3 dir = (targetPosition - _target).normalized * distance;
        Vector2 rayTargetPosition = new Vector2(_target.x + dir.x, _target.y + dir.y);

        //rayTargetPosition의 위치를 tile의 한가운데로 위치시켜줌
        rayTargetPosition = Vector2Int.FloorToInt(rayTargetPosition);
        rayTargetPosition.x += _targetDist;
        rayTargetPosition.y += _targetDist;
        _rayPos = rayTargetPosition;

        for (int i = 0; i < 8; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(rayTargetPosition, mVectorDir[i], _rayDist, LayerMask.GetMask("Tilemap"));
            if (ray.collider != null)
            {
                if (_drawRay)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Vector3 debugVector = mVectorDir[j] * _rayDist;
                        Debug.DrawRay(new Vector2(rayTargetPosition.x, rayTargetPosition.y), debugVector, Color.red);
                    }
                }
                return false;
            }
        }
        return true;

    }
}
