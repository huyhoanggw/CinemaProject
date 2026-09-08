# Cinema Project

Backend API cho hệ thống quản lý và đặt vé xem phim, được xây dựng bằng **ASP.NET Core .NET 8** theo kiến trúc **Clean Architecture**.

Project tập trung vào việc xây dựng một hệ thống booking thực tế với các chức năng như **quản lý phim, đặt vé, giữ ghế, real-time seat status, thanh toán VNPAY và authentication/authorization**.

---

## 🚀 Công nghệ sử dụng

* **.NET 8 / ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Redis**
* **Clean Architecture**
* **MediatR / CQRS**
* **AutoMapper**
* **Duende IdentityServer**
* **JWT Authentication & Authorization**
* **SignalR**
* **VNPAY Sandbox**
* **Docker / Docker Compose**
* **Swagger / OpenAPI**
* **xUnit**

---

## 📁 Cấu trúc project

```text
CinemaProjectSolution
│
├── Cinema.Api
│   └── REST API và Controllers
│
├── Cinema.Application
│   └── Business Logic, CQRS, Services
│
├── Cinema.Domain
│   └── Entities và Domain Models
│
├── Cinema.Infrastructure
│   └── Database, Repository, Redis, Payment
│
├── Cinema.SignalR
│   └── Real-time Seat Notification
│
├── Cenima.IdentityApi
│   └── Authentication & Authorization
│
├── SeedWorks
│   └── DTOs, Models, API Responses
│
└── Cinema.Application.Test
    └── Unit Tests
```

---

## 🎬 Chức năng chính

### Movie & Showtime

* Quản lý phim
* Quản lý thể loại
* Quản lý rạp / theater
* Quản lý phòng chiếu
* Quản lý ghế
* Quản lý suất chiếu

### Booking

* Đặt vé
* Hủy booking
* Chọn nhiều ghế trong một booking
* Kiểm tra trạng thái ghế
* Giữ ghế trong thời gian giới hạn
* Quản lý đồ ăn đi kèm booking
* Tự động xử lý booking hết hạn

### Authentication & Authorization

* Đăng ký / đăng nhập
* JWT Authentication
* Role-based Authorization
* Permission-based Authorization
* Duende IdentityServer

### Payment

* Tạo payment
* Thanh toán thông qua VNPAY Sandbox
* Payment Callback
* Payment Return
* Cập nhật trạng thái booking sau khi thanh toán

### Real-time

* Real-time cập nhật trạng thái ghế
* Thông báo khi ghế được giữ
* Thông báo khi ghế được giải phóng
* SignalR Group theo từng Showtime

---

# 🗄️ Database

Project sử dụng **SQL Server** làm database chính.

SQL Server lưu trữ dữ liệu persistent của hệ thống như:

```text
Movie
Genre
Theater
Room
Seat
Showtime
ShowtimeSeat
Booking
BookingSeat
BookingFood
Food
Payment
...
```

SQL Server được xem là **source of truth** của hệ thống.

---

# 🔴 Redis

Redis được sử dụng để xử lý **temporary seat hold / distributed seat locking** trong quá trình booking.

Redis **không thay thế SQL Server** mà đóng vai trò là một temporary storage cho trạng thái giữ ghế.

## Seat Hold

Khi user chọn ghế:

```text
User
  │
  ▼
Cinema API
  │
  ├── Check ShowtimeSeat trong SQL Server
  │
  ▼
Redis
  │
  ├── SET NX
  ├── userId
  └── TTL
```

Mỗi ghế được lưu bằng một Redis key riêng:

```text
showtime:{showtimeId}:seat:{seatId}:hold
```

Ví dụ:

```text
showtime:10:seat:25:hold
```

Value:

```text
userId
```

TTL mặc định:

```text
10 minutes
```

Redis sử dụng `SET NX` để đảm bảo chỉ một user có thể giành quyền giữ một ghế tại cùng một thời điểm.

---

## 🔒 Multi-seat Hold

Một user có thể chọn nhiều ghế:

```text
A1
A2
A3
```

Hệ thống sẽ tạo các Redis key:

```text
showtime:10:seat:A1:hold
showtime:10:seat:A2:hold
showtime:10:seat:A3:hold
```

Nếu một trong các ghế đã được user khác giữ:

```text
A1 → Success
A2 → Success
A3 → Failed
```

Hệ thống sẽ release các key đã tạo trước đó:

```text
A1 → Release
A2 → Release
A3 → Failed
```

Điều này giúp tránh trường hợp user chỉ giữ được một phần số ghế đã chọn.

---

