
  

# 퍼펙트 샷 : 만발의 법칙

![Logo](https://raw.githubusercontent.com/osamhack2021/APP_IoT_PerfectShot_macgyvers/main/APP/PerfectShotVR/Assets/Models/Logo/logo.PNG)

퍼펙트샷 : 만발의 법칙

언제, 어디서나, 마음대로 사격훈련

## 개요

어느 한 병사가 있었습니다. 이번에 사격훈련을 가는데, 만발을 맞히면 포상을 준다고 합니다. 분명 좋은 소식일텐데도 이 사람은 고민에 빠졌습니다. '어떻게 하면 사격을 연습할 수 있을까?'

<br>![thinking_icon](https://github.com/osamhack2021/APP_IoT_PerfectShot_macgyvers/blob/main/github_page/images/%EA%B3%A0%EB%AF%BC%ED%95%98%EB%8A%94%20%EC%9D%B4%EB%AA%A8%ED%8B%B0%EC%BD%98%20small.gif?raw=true)<br>

실제로 총을 쏴볼 수도 없고, 그렇다고 빈 총의 방아쇠를 당겨봤자 내가 잘 쏘고 있는지도 모릅니다. 결국 사격훈련 당일에서야 자신의 실력을 확인하고 그때에만 연습을 하는 수밖엔 없죠.

<br>![i got an idea](https://github.com/osamhack2021/APP_IoT_PerfectShot_macgyvers/blob/main/github_page/images/%EC%95%84%EC%9D%B4%EB%94%94%EC%96%B4%EA%B0%80%20%EB%96%A0%EC%98%A4%EB%A5%B8%20%EC%82%AC%EB%9E%8C.png?raw=true)<br>

'생활관에서도 사격 연습을 할 수 있게 만들 수 있을까?'

<br>그러자 아이디어가 번뜩입니다. 우리 모두가 가지고 있는 휴대폰으로 사격 연습을 하면 좋겠다는 생각이 들었습니다. 퍼펙트샷 : 만발의 법칙은 이를 실현시키기 위해 시작한 프로젝트입니다. 시공간의 제약 없이, 저비용으로 우리 모두가 사격 연습을 할 수 있도록 도움을 주는 앱을 만들었습니다.

<br>(이 앱으로 사격 연습을 하고 있는 모습의 사진)

  
  

## 소개

(소총에 휴대폰을 거치한 모습)

<br>우선, 총에 휴대폰을 매달아서 진행합니다.

<br>(정조준하여 휴대폰의 화면을 겨냥한 사진)

<br>앱에 있는 표적을 쏘아 맞히는 것이 목표입니다.

<br>(블루투스 컨트롤러 사진) (컨트롤러를 연결하여 방아쇠를 당기는 gif) (발사되어 타겟이 넘어가는 gif)

<br>블루투스 컨트롤러를 이용하여 방아쇠를 당겼을 때 발사가 되도록 할 수 있습니다. 이렇게 하면 더욱 실감 나는 연습이 가능합니다.

<br>(진동 모듈을 부착한 사진) (진동모듈을 부착한 채 반동을 제어하는 사진)
<br>진동 모듈을 통해서 반동 제어를 연습할 수 있습니다.

<br>(응시모드로 표적을 쏴맞히는 gif) (응시모드로 표적을 빗맞히는 gif)

<br>블루투스 컨트롤러가 없으시다구요? 걱정하지 마세요. 그런 당신을 위해 응시모드가 있습니다. 조준하고 있으면 발사가 됩니다! 오조준 시에도 발사되기 때문에 잘 보고 있어야겠죠?

<br>(영점 사격지가 다가오는 gif)

<br>이렇게 어디를 맞혔는지 확인하고 크리크 수정을 할 수 있답니다.

<br>(실거리 사격 결과가 나오는 gif)

<br>자신이 몇 발을 어떻게 어디를 맞혔는지 다 나옵니다!

<br>

![victory](https://github.com/osamhack2021/APP_IoT_PerfectShot_macgyvers/blob/main/github_page/images/%EC%84%B1%EA%B3%B5%ED%95%9C%20%EC%82%AC%EB%9E%8C.jpeg?raw=true)

<br>이 앱만 있으면 만발에 한 걸음 더 가까워지겠죠?

## 소개 영상
  (소개 영상이 들어갈 위치입니다)

## 기능 설명

목록

1. 가늠쇠 동기화
가늠쇠 동기화라 함은, 게임 내의 카메라 중앙 지점을 현실 총의 가늠쇠에 맞추는 것을 말합니다. 이러한 카메라의 움직임을 반영하기 위해, 카메라 렌즈 시프트 값을 저장하여 사용합니다.
(가늠쇠 동기화하는 장면 gif) (가늠쇠 동기화하는 화면 gif)
2. 탄도학
(탄도학 사진) (탄도학 시연 gif)
탄도학을 구현하였습니다. 총알 오브젝트를 960m/s로 발사합니다. 총구는 가늠쇠 오브젝트의 0.055유닛(현실의 5.5cm에 해당합니다) 밑에 있으며, 아주 미세하게 위쪽을 향하고 있습니다. 덕분에 25m 지점에서 아주 조금 밑, 250m에서 정면을 맞히는 완곡한 포물선을 그리게 됩니다.
3. 크리크 수정
(크리크 수정하는 gif)
크리크 수정은 가늠쇠 동기화를 더 정확하게 교정하는 과정입니다. 현실에서는 총과 사수 간의 오차를 잡는 과정입니다만, 총의 오차를 구현할 수는 없기에 게임 내에서는 카메라 렌즈시프트를 수정하는 과정으로 구현하였습니다. 주의해야 할 점은, 카메라 렌즈시프트 값과 크리크 값은 독립적으로 다루어져야 한다는 것입니다. 
4. 영점 사격 / 실거리 사격
영점 사격은 상술한 크리크 수정을 도와주기 위한 사격 과정입니다. 다만 크리크 수정을 도와줄 뿐만 아니라 사격에 대한 피드백도 제공합니다.
(영점사격지가 다가오며 피드백하는 gif)
호흡불량, 격발불량 등을 피드백해주며, 영점사격지를 확인하여 자신의 탄착군이 어디에 형성되어있는지를 알 수 있습니다.
(실거리 사격에서 명중한 gif) (결과창이 나오는 gif)
실거리 사격은 100m, 200m, 250m 표적을 순서에 따라 사격하는 사격 과정입니다. 이 표적들은 실제로 해당 거리에 위치해 있습니다. 이에 유념하여 사격해야겠죠? 또한 결과창을 보여주여 탄착지점을 알려주니 이를 보고 사격을
5. 응시 모드
(응시 모드로 사격하는 gif)
블루투스 컨트롤러가 없어도 사격을 실시할 수 있는 모드입니다. 옵션 창에서 활성화시킬 수 있으며, 응시하고 있으면 발사됩니다.
7. 블루투스 컨트롤러 연결
(블루투스를 연결하는 gif)
블루투스 컨트롤러를 연결할 수 있습니다. 블루투스 매니저, 블루투스 컨트롤러로 구성되는데, 아두이노 HC-06, HC-05 모델을 기준으로 제작하였기 때문에 호환성을 위해서 해당 모델을 사용하시길 권장드립니다.
8. 사로 통제
(사로 통제하는 gif)
사로 통제도 구현하였습니다. 사격훈련에서 얼타지 않을 수 있겠죠?
  

## 기기 구성 / 필수 조건 안내 (Prerequisites)

* 최소 사양 : 안드로이드 운영체제 4.4(KitKat) 이상
* 아두이노 블루투스 모듈 hc-05, hc-06 기종 권장
  

## 기술 스택 (Technique Used)

### App

- Unity 3D (C# Script)

### IoT

- Arduino

  

## 설치 안내 (Installation Process)

```bash

$ git clone git주소
유니티 허브 프로젝트 추가 버튼 누르기
.\APP\PerfectShotVR 등록

```

## 프로젝트 사용법 (Getting Started)

추후 작성 예정

## 팀 정보 (Team Information)

- 최성민 : 팀장, 앱 개발, 프로젝트 총괄 (JadenChoi2k, jkya02@gmail.com)

- 마승훈 : 팀원, 컨트롤러 개발, 디자인 담당 (sseungh, ink0513@unist.ac.kr)

  

## 저작권 및 사용권 정보 (Copyleft / End User License)

*  [MIT](https://github.com/osam2020-WEB/Sample-ProjectName-TeamName/blob/master/license.md)

  

This project is licensed under the terms of the MIT license.

  

※ [라이선스 비교표(클릭)](https://olis.or.kr/license/compareGuide.do)

  

※ [Github 내 라이선스 키워드(클릭)](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-on-github/licensing-a-repository)

  

※ [\[참조\] Github license의 종류와 나에게 맞는 라이선스 선택하기(클릭)](https://flyingsquirrel.medium.com/github-license%EC%9D%98-%EC%A2%85%EB%A5%98%EC%99%80-%EB%82%98%EC%97%90%EA%B2%8C-%EB%A7%9E%EB%8A%94-%EB%9D%BC%EC%9D%B4%EC%84%A0%EC%8A%A4-%EC%84%A0%ED%83%9D%ED%95%98%EA%B8%B0-ae29925e8ff4)
