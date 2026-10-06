# smartAgriculture

简体中文说明文档（README）。

## 项目概述
本项目是基于 WPF 的智能农业数据采集与展示桌面应用（`smartAgriculture`）。通过 RS-485/串口与传感器通信，解析土壤相关数据（温度、含水率、pH、氮/磷/钾等），并定期写入 MySQL 数据库，使用 `LiveCharts` 展示历史曲线与仪表盘。

## 技术栈
- .NET Framework 4.7.2（WPF 桌面应用）
- C#（WPF / MVVM 风格）
- LiveCharts（图表展示）
- MySQL（数据持久化）
- `MySql.Data`（MySQL ADO.NET 驱动）
- 串口通信：`System.IO.Ports`（RS-485 逻辑在 `SerialPort485` 命名空间中）

## 本地准备
1. 操作系统：Windows 10/11
2. Visual Studio 2019/2022（含 .NET Framework 4.7.2 支持）
3. MySQL Server
4. 还原/安装 NuGet 包：`LiveCharts`、`MySql.Data`
5. 串口访问权限（若接入真实设备）

## 必要配置
- MySQL 连接串位于：`Models/SystemGlobalVariable.cs` 中的 `mysqlconnstr`。默认：

```
server=localhost;port=3306;database=soildata;uid=root;pwd=123456;Charset=utf8
```

请根据实际修改数据库主机、账户和密码。

- 串口配置通过 `SystemGlobalVariable.mySerialInfo` 初始化。运行前请设置 `PortName`、`BaudRate` 等。

## 数据库初始化（示例 SQL）
```sql
CREATE DATABASE IF NOT EXISTS soildata CHARACTER SET utf8 COLLATE utf8_general_ci;
USE soildata;

CREATE TABLE IF NOT EXISTS tb_historydata (
  id INT AUTO_INCREMENT PRIMARY KEY,
  `Date` DATETIME NULL,
  Temperature FLOAT NULL,
  WaterContent FLOAT NULL,
  PhContent FLOAT NULL,
  Nitrogen FLOAT NULL,
  Phosphorus FLOAT NULL,
  Potassium FLOAT NULL,
  insert_time DATETIME NULL
);

CREATE TABLE IF NOT EXISTS tb_Page (
  id INT AUTO_INCREMENT PRIMARY KEY,
  TemperatrueUpper FLOAT,
  TemperatrueLower FLOAT,
  WaterContentUpper FLOAT,
  WaterContentLower FLOAT,
  PhUpper FLOAT,
  PhLower FLOAT,
  NitrogenUpper FLOAT,
  NitrogenLower FLOAT,
  PhosphorusUpper FLOAT,
  PhosphorusLower FLOAT,
  PotassiumUpper FLOAT,
  PotassiumLower FLOAT,
  room_temperatureUpper FLOAT,
  room_temperatureLower FLOAT
);

CREATE TABLE IF NOT EXISTS tb_Alarm (
  id INT AUTO_INCREMENT PRIMARY KEY,
  `time` DATETIME,
  `alarm` TEXT
);
```

## 启动流程（简要）
1. 克隆仓库并打开解决方案（`.sln`）于 Visual Studio。
2. 等待 NuGet 包恢复（或手动还原）。
3. 在 `Models/SystemGlobalVariable.cs` 修改 `mysqlconnstr` 为你的 DB 连接串。
4. 配置串口参数（`PortName`、`BaudRate` 等）。
5. 生成解决方案（`生成` -> `生成解决方案`）。
6. 运行应用（`调试` -> `开始调试` 或 `开始执行（不调试）`）。

## 运行时说明
- 应用在后台从 RS-485 接收数据并解析（`SerialPort485/Rtu485.cs`），解析结果保存到 `SystemGlobalVariable` 并定期写入 MySQL（`Models/DBHelper.cs`）。
- 若使用真实设备，确保串口号正确且未被占用。

## 常见问题
- 无法连接 MySQL：检查 `mysqlconnstr`、MySQL 服务、端口与防火墙。
- 串口错误：确认 COM 口存在且未被占用，检查波特率及串口权限。


