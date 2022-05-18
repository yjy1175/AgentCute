# load&save

## Unity 내장 라이브러리 JsonUtility를 사용
* 유저데이터가 필요한 상황에서 Json형식으로 저장되어있던 유저의 데이터를 JsonUtility를 사용하여 해당 Class 타입으로 변경 후 Load.
* 데이터가 필시 저장되어야 하는 상황에서 Class 타입의 유저의 데이터를 JsonUtility를 사용하여 Json형식으로 변환 후 Save.

## 직렬화가 안되어있는 타입의 Load&Save
* Dictionary타입은 [serialize](../serialize/Serialize.md)를 통하여 직렬화 후 Load&Save.

## Json데이터의 노출 및 변조 방지
* Json데이터를 그대로 저장할 경우, 데이터의 노출 및 변조에 취약하다.
* [AES.cs](../../Assets/Scripts/Util/SaveLoadManager/AES/AES.cs)AES-128 암호화 방식을 채택하여 적용.

## 추가 개선방안
* 현재는 로컬에 데이터를 저장시켜, 유저가 어플리케이션을 삭제하면 데이터도 모두 삭제된다. 추후에 데이터서버나 구글 클라우드 서비스를 통하여 유저가 어플리케이션을 삭제해도 다시 다운로드 받았을 때, 서버나 클라우드에 저장되어 있는 데이터를 불러와서 정상적으로 플레이 할 수 있게 구현.

## 참고 소스
[SaveLoadManager](../../Assets/Scripts/Util/SaveLoadManager/SaveLoadManager.cs) Save와 Load를 관리하는 Manager
[LobbyPlayerAchievementData](../../Assets/Scripts/Unit/LobbyPlayer/LobbyPlayerAchievementData.cs) 유저의 업적 데이터 Class
[LobbyPlayerInfo](../../Assets/Scripts/Unit/LobbyPlayer/Info/LobbyPlayerInfo.cs) 유저의 정보 데이터 Class
[WarInfo](../../Assets/Scripts/Unit/LobbyPlayer/Info/WarInfo.cs) 유저의 전투 진입 시 필요한 정보 데이터 Class