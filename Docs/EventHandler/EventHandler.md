
# EventHandler

## delegate와 event를 활용한 Observing Pattern
* 매 update마다 특정 GameObject의 값을 체크하는게 아닌 변화가 생겼을 때 정보를 얻고싶은 값에 대해 Event를 등록하여 
값이 변화할때마다 등록된 EventHandler들에 대해 Invoke하여 변화를 알려주는 방식입니다.

* 각 object의 정보에 대한 Observer를 추가하는데 있어 확장이 열려있어 구현이 용이합니다.

## TroubleShooting
* 몬스터가 죽을 경우 경험치가 2배로 들어오는 버그가 발생했습니다. Hp가 0이하일 경우를 조건으로 경험치를 주는 형태로 update문에서 구현하였는데 구조적으로 hp 0이하에서 체크하는건
여러번 발새할 수 있어 경험치가 두배로 들어오는건 언제든 발생할 수 있어 발생한 이슈였습니다. 
EventHandler로 Monster가 hp가 0일때 die를 체크하고 die에 대한 EventHandler를 추가하여 die에 대해 중복으로 값을 확인하는 이슈를 막고,
Monster가 Die가 될시에만 경험치를 올려주는 방식으로 수정하여 이슈를 해결하였습니다.
