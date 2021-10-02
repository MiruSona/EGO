# EGO

## 프로젝트 설명
* 제주도 게임잼에서 창의적인 게임상을 탄 게임
* 무박 3일간 개발
* '두려운 부분도 나의 일부분'이라는 철학적인 내용을 담은 게임

## 영상(이미지 클릭 시 유투브로 이동)
[![EGO](https://img.youtube.com/vi/PL0v7AAMD80/0.jpg)](https://youtu.be/PL0v7AAMD80 "EGO")

## 개발환경
* Unity 5.3.2f1버전으로 개발
* iTween API 사용
* 안드로이드 버전으로 개발

## 프로젝트 팀원 및 역할
* 그래픽, 기획, 프로그래머 3인 개발
* 나의 역할 : 프로그래머

## 개발 기능
* 기본적인 플레이어 이동 및 UI
	* 방향 키 UI를 터치하면 지정 방향으로 이동하도록 설정
* 피격함정, 트리거 방식 함정, 슬로우 함정
	* 피격함정 : 닿은 직후에만 데미지와 함께 가시가 튀어나오는 애니메이션
	* 트리거 방식 함정 : 투명한 충돌체에 닿으면 iTween으로 이동경로를
	지정해둔 함정이 지나감. 닿으면 데미지 판정
	* 슬로우 함정 : 데미지는 없으며, 함정과 닿아있는 동안 느려짐
* 쫓아오는 그림자
	* iTween으로 정해진 경로대로 이동
	* 엔딩 연출 시 이동 중지
	* 플레이어와 닿으면 바로 데드씬 나오도록 설정
	* 엔딩 연출에 나오는 그림자는 다른 오브젝트다
* 안개 효과
	* 특수효과용 카메라를 따로 만들어 안개 스프라이트를 배치
* 엔딩 연출
	* 미로의 끝에있는 투명 충돌체에 닿으면 연출 시작되도록 설정
	* 연출은 스프라이트 애니메이션과 iTween을 사용해 구현
	