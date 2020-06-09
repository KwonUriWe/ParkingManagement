# ParkingManagement
## Json, Xml, DB를 이용한 주차 관리 프로그램

### 화면 구성
![K-001](https://user-images.githubusercontent.com/59382990/84114596-9ec0a480-aa67-11ea-93cd-d5bb2192ed05.jpg)

### 기능 테스트

#### 주차하기
- 주차 완료   
![K-002](https://user-images.githubusercontent.com/59382990/84114603-a122fe80-aa67-11ea-97b2-82dd25db59fa.jpg)

- 입력되지 않은 정보가 있어 주차 불가   
![K-003](https://user-images.githubusercontent.com/59382990/84114608-a1bb9500-aa67-11ea-9634-a94d6e008012.jpg)

- 이미 주차 중인 주차공간으로 주차 불가   
![K-004](https://user-images.githubusercontent.com/59382990/84114612-a2542b80-aa67-11ea-894b-4d8a5adbf701.jpg)

- 존재하지 않는 주차공간으로 주차 불가   
![K-005](https://user-images.githubusercontent.com/59382990/84114614-a2ecc200-aa67-11ea-8605-26107d5f704d.jpg)

#### 출차하기

- 출차 완료   
![K-001](https://user-images.githubusercontent.com/59382990/84114999-44741380-aa68-11ea-9cf6-f8eb8f2fbf43.jpg)

- 주차공간 번호가 입력되지 않아 출차 불가   
![K-002](https://user-images.githubusercontent.com/59382990/84115003-45a54080-aa68-11ea-9429-8002321b8ef3.jpg)

- 빈 주차공간으로 출차 불가
![K-003](https://user-images.githubusercontent.com/59382990/84115007-463dd700-aa68-11ea-8277-220883850396.jpg)

- 존재하지 않는 주차공간으로 출차 불가
![K-004](https://user-images.githubusercontent.com/59382990/84115009-46d66d80-aa68-11ea-9506-b31335fd3c48.jpg)

#### 주차공간 확인하기

- 주차공간 번호가 입력되지 않아 확인 불가
![K-001](https://user-images.githubusercontent.com/59382990/84115281-c401e280-aa68-11ea-89f5-cbbc300c6649.jpg)

- 확인시 빈 주차공간   
![K-002](https://user-images.githubusercontent.com/59382990/84115285-c5330f80-aa68-11ea-94ff-5c84cd372599.jpg)

- 확인시 주차된 차량 있음   
![K-003](https://user-images.githubusercontent.com/59382990/84115287-c5cba600-aa68-11ea-9dc8-7ea1c759c204.jpg)

- 확인시 존재하지 않는 주차공간   
![K-004](https://user-images.githubusercontent.com/59382990/84115289-c6fcd300-aa68-11ea-8b55-704396250e85.jpg)

#### 로그파일    
![K-005](https://user-images.githubusercontent.com/59382990/84115362-ed227300-aa68-11ea-8775-6bd6280865f5.jpg)

### 추후 개선해야 할 사항   
- 관리자 탭을 추가하여 주차공간의 수를 지정할 수 있도록 한다.
- 데이터 그리드의 셀을 클릭하면 주차/출차 그룹박스 뿐 아니라 주차공간 확인 그룹박스에도 셀에 저장된 정보가 자동 기입 되도록 한다.
