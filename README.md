# Cinema Project

Backend API cho hệ thống quản lý và đặt vé xem phim, được xây dựng bằng **ASP.NET Core .NET 8** theo kiến trúc **Clean Architecture**.

## 🚀 Công nghệ sử dụng

* .NET 8 / ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Clean Architecture
* MediatR (CQRS)
* AutoMapper
* Duende IdentityServer
* JWT Authentication & Authorization
* SignalR
* VNPAY Sandbox
* Swagger / OpenAPI
* xUnit

## 📁 Cấu trúc project

```text
CinemaProjectSolution
│
├── Cinema.Api              # REST API và Controllers
├── Cinema.Application      # Business logic, CQRS, Services
├── Cinema.Domain           # Entities và Domain models
├── Cinema.Infrastructure   # Database, Repository, Payment
├── Cinema.SignalR          # Real-time seat notification
├── Cenima.IdentityApi      # Authentication & Authorization
├── SeedWorks               # DTOs, Models, API Responses
└── Cinema.Application.Test # Unit Tests
```

## 🎬 Chức năng chính

* Quản lý phim, thể loại
* Quản lý rạp, phòng và ghế
* Quản lý suất chiếu
* Đặt và hủy vé
* Chọn ghế và kiểm tra trạng thái ghế
* Giữ ghế trong thời gian giới hạn
* Real-time cập nhật trạng thái ghế bằng SignalR
* Quản lý đồ ăn đi kèm booking
* Xác thực và phân quyền bằng IdentityServer + JWT
* Thanh toán qua VNPAY Sandbox
* Background Service xử lý booking hết hạn
* Pagination và xử lý API response thống nhất

## ⚙️ Cài đặt

### 1. Clone project

```bash
git clone <https://github.com/huyhoanggw/CinemaProject.git>
cd CinemaProjectSolution
```

### 2. Cấu hình Database

Cập nhật `ConnectionStrings` trong:

```text
Cinema.Api/appsettings.json
Cenima.IdentityApi/appsettings.json
```

Sau đó chạy migration:

```bash
dotnet ef database update
```

### 3. Chạy project

Chạy các project:

```bash
dotnet run --project Cinema.Api
dotnet run --project Cenima.IdentityApi
```

Hoặc chạy trực tiếp bằng Visual Studio.

## 🔐 Authentication

Project sử dụng **Duende IdentityServer** để xác thực người dùng và cấp JWT Access Token.

Các scope chính:

```text
cinema.read
cinema.write
```

API sử dụng JWT Bearer để xác thực request.

## 💳 Thanh toán

Hệ thống tích hợp **VNPAY Sandbox** để xử lý thanh toán booking.

Thông tin cấu hình nằm trong:

```text
Cinema.Api/appsettings.json
```

> Không commit thông tin secret thật lên GitHub. Nên sử dụng User Secrets hoặc Environment Variables khi deploy.

## 🔄 Real-time Seat

**SignalR** được sử dụng để cập nhật trạng thái ghế theo thời gian thực.

Ví dụ khi một người dùng giữ ghế, những client khác có thể nhận được thông báo và cập nhật trạng thái ghế ngay lập tức.

## 🧪 Unit Test

Project sử dụng **xUnit** để kiểm thử Application Layer.

Chạy test:

```bash
dotnet test
```

## 📌 Mục tiêu

Project được xây dựng nhằm thực hành và áp dụng các kiến thức về:

* Clean Architecture
* CQRS / MediatR
* Repository & Unit of Work
* Authentication / Authorization
* Real-time communication với SignalR
* Payment Gateway
* Background Service
* Entity Framework Core
* Unit Testing