# 🔄 Booking Flow

Flow chính khi user đặt vé:

```text
User selects seats
        │
        ▼
Check Showtime
        │
        ▼
Get ShowtimeSeat
        │
        ▼
Check Seat Availability
        │
        ▼
Redis SET NX
        │
        ├── Failed → Another user is holding seat
        │
        ▼
Update ShowtimeSeat
Status = Hold
        │
        ▼
Create Booking
        │
        ▼
Create BookingSeat
        │
        ▼
Create BookingFood
        │
        ▼
SQL Transaction
        │
        ▼
Booking Created
```

---

# 💺 Seat Status

Trạng thái ghế được quản lý trên SQL Server.

Ví dụ:

```text
Available
    │
    ▼
  Hold
    │
    ▼
  Sold
```

Khi user giữ ghế:

```text
Redis:
seat → userId + TTL

SQL:
ShowtimeSeat.Status = Hold
```

Khi thanh toán thành công:

```text
SQL:
Hold → Sold

Booking:
Pending → Confirmed

Payment:
Pending → Success

Redis:
Delete Hold Key
```

---

# ⏱️ Redis TTL & Background Service

Redis sử dụng TTL để tự động xóa temporary hold key.

Ví dụ:

```text
showtime:10:seat:25:hold
TTL = 600 seconds
```

Sau 10 phút Redis key sẽ tự động biến mất.

Tuy nhiên, Redis hết TTL **không tự động cập nhật SQL Server**.

Vì vậy project sử dụng **Background Service** để kiểm tra các ghế đã hết thời gian giữ:

```text
Background Service
        │
        ▼
Find ShowtimeSeat
Status = Hold
ReservedUntil <= UtcNow
        │
        ▼
Status = Available
        │
        ▼
Save SQL Server
        │
        ▼
SignalR
        │
        ▼
Notify Clients
```

---

# 📡 SignalR

**SignalR** được sử dụng để cập nhật trạng thái ghế theo thời gian thực.

Client tham gia SignalR Group theo `ShowtimeId`.

Ví dụ:

```text
Showtime 10
    │
    ├── User A
    ├── User B
    └── User C
```

Khi User A giữ ghế:

```text
User A
  │
  ▼
Cinema API
  │
  ▼
Redis + SQL Server
  │
  ▼
SignalR
  │
  ▼
Showtime Group
  │
  ├── User B
  └── User C
```

Các client khác nhận event:

```text
SeatsStatusChanged
```

và cập nhật giao diện mà không cần refresh trang.

---

# 💳 Payment

Project tích hợp **VNPAY Sandbox** để xử lý thanh toán booking.

Payment flow:

```text
Create Booking
      │
      ▼
Booking = Pending
      │
      ▼
Payment = Pending
      │
      ▼
VNPAY
      │
      ▼
Payment Callback
      │
      ▼
Verify Payment
      │
      ├── Failed
      │     └── Release Seat
      │
      └── Success
            │
            ├── Booking = Confirmed
            ├── Payment = Success
            ├── ShowtimeSeat = Sold
            └── Release Redis Hold
```

Thông tin cấu hình VNPAY nằm trong:

```text
Cinema.Api/appsettings.json
```

> **Không commit thông tin secret thật lên GitHub.**
> Nên sử dụng User Secrets hoặc Environment Variables khi deploy.

---

# 🔐 Authentication

Project sử dụng **Duende IdentityServer** để xác thực người dùng và cấp JWT Access Token.

Các scope chính:

```text
cinema.read
cinema.write
```

API sử dụng:

```text
JWT Bearer Authentication
```

để xác thực request.

Authorization được thực hiện thông qua:

```text
Role
Permission
Policy
```

---

# 🐳 Docker

Redis có thể được chạy thông qua Docker Compose.

Ví dụ:

```yaml
services:

  redis:
    image: redis:7-alpine
    container_name: cinema-redis
    ports:
      - "6379:6379"
```

Kiểm tra Redis:

```bash
docker ps
```

Kiểm tra Redis CLI:

```bash
docker exec -it cinema-redis redis-cli
```

Test Redis:

```bash
PING
```

Kết quả:

```text
PONG
```

Kiểm tra các key:

```bash
SCAN 0 MATCH "showtime:*:seat:*:hold"
```

Kiểm tra một seat hold:

```bash
GET showtime:10:seat:25:hold
```

Kiểm tra TTL:

```bash
TTL showtime:10:seat:25:hold
```

---

# ⚙️ Cài đặt

## 1. Clone project

```bash
git clone https://github.com/huyhoanggw/CinemaProject.git
cd CinemaProjectSolution
```

