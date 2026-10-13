# EDOPro Gateball AI (WindBot Add-on)

<p align="center">
  <b>Yu-Gi-Oh! Gateball (GOAT, 0703, 1103) AI for EDOPro / ProjectIgnis</b><br>
  기존 순정 파일 덮어쓰기 없는 무손실 애드온 & 14종 게이트볼 전용 AI
</p>

---

## 📖 프로젝트 소개

본 프로젝트는 **EDOPro (ProjectIgnis)**의 봇 대전(WindBot)에서 유희왕 역사상 가장 인기 있는 3대 게이트볼 포맷(**GOAT, 0703, 1103**)을 지능적인 AI와 함께 즐길 수 있도록 제작된 전용 애드온입니다.

- **무손실 설치 (Non-destructive)**: 기존 WindBot의 순정 파일, 기본 봇, 다른 커스텀 덱을 일체 덮어쓰지 않습니다.
- **Windows Defender 친화적**: 실행 파일(`.exe`) 없이 순수 C# 라이브러리(`.dll`)로 제공됩니다.
- **마스터 룰 1~5 자유 호환**: 어떤 룰 세팅의 방에서도 자유롭게 대전 가능합니다.

---

## 🃏 지원 덱 목록 (총 14종)

### 1. GOAT 포맷 (2005.04)
- **[GOAT] 범용 게이트볼 봇** (`GoatUniversal`): 스탠다드 카오스 굿스터프
- **[GOAT] 데몬** (`GoatDaemon`): 제노사이드 킹 데몬 축 비트다운
- **[GOAT] 변이카오스** (`GoatMetamorphosisChaos`): 마도전사 브레이커, 사우전드 아이즈 새크리파이스 변이 연계
- **[GOAT] 다크 카오스 터보** (`GoatZorcChaosTurbo`): 어둠/빛 속성 압축 및 암흑지배자 조크
- **[GOAT] 카오스 비트** (`GoatChaosWarrior`): 블레이드 나이트, 이차원의 여전사 전사 비트
- **[GOAT] 화지구지** (`GoatHinoKaguTsuchi`): 불꽃의 지배자 & 화지구지 핸드 파괴 콤보

### 2. 0703 포맷 (2007.03)
- **[0703] 범용 게이트볼 봇** (`0703Universal`): 싸이크론, 고즈, 황천개구리 탑재
- **[0703] 생환 야수** (`0703SafeReturnBeast`): 생환의 패 기반 드로우 가속
- **[0703] 언데드** (`0703Undead`): 좀비 마스터, 마두귀 묘지 특소 연계
- **[0703] 오션 비트** (`0703OceanBeat`): **N·그란 몰**, E·HERO 에어맨 & 오션 무한 회수 루프

### 3. 1103 포맷 (2011.03)
- **[1103] 범용 게이트볼 봇** (`1103Universal`): 이펙트 뵐러, 싱크로 소환 탑재
- **[1103] 결속 가제트** (`1103SolidarityGadget`): 일족의 결속 타점 비트 & 머시너즈 포트리스
- **[1103] 스드히어로** (`1103SkillDrainHero`): 스킬 드레인 & 신수왕 바르바로스, 디아볼릭 가이
- **[1103] 제왕** (`1103Monarch`): 황천개구리 제물 제왕(가이우스, 라이저)

---

## ⚡ 주요 탑재 알고리즘

- **카운터 함정 안전 공식 (신의 심판 / 신의 경고)**:
  - 아군 몬스터 소환 시 자신의 소환을 카운터 함정으로 무효화하던 순정 WindBot의 구조적 결함 완벽 박멸.
  - 상대의 핵심 파워 카드(태풍, 블랙홀, 욕망의 항아리, 담드, 싱크로/융합 몬스터 등)에만 정밀 카운터.
- **수축 (Shrink) 데미지 스텝 정밀 타격**:
  - 배틀페이즈 개시 시 조기 발동 차단.
  - 오직 데미지 스텝 / 데미지 계산 시에 아군이 승리할 수 있는 역전 각에만 발동.
- **N·그란 몰 (Neo-Spacian Grand Mole) 전용 루프**:
  - 상대 필드에 몬스터 존재 시 최우선 소환.
  - 상대 최강 보스(싱크로/융합, 고타점, 스피릿 리퍼 등 파괴내성 벽)를 공격력 상관없이 우선 타격하여 패/엑스트라 덱으로 영구 바운스.
- **전투 지능화 및 자폭 방지**:
  - 수비/세트 몬스터를 주저 없이 공격하여 필드 클리어.
  - 돌파 불가능한 상대에 대한 무모한 들이받기 자폭을 차단하고 안전하게 메인 2로 턴 이양.

---

## 📥 설치 방법

### 방법 1: 1-클릭 자동 설치 (추천)
1. 릴리즈에서 `EDOPro_Gateball_AI_v1.0.zip`을 다운로드하여 **EDOPro 폴더**(`EDOPro.exe`가 있는 곳)에 압축을 풉니다.
2. `Install_Gateball_AI.bat`을 더블 클릭하여 실행합니다.
3. 기존 `bots.json`이 자동 백업(`bots.json.bak`)되고, 기존 봇 목록에 게이트볼 봇 14종이 자동으로 안전하게 병합됩니다!

### 방법 2: 수동 설치
1. `WindBot/Executors/GateballExecutors.dll`을 본인 EDOPro의 `WindBot/Executors/` 폴더에 복사합니다.
2. `WindBot/Decks/` 내 14개 `.ydk` 파일들을 본인 EDOPro의 `WindBot/Decks/` 폴더에 복사합니다.
3. 메모장으로 본인 EDOPro의 `WindBot/bots.json`을 열고, `gateball_bots.json`의 내용을 복사하여 기존 리스트 맨 뒤에 추가하고 저장합니다.

---

## 🛠 빌드 방법 (개발자용)

C# 소스코드([GateballExecutors.cs](GateballExecutors.cs))를 직접 컴파일하려면 .NET Framework 4.0 이상 환경에서 다음 명령어를 실행합니다:

```cmd
csc /target:library /r:ExecutorBase.dll /out:GateballExecutors.dll GateballExecutors.cs
```

---

## 📜 라이선스

이 프로젝트는 오픈 소스이며, EDOPro 및 WindBot 커뮤니티의 가이드라인을 준수합니다.
