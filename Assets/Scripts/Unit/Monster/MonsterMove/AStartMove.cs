using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) 
    {
        mIsWall = _isWall;
        mX = _x;
        mY = _y; 
    }
    // 해당 노드가 벽인지 아닌지 판단
    private bool mIsWall;
    public bool IsWall
    {
        get { return mIsWall; }
    }
    // 해당 노드의 부모노드(어느 노드로부터 왔는지)
    private Node mParentNode;
    public Node ParentNode
    {
        get { return ParentNode; }
        set { mParentNode = value; }
    }

    // 해당 노드의 x, y좌표
    private int mX;
    public int X
    {
        get { return mX; }
    }
    private int mY;
    public int Y
    {
        get { return mY; }
    }
    // G : 시작으로부터 이동했던 거리
    // H : |가로|+|세로| 장애물 무시하여 목표까지의 거리
    // F : G + H
    private int mG;
    public int G
    {
        get { return mG; }
    }
    private int mH;
    public int H
    {
        get { return mH; }
    }
    private int mF;
    public int F
    { 
        get{ return mG + mH; }
    }
}

public class AStartMove : MonoBehaviour
{
    // startPos : 몬스터의 위치
    // targetPos : 플레이어의 위치
    [SerializeField]
    private Vector2Int mStartPos, mTargetPos;
    // 최종 길찾기를 완료한 노드리스트
    [SerializeField]
    private List<Node> mFinalNodeList;
    // 대각선 이동
    [SerializeField]
    private bool mAllowDaigonal;


    private int mSizeX, mSizeY;
    // startPos와 targetPos를 기준으로한 가상의 사각형내의 
    [SerializeField]
    private Node[,] mCurrentNodeArray;
    // 시작노드, 종료노드, 현재노드
    [SerializeField]
    private Node mStartNode, mTargetNode, mCurrentNode;
    // 
    [SerializeField]
    private List<Node> mOpenList, mCloseList;

}