## 2. Cấu hình Database

Cập nhật `ConnectionStrings` trong:

```text
Cinema.Api/appsettings.json
Cenima.IdentityApi/appsettings.json
```

Sau đó chạy migration:

```bash
dotnet ef database update
```

---

## 3. Cấu hình Redis

Thêm Redis connection string:

```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

Nếu API cũng chạy bên trong Docker Compose, Redis host sẽ là tên service:

```text
redis:6379
```

thay vì:

```text
localhost:6379
```

---

## 4. Chạy Redis

Nếu sử dụng Docker Compose:

```bash
docker compose up -d redis
```

Kiểm tra:

```bash
docker ps
```

---

## 5. Chạy project

Chạy API:

```bash
dotnet run --project Cinema.Api
```

Chạy Identity Server:

```bash
dotnet run --project Cenima.IdentityApi
```

Hoặc chạy trực tiếp bằng **Visual Studio**.

---

# 🧪 Unit Test

Project sử dụng **xUnit** để kiểm thử Application Layer.

Chạy toàn bộ test:

```bash
dotnet test
```

Có thể chạy test theo project:

```bash
dotnet test Cinema.Application.Test
```

---

# 🧪 Redis Testing

Một số trường hợp quan trọng cần kiểm thử:

### 1. Một user giữ nhiều ghế

```text
User A
 ├── Seat A1
 ├── Seat A2
 └── Seat A3
```

Kiểm tra Redis có đủ 3 key và cùng thuộc về User A.

---

### 2. Hai user cùng giữ một ghế

```text
User A ──► Seat A1 ──► Success
User B ──► Seat A1 ──► Failed
```

Redis phải chỉ chứa `userId` của User A.

---

### 3. Multi-seat rollback

```text
User A
 ├── A1 → Success
 ├── A2 → Success
 └── A3 → Failed
```

Sau khi request thất bại:

```text
A1 → Released
A2 → Released
A3 → User khác đang giữ
```

---

### 4. TTL expiration

Kiểm tra:

```bash
TTL showtime:10:seat:25:hold
```

Sau khi TTL hết:

```text
Redis key → Deleted
```

Background Service sẽ xử lý:

```text
SQL ShowtimeSeat
Hold → Available
```

---

### 5. Payment Success

Kiểm tra:

```text
Booking      = Confirmed
Payment      = Success
ShowtimeSeat = Sold
Redis Hold   = Deleted
```

---

# 📊 Kiến trúc tổng quan

```text
                    ┌──────────────────┐
                    │      Client      │
                    └────────┬─────────┘
                             │
                  ┌──────────▼──────────┐
                  │     Cinema.Api      │
                  └──────────┬──────────┘
                             │
              ┌──────────────▼──────────────┐
              │       Application           │
              │                             │
              │ CQRS / MediatR / Services   │
              └──────────────┬──────────────┘
                             │
             ┌───────────────┼────────────────┐
             │               │                │
             ▼               ▼                ▼
       ┌──────────┐    ┌───────────┐   ┌────────────┐
       │ SQL      │    │   Redis   │   │  SignalR   │
       │ Server   │    │ Seat Hold │   │ Real-time  │
       └──────────┘    └───────────┘   └────────────┘
             │
             ▼
       ┌──────────────┐
       │ VNPAY        │
       │ Payment      │
       └──────────────┘
```

---

# 🏗️ Kiến trúc

Project áp dụng **Clean Architecture** nhằm tách biệt business logic khỏi infrastructure.

```text
Cinema.Api
     │
     ▼
Cinema.Application
     │
     ▼
Cinema.Domain

Cinema.Infrastructure
     │
     ├── EF Core
     ├── SQL Server
     ├── Redis
     ├── Repository
     └── Payment Gateway

Cinema.SignalR
     │
     └── Real-time Communication

Cenima.IdentityApi
     │
     └── Authentication / Authorization
```

---

# 📌 Mục tiêu Project

Project được xây dựng nhằm thực hành và áp dụng các kiến thức về:

* Clean Architecture
* CQRS / MediatR
* Repository Pattern
* Unit of Work
* Entity Framework Core
* SQL Server
* Redis
* Distributed Seat Locking
* JWT Authentication / Authorization
* Duende IdentityServer
* Real-time communication với SignalR
* Payment Gateway
* VNPAY
* Background Service
* Docker / Docker Compose
* Unit Testing
* API Design
* Concurrency Handling

---

## 👨‍💻 Author

**Hoang**

GitHub:

```text
https://github.com/huyhoanggw/CinemaProject
```
