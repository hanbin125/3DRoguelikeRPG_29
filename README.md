# 3DRoguelikeRPG_29
 
프로젝트 소개

- **로스트 던전**은 던전의 각 방을 돌파하며 최종 보스를 처치하는 것을 목표로 하는 **탑뷰 3D 로그라이크 모바일 RPG**입니다. 플레이어는 다양한 스킬과 능력 강화를 통해 매 스테이지마다 새롭게 도전하게 됩니다.
- 튜토리얼
    
![image](https://github.com/user-attachments/assets/cd7e5280-55a1-4ca4-b5b2-07a79c6ff746)

![image](https://github.com/user-attachments/assets/46e1f83d-10ed-4f01-b004-8be8526b5d16)
![image](https://github.com/user-attachments/assets/bfc396c9-0b00-40cb-bf66-a39c1473cb67)
![image](https://github.com/user-attachments/assets/983712cf-70c1-4edb-98ac-050d3160dc4d)
![image](https://github.com/user-attachments/assets/a552ecac-271a-4743-a5f7-053b6bd9f802)
![image](https://github.com/user-attachments/assets/9f4279cb-be7c-4bbf-97c6-d1d403d06449)
![image](https://github.com/user-attachments/assets/09f76dea-0c0c-4773-8e03-dd5b2ab95198)
![image](https://github.com/user-attachments/assets/6ed53a91-3185-47d2-9ac2-f4ad865fe7f3)
![image](https://github.com/user-attachments/assets/7321b679-bd85-4272-a499-59407b7b4989)
![image](https://github.com/user-attachments/assets/825ca2d7-400f-4076-b7e5-500d5f5349b7)
![image](https://github.com/user-attachments/assets/3a8d08d2-ed14-4494-b72c-24492590f2d9)
![image](https://github.com/user-attachments/assets/698c9d40-3f9c-46c0-95af-80094fc17a5c)


---

## 프로젝트 기획

### 장르 : 3D 탑뷰 액션 로그라이크 RPG

### 스타일 : 판타지 + 던전 어드벤쳐

- 랜덤으로 생성되는 던전 방을 클리어하며 진행 경로를 선택하는 탐험 중심 구조
- 장비와 유물을 구매·강화하며 성장하는 시스템
- 세 가지 고유 스킬로 전투를 완수하는 집중도 높은 실시간 액션

---

## 스토리

```jsx
세상의 균형을 지키던 고대의 봉인이 무너졌다.
던전 깊은 곳에서 잠들어 있던 어둠이 다시 깨어난다.
왕국은 침묵했고, 영웅들은 사라졌다.
이제, 한 명의 전사만이 그 문을 통과하려 한다.
그 끝에서 무엇이 기다릴지는 아무도 모른다.
```

### 사용된 기술 스택

---

- Unity
- C#
- Github

---

### 클라이언트 구조

```markdown
├── Scripts/                    # 게임 로직 스크립트
│   ├── Manager/               # 핵심 매니저 시스템
│   ├── Player/                # 플레이어 관련 스크립트
│   ├── Enemy/                 # 적 AI 및 행동 패턴
│   ├── Map/                   # 맵 생성 및 관리
│   ├── UI/                    # 사용자 인터페이스
│   ├── Inventory/             # 인벤토리 시스템
│   ├── Item/                  # 아이템 시스템
│   ├── Skill/                 # 스킬 시스템
│   ├── Shop/                  # 상점 시스템
│   ├── Enhance/               # 강화 시스템
│   ├── Tutorial/              # 튜토리얼 시스템
│   └── GameData/              # 게임 데이터 관리
```

---

### 프로젝트 결과 및 성과
✅ **핵심 게임플레이**
- 3D 액션 전투 시스템
- 로그라이크 랜덤 맵 생성
- 플레이어 이동 및 전투 (공격, 회피)
- 적 AI 및 다양한 몬스터 타입

✅ **RPG 시스템**
- 능력치 시스템
- 아이템 및 장비 시스템
- 스킬 학습 및 사용
- 인벤토리 관리

✅ **게임 진행 시스템**
- 상점 시스템 (골드/소울 화폐)
- 아이템 강화 시스템
- 세이브/로드 기능

✅ **UI/UX**
- 게임 내 UI 인터페이스
- 인벤토리 및 장비창
- 스킬창 및 상점 UI

#### 1.1 기술적 성과
- **Unity 3D 엔진 활용**: 3D 환경에서의 로그라이크 게임 구현
- **모듈화된 매니저 시스템**: 유지보수성과 확장성을 고려한 구조 설계
- **랜덤 생성 알고리즘**: 로그라이크 특성을 살린 맵 생성 시스템
- **데이터 관리 시스템**: ScriptableObject를 활용한 게임 데이터 관리

#### 1.2 기술적 학습
- **Unity 3D 개발 경험**: 3D 환경에서의 게임 개발 노하우 습득
- **게임 아키텍처 설계**: 매니저 패턴을 활용한 시스템 설계 경험
- **로그라이크 구현**: 랜덤 생성 알고리즘 및 절차적 콘텐츠 생성 경험

#### 1.3 프로젝트 관리 학습
- **시간 관리의 중요성**: 제한된 시간과 인원으로 인한 기능 우선순위 결정 경험
- **사용자 중심 설계**: 개발자 관점과 사용자 관점의 차이 인식
- **피드백 수집 및 분석**: 실제 유저 테스트를 통한 개선점 도출
